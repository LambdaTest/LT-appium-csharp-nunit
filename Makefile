# ─────────────────────────────────────────────────────────────
# Makefile  -  NUnitAppium macOS/Linux runner (.NET 10)
#
# Usage:
#   make          -> build + test
#   make build    -> compile only
#   make test     -> run tests (build first)
#   make clean    -> remove build artifacts
# ─────────────────────────────────────────────────────────────

.PHONY: all build test clean

all: build test

build:
	dotnet build NUnitAppium.sln --configuration Release --nologo

test:
	dotnet test NUnitAppium.sln --configuration Release --no-build --nologo \
		--logger "console;verbosity=normal"

clean:
	dotnet clean NUnitAppium.sln --nologo
	rm -rf NUnitAppium/bin NUnitAppium/obj
