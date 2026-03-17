#!/usr/bin/env bash
# ============================================================
#  run-tests.sh  -  NUnitAppium Runner for macOS and Linux
#  Runtime: .NET 10
# ============================================================
set -e

GREEN='\033[0;32m'; YELLOW='\033[1;33m'; RED='\033[0;31m'; NC='\033[0m'
ok()   { echo -e "${GREEN}[OK]${NC}    $1"; }
info() { echo -e "${YELLOW}[INFO]${NC}  $1"; }
err()  { echo -e "${RED}[ERROR]${NC} $1"; }

SCRIPTDIR="$(cd "$(dirname "${BASH_SOURCE[0]}")" && pwd)"

echo ""
echo "================================================"
echo "  NUnitAppium - LambdaTest TestMu AI Runner"
echo "  Runtime: .NET 10"
echo "================================================"
echo ""

# ── Step 0: Check credentials ────────────────────────────────
if [[ -z "$LT_USERNAME" ]]; then
    err "LT_USERNAME is not set!"
    echo ""
    echo "  Run these first in this terminal:"
    echo "    export LT_USERNAME=your_username_here"
    echo "    export LT_ACCESS_KEY=your_access_key_here"
    echo "    bash run-tests.sh"
    echo ""
    exit 1
fi
if [[ -z "$LT_ACCESS_KEY" ]]; then
    err "LT_ACCESS_KEY is not set!"
    echo "  Run: export LT_ACCESS_KEY=your_access_key_here"
    exit 1
fi
ok "Credentials set for user: $LT_USERNAME"

# ── Step 1: Check .NET 10 SDK ────────────────────────────────
echo ""
echo "[1/3] Checking .NET SDK..."

install_dotnet_macos() {
    info "Installing .NET 10 via Homebrew..."
    if ! command -v brew &>/dev/null; then
        err "Homebrew not found. Install it first:"
        echo "  /bin/bash -c \"\$(curl -fsSL https://raw.githubusercontent.com/Homebrew/install/HEAD/install.sh)\""
        exit 1
    fi
    brew install --cask dotnet-sdk
}

install_dotnet_linux() {
    info "Installing .NET 10 on Linux..."
    # Microsoft's universal install script
    curl -fsSL https://dot.net/v1/dotnet-install.sh | bash /dev/stdin --channel 10.0
    export PATH="$HOME/.dotnet:$PATH"
    echo 'export PATH="$HOME/.dotnet:$PATH"' >> ~/.bashrc
    echo 'export DOTNET_ROOT="$HOME/.dotnet"' >> ~/.bashrc
}

if ! command -v dotnet &>/dev/null; then
    if [[ "$OSTYPE" == "darwin"* ]]; then
        install_dotnet_macos
    else
        install_dotnet_linux
    fi
fi

# Check version
DOTNET_VER=$(dotnet --version 2>/dev/null || echo "unknown")
ok "dotnet found: $DOTNET_VER"

if [[ ! "$DOTNET_VER" == 10.* ]]; then
    info "Found .NET $DOTNET_VER - .NET 10 is preferred but continuing..."
fi

# ── Step 2: Restore packages and build ───────────────────────
echo ""
echo "[2/3] Restoring packages and building..."
dotnet build "$SCRIPTDIR/NUnitAppium.sln" --configuration Release --nologo
ok "Build succeeded."

# ── Step 3: Run tests ─────────────────────────────────────────
echo ""
echo "[3/3] Running tests..."
echo ""
echo "------------------------------------------------"
echo "  Username : $LT_USERNAME"
echo "  Device 1 : OnePlus 6  (Android 8)"
echo "  Device 2 : iPhone 11  (iOS 14)"
echo "  Dashboard: https://appautomation.lambdatest.com/build"
echo "------------------------------------------------"
echo ""

dotnet test "$SCRIPTDIR/NUnitAppium.sln" \
    --configuration Release \
    --no-build \
    --nologo \
    --logger "console;verbosity=normal"

echo ""
echo "================================================"
echo "  Done! View results at:"
echo "  https://appautomation.lambdatest.com/build"
echo "================================================"
echo ""
