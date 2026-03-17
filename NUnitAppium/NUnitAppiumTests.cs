using System;
using System.Collections.Generic;
using OpenQA.Selenium;
using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Appium.Android;
using OpenQA.Selenium.Appium.iOS;
using OpenQA.Selenium.Support.UI;
using NUnit.Framework;

// ─────────────────────────────────────────────────────────────────────────────
// NUnitAppiumTests.cs
// Parallel Appium tests on LambdaTest Real Device Cloud.
// Built with: .NET 10 | Appium.WebDriver 5.x | NUnit 4 | W3C Protocol
//
// HOW TO CHOOSE WHAT TO RUN:
//   - Run ONLY Android : comment out the iOS [TestFixture] line
//   - Run ONLY iOS     : comment out the Android [TestFixture] line
//   - Run BOTH         : keep both lines (they run in parallel)
// ─────────────────────────────────────────────────────────────────────────────

namespace NUnitAppium;

[TestFixture("Galaxy S24", "14", "Android", "lt://proverbial-android")]  // Android
// [TestFixture("iPhone 15", "17", "iOS", "lt://proverbial-ios")]         // iOS — uncomment to run

[Parallelizable(ParallelScope.Fixtures)]
public class NUnitAppiumTests
{
    // ── LambdaTest credentials ────────────────────────────────────────────────
    private static readonly string LT_USERNAME =
        Environment.GetEnvironmentVariable("LT_USERNAME") ?? "YOUR_LT_USERNAME";

    private static readonly string LT_ACCESS_KEY =
        Environment.GetEnvironmentVariable("LT_ACCESS_KEY") ?? "YOUR_LT_ACCESS_KEY";

    // LambdaTest Appium hub URL
    private const string HUB_URL = "https://mobile-hub.lambdatest.com/wd/hub";

    // Full package prefix for Proverbial Android app element IDs
    private const string ANDROID_PKG = "com.lambdatest.proverbial:id/";

    // Per-fixture parameters
    private readonly string _deviceName;
    private readonly string _platformVersion;
    private readonly string _platformName;
    private readonly string _app;

    // Appium driver
    private AppiumDriver _driver;

    public NUnitAppiumTests(string deviceName, string platformVersion,
                            string platformName, string app)
    {
        _deviceName      = deviceName;
        _platformVersion = platformVersion;
        _platformName    = platformName;
        _app             = app;
    }

    [SetUp]
    public void Init()
    {
        bool isIOS = _platformName.Equals("iOS", StringComparison.OrdinalIgnoreCase);

        // Log what is about to be created
        Console.WriteLine($"[SetUp] Connecting to LambdaTest -> {_deviceName} | {_platformName} {_platformVersion}");

        // ── Validate credentials are not placeholder values ───────────────────
        if (LT_USERNAME == "YOUR_LT_USERNAME" || LT_ACCESS_KEY == "YOUR_LT_ACCESS_KEY")
        {
            throw new InvalidOperationException(
                "LT_USERNAME or LT_ACCESS_KEY environment variables are not set!\n" +
                "Run:  set LT_USERNAME=your_username\n" +
                "      set LT_ACCESS_KEY=your_key\n" +
                "Then run the test again in the SAME CMD window.");
        }

        // ── Build AppiumOptions ───────────────────────────────────────────────
        // IMPORTANT: DeviceName, PlatformName, PlatformVersion, App, AutomationName
        // are reserved properties — must use typed setters, NOT AddAdditionalAppiumOption()
        var options = new AppiumOptions
        {
            DeviceName      = _deviceName,
            PlatformName    = _platformName,
            PlatformVersion = _platformVersion,
            App             = _app,
            AutomationName  = isIOS ? "XCUITest" : "UiAutomator2"
        };

        // LambdaTest-specific options inside lt:options block (W3C vendor extension)
        options.AddAdditionalAppiumOption("lt:options", new Dictionary<string, object>
        {
            { "username",     LT_USERNAME   },
            { "accessKey",    LT_ACCESS_KEY },
            { "build",        "CSharp NUnit .NET10 Appium" },
            { "name",         $"NUnit - {_deviceName} {_platformName} {_platformVersion}" },
            { "isRealMobile", true  },
            { "deviceName",      _deviceName    },
            { "platformVersion", _platformVersion },
            {"autoGrantPermissions", true},
            {"autoAcceptAlerts", true},
            { "w3c",          true  }
        });

        



        try
        {
            if (isIOS)
                _driver = new IOSDriver(new Uri(HUB_URL), options, TimeSpan.FromSeconds(600));
            else
                _driver = new AndroidDriver(new Uri(HUB_URL), options, TimeSpan.FromSeconds(600));

            Console.WriteLine($"[SetUp] Session created! SessionId: {_driver.SessionId}");
            Console.WriteLine($"[SetUp] Device: {_deviceName} | {_platformName} {_platformVersion}");
        }
        catch (Exception ex)
        {
            // Print full exception so we can see exactly what LambdaTest rejected
            Console.WriteLine("[ERROR] Failed to create Appium session!");
            Console.WriteLine($"[ERROR] Exception type : {ex.GetType().FullName}");
            Console.WriteLine($"[ERROR] Message        : {ex.Message}");
            if (ex.InnerException != null)
                Console.WriteLine($"[ERROR] Inner exception: {ex.InnerException.Message}");
            throw; // Re-throw so NUnit marks test as failed
        }
    }

    [Test]
    public void ProverbialAppTest()
    {
        bool isIOS = _platformName.Equals("iOS", StringComparison.OrdinalIgnoreCase);

        // Helper: wait for element by resource-id (auto-prefixes Android package name)
        AppiumElement WaitForId(string elementId, int sec = 20)
        {
            string fullId = isIOS ? elementId : ANDROID_PKG + elementId;
            Console.WriteLine($"[Test] Looking for: {fullId}");
            return (AppiumElement)new WebDriverWait(_driver, TimeSpan.FromSeconds(sec))
                .Until(SeleniumExtras.WaitHelpers.ExpectedConditions
                    .ElementToBeClickable(MobileBy.Id(fullId)));
        }

        // Helper: Android back button
        void PressBack()
        {
            if (_driver is AndroidDriver ad)
                ad.PressKeyCode(AndroidKeyCode.Back);
        }

        // Step 1: Change text color (tap twice)
        Console.WriteLine("[Test] Step 1 - Color change");
        var colorBtn = WaitForId("color");
        colorBtn.Click();
        System.Threading.Thread.Sleep(1000);
        colorBtn.Click();
        System.Threading.Thread.Sleep(1000);

        // Step 2: Change displayed text
        Console.WriteLine("[Test] Step 2 - Text change");
        WaitForId("Text").Click();
        System.Threading.Thread.Sleep(1000);

        // Step 3: Show Toast message
        Console.WriteLine("[Test] Step 3 - Toast");
        WaitForId("toast").Click();
        System.Threading.Thread.Sleep(1000);

        // Step 4: Trigger system notification
        Console.WriteLine("[Test] Step 4 - Notification");
        WaitForId("notification").Click();
        System.Threading.Thread.Sleep(2000);

        // Step 5: Geolocation screen → back
        Console.WriteLine("[Test] Step 5 - Geolocation");
        WaitForId("geoLocation").Click();
        System.Threading.Thread.Sleep(4000);
        PressBack();
        System.Threading.Thread.Sleep(1000);

        // Step 6: Speed Test screen → back
        Console.WriteLine("[Test] Step 6 - Speed Test");
        WaitForId("speedTest").Click();
        System.Threading.Thread.Sleep(5000);
        PressBack();
        System.Threading.Thread.Sleep(1000);

        // Step 7: In-app Browser → go to lambdatest.com
        Console.WriteLine("[Test] Step 7 - Browser");
        var browserIcon = (AppiumElement)new WebDriverWait(_driver, TimeSpan.FromSeconds(30))
            .Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(
                MobileBy.XPath(
                    "//android.widget.FrameLayout[@content-desc='Browser']" +
                    "/android.widget.FrameLayout/android.widget.ImageView")));
        browserIcon.Click();

        var urlBar = WaitForId("url");
        urlBar.Click();
        urlBar.SendKeys("www.lambdatest.com");
        System.Threading.Thread.Sleep(1000);
        PressBack();
        System.Threading.Thread.Sleep(3000);

        Console.WriteLine("[Test] All 7 steps completed successfully.");
    }

    [TearDown]
    public void Cleanup()
    {
        bool passed = TestContext.CurrentContext.Result.Outcome.Status
                      == NUnit.Framework.Interfaces.TestStatus.Passed;
        try
        {
            if (_driver != null)
            {
                ((IJavaScriptExecutor)_driver)
                    .ExecuteScript("lambda-status=" + (passed ? "passed" : "failed"));
                Console.WriteLine($"[TearDown] Marked as: {(passed ? "PASSED" : "FAILED")}");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"[TearDown] Could not mark status: {ex.Message}");
        }
        finally
        {
            _driver?.Quit();
            Console.WriteLine("[TearDown] Driver session closed.");
        }
    }
}
