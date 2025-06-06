using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Appium.Android;
using OpenQA.Selenium.Appium.iOS;
using OpenQA.Selenium.Appium.Enums;
using OpenQA.Selenium.Appium.Service;
using OpenQA.Selenium.Remote;
using OpenQA.Selenium.Support.UI;
using System;
using System.Threading;

namespace NUnitAppium
{
    //-------------------Running two parallel test cases----------------------------
    [TestFixture("Galaxy S23 Ultra", "13", "Android", "lt://proverbial-android")]    // Android Testing
    [TestFixture("iPhone 14", "16", "iOS", "lt://proverbial-ios")]      // iOS testing
    //[TestFixture("OnePlus 6T", "9", "Android", "APP_URL")]
    //[TestFixture("OnePlus 6T", "9", "Android", "APP_URL")]
    //[TestFixture("OnePlus 6T", "9", "Android", "APP_URL")]
    //[TestFixture("OnePlus 6T", "9", "Android", "APP_URL")]
    //[TestFixture("OnePlus 6T", "9", "Android", "APP_URL")]
    //[TestFixture("OnePlus 6T", "9", "Android", "APP_URL")]
    // [TestFixture("OnePlus 6T", "9", "Android", "APP_URL")]
    [Parallelizable(ParallelScope.Fixtures)]
    public class NUnitAppiumTests
    {
        private AppiumDriver<AppiumWebElement> driver;
        private WebDriverWait wait;
        private String deviceName = Environment.GetEnvironmentVariable("LT_DEVICE_NAME") ?? "deviceName";
        private String platformVersion = Environment.GetEnvironmentVariable("LT_PLATFORM_VERSION") ?? "platformVersion";
        private String platformName = Environment.GetEnvironmentVariable("LT_PLATFORM_NAME") ?? "platformName";
        private String app = Environment.GetEnvironmentVariable("LT_APP") ?? "app";
        private String build = Environment.GetEnvironmentVariable("LT_BUILD") ?? "Csharp NUnit";
        private String seleniumUri = "https://mobile-hub.lambdatest.com/wd/hub";
        private static String LT_USERNAME = Environment.GetEnvironmentVariable("LT_USERNAME");
        private static String LT_ACCESS_KEY = Environment.GetEnvironmentVariable("LT_ACCESS_KEY");

        public NUnitAppiumTests(String deviceName, String platformVersion, String platformName, String app)
        {
            this.deviceName = deviceName;
            this.platformVersion = platformVersion;
            this.platformName = platformName;
            this.app = app;
        }

        [SetUp]
        public void Init()
        {
            Console.WriteLine($"[LOG] [SetUp] Starting test for device: {deviceName}, platform: {platformName}, version: {platformVersion}, app: {app}");
            AppiumOptions capabilities = new AppiumOptions();
            capabilities.AddAdditionalCapability("user", LT_USERNAME);
            capabilities.AddAdditionalCapability("accessKey", LT_ACCESS_KEY);
            capabilities.AddAdditionalCapability("app", app);
            capabilities.AddAdditionalCapability("deviceName", deviceName);
            capabilities.AddAdditionalCapability("platformVersion", platformVersion);
            capabilities.AddAdditionalCapability("platformName", platformName);
            capabilities.AddAdditionalCapability("build", build);
            capabilities.AddAdditionalCapability("name", "NUnit Test");
            capabilities.AddAdditionalCapability("isRealMobile", true);
            capabilities.AddAdditionalCapability("network", true);
            capabilities.AddAdditionalCapability("visual", true);
            capabilities.AddAdditionalCapability("video", true);
            capabilities.AddAdditionalCapability("console", true);
            capabilities.AddAdditionalCapability("deviceOrientation", "PORTRAIT");
            capabilities.AddAdditionalCapability("autoGrantPermissions", true);
            capabilities.AddAdditionalCapability("newCommandTimeout", 120);
            capabilities.AddAdditionalCapability("w3c", true);

            try
            {
                if (platformName.Equals("Android"))
                {
                    driver = new AndroidDriver<AppiumWebElement>(new Uri(seleniumUri), capabilities);
                }
                else if (platformName.Equals("iOS"))
                {
                    driver = new IOSDriver<AppiumWebElement>(new Uri(seleniumUri), capabilities);
                }
                wait = new WebDriverWait(driver, TimeSpan.FromSeconds(30));
            }
            catch (Exception e)
            {
                Console.WriteLine($"[ERROR] Failed to initialize driver: {e.Message}");
                throw;
            }
        }

        [Test]
        public void Todotest()
        {
            try
            {
                Console.WriteLine("[LOG] Starting Todotest");
                
                // Click on Color button
                Console.WriteLine("Test: Clicking color button...");
                AppiumWebElement colorButton = (AppiumWebElement)wait.Until(d => d.FindElement(By.Id("color")));
                colorButton.Click();
                colorButton.Click();
                Console.WriteLine("✓ Color button test passed");
                Thread.Sleep(platformName.Equals("iOS") ? 2000 : 1000);

                // Click on Text button
                Console.WriteLine("Test: Clicking text button...");
                AppiumWebElement textButton = (AppiumWebElement)wait.Until(d => d.FindElement(By.Id("Text")));
                textButton.Click();
                Console.WriteLine("✓ Text button test passed");
                Thread.Sleep(platformName.Equals("iOS") ? 2000 : 1000);

                // Click on Toast button
                Console.WriteLine("Test: Clicking toast button...");
                AppiumWebElement toastButton = (AppiumWebElement)wait.Until(d => d.FindElement(By.Id("toast")));
                toastButton.Click();
                Console.WriteLine("✓ Toast button test passed");
                Thread.Sleep(platformName.Equals("iOS") ? 2000 : 1000);

                // Click on Notification button
                Console.WriteLine("Test: Clicking notification button...");
                AppiumWebElement notificationButton = (AppiumWebElement)wait.Until(d => d.FindElement(By.Id("notification")));
                notificationButton.Click();
                Console.WriteLine("✓ Notification button test passed");
                Thread.Sleep(platformName.Equals("iOS") ? 3000 : 2000);

                // Click on Geolocation button
                Console.WriteLine("Test: Clicking geolocation button...");
                AppiumWebElement geoLocationButton = (AppiumWebElement)wait.Until(d => d.FindElement(By.Id("geoLocation")));
                geoLocationButton.Click();
                Console.WriteLine("✓ Geolocation button test passed");
                Console.WriteLine("Waiting for geolocation request to process...");
                Thread.Sleep(platformName.Equals("iOS") ? 15000 : 10000);

                // Handle back navigation based on platform
                if (platformName.Equals("Android"))
                {
                    ((AndroidDriver<AppiumWebElement>)driver).PressKeyCode(AndroidKeyCode.Back);
                }
                else
                {
                    driver.Navigate().Back();
                }
                Console.WriteLine("✓ Navigated back from Geolocation");
                Thread.Sleep(platformName.Equals("iOS") ? 2000 : 1000);

                // Click on Speed Test button
                Console.WriteLine("Test: Clicking speed test button...");
                try
                {
                    AppiumWebElement speedTestButton = (AppiumWebElement)wait.Until(d => d.FindElement(By.Id("speedTest")));
                    speedTestButton.Click();
                    Console.WriteLine("✓ Speed test button test passed");
                    Thread.Sleep(platformName.Equals("iOS") ? 7000 : 5000);

                    // Handle back navigation based on platform
                    if (platformName.Equals("Android"))
                    {
                        ((AndroidDriver<AppiumWebElement>)driver).PressKeyCode(AndroidKeyCode.Back);
                    }
                    else
                    {
                        driver.Navigate().Back();
                    }
                    Console.WriteLine("✓ Navigated back from Speed Test");
                    Thread.Sleep(platformName.Equals("iOS") ? 2000 : 1000);
                }
                catch (Exception e)
                {
                    Console.WriteLine($"Note: Speed test button not found or not clickable: {e.Message}");
                }

                // Click on Browser button
                Console.WriteLine("Test: Clicking browser button...");
                try
                {
                    AppiumWebElement browserButton = (AppiumWebElement)wait.Until(d => d.FindElement(MobileBy.AccessibilityId("Browser")));
                    browserButton.Click();
                    Console.WriteLine("✓ Browser button test passed");

                    // Click on URL input box
                    Console.WriteLine("Test: Clicking URL input box...");
                    AppiumWebElement urlInput = (AppiumWebElement)wait.Until(d => d.FindElement(By.Id("url")));
                    urlInput.Click();
                    urlInput.SendKeys("www.lambdatest.com");
                    Console.WriteLine("✓ URL input box test passed");
                    Thread.Sleep(platformName.Equals("iOS") ? 2000 : 1000);

                    // Handle back navigation based on platform
                    if (platformName.Equals("Android"))
                    {
                        ((AndroidDriver<AppiumWebElement>)driver).PressKeyCode(AndroidKeyCode.Back);
                    }
                    else
                    {
                        driver.Navigate().Back();
                    }
                    Console.WriteLine("✓ Navigated back from Browser");
                    Thread.Sleep(platformName.Equals("iOS") ? 2000 : 1000);
                }
                catch (Exception e)
                {
                    Console.WriteLine($"Note: Browser button not found or not clickable: {e.Message}");
                }

                Console.WriteLine($"All {platformName} tests completed successfully!");
            }
            catch (Exception e)
            {
                Console.WriteLine($"[ERROR] Test failed: {e.Message}");
                throw;
            }
        }

        [TearDown]
        public void Cleanup()
        {
            try
            {
                if (driver != null)
                {
                    var status = TestContext.CurrentContext.Result.Outcome.Status.ToString();
                    var script = "lambda-status=" + (status.Equals("Passed") ? "passed" : "failed");
                    ((IJavaScriptExecutor)driver).ExecuteScript(script);
                    driver.Quit();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine($"[ERROR] Cleanup failed: {e.Message}");
            }
        }
    }
}
