using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Remote;

using NUnit.Framework;
using System.Threading;
using System.Collections.Generic;
using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Appium.Android;
using OpenQA.Selenium.Appium.iOS;
using OpenQA.Selenium.Support.UI;

namespace NUnitSelenium
{
    //-------------------Running two parallel test cases----------------------------
    [TestFixture("OnePlus 6", "8", "Android", "APP_URL")]    // Android Testing 
    [TestFixture("iPhone 11", "14", "iOS", "APP_URL")]      // iOS testing
    //[TestFixture("OnePlus 6T", "9", "Android", "APP_URL")]
    //[TestFixture("OnePlus 6T", "9", "Android", "APP_URL")]
    //[TestFixture("OnePlus 6T", "9", "Android", "APP_URL")]
    //[TestFixture("OnePlus 6T", "9", "Android", "APP_URL")]
    //[TestFixture("OnePlus 6T", "9", "Android", "APP_URL")]
    //[TestFixture("OnePlus 6T", "9", "Android", "APP_URL")]
    // [TestFixture("OnePlus 6T", "9", "Android", "APP_URL")]
    [Parallelizable(ParallelScope.Fixtures)]
    public class NUnitSeleniumSample
    {
        //--------------------We can intialize username and access Key with hub url to authenticate our test script-------------------------------------
        public static string LT_USERNAME = Environment.GetEnvironmentVariable("LT_USERNAME") ==null ? "your username" : Environment.GetEnvironmentVariable("LT_USERNAME");
        public static string LT_ACCESS_KEY = Environment.GetEnvironmentVariable("LT_ACCESS_KEY") == null ? "your accessKey" : Environment.GetEnvironmentVariable("LT_ACCESS_KEY");
        public static bool tunnel = Boolean.Parse(Environment.GetEnvironmentVariable("LT_TUNNEL")== null ? "false" : Environment.GetEnvironmentVariable("LT_TUNNEL"));       
        public static string build = Environment.GetEnvironmentVariable("LT_BUILD") == null ? "your build name" : Environment.GetEnvironmentVariable("LT_BUILD");
        public static string seleniumUri = "https://mobile-hub.lambdatest.com:443/wd/hub";




        //-------------------------Initialization of Driver--------------------------
        AndroidDriver<AndroidElement> driver;

        // Initialization some parameter 
        private String deviceName;
        private String platformVersion;
        private String platformName;
        private String app;

       

        public NUnitSeleniumSample(String deviceName, String platformVersion, String platformName, String app)
        {
            this.deviceName = deviceName;
            this.platformVersion = platformVersion;
            this.platformName = platformName;
            this.app = app;
        }

        [SetUp]
        public void Init()
        {
            //-----------------------------------Create instance for passing capabilities-----------------------------------------------------------------
            AppiumOptions capabilities = new AppiumOptions();
            capabilities.AddAdditionalCapability("user", "LT_USERNAME");   //Add LambdaTest username here
            capabilities.AddAdditionalCapability("accessKey", "LT_ACCESS_KEY");   //Add LambdaTest accessKey here
            capabilities.AddAdditionalCapability("app",app);
            capabilities.AddAdditionalCapability("deviceName", deviceName);
            capabilities.AddAdditionalCapability("platformVersion", platformVersion);
            capabilities.AddAdditionalCapability("platformName", platformName);
            capabilities.AddAdditionalCapability("build", "Csharp NUnit");
            capabilities.AddAdditionalCapability("name", "NUnit Test");
            capabilities.AddAdditionalCapability("isRealMobile", true);

            
                 driver = new AndroidDriver<AndroidElement> (new Uri(seleniumUri), capabilities, TimeSpan.FromSeconds(600));
            
           // Console.Out.WriteLine(driver);
            Console.Out.WriteLine("On Which Device/Paltform test is running:"+deviceName+" "+platformVersion+" "+platformName);
            
        }

        [Test]
        public void Todotest()
        {
            {
                //----------------------Text Color Changes---------------------------------
                Console.WriteLine("1.Text Color Changes");
                AndroidElement searchElement = (AndroidElement)new WebDriverWait(
                 driver, TimeSpan.FromSeconds(20)).Until(
                 SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(
                    MobileBy.Id("color"))
             );
                System.Threading.Thread.Sleep(1000);
                searchElement.Click();
                System.Threading.Thread.Sleep(1000);
                searchElement.Click();

                System.Threading.Thread.Sleep(1000);

                //----------------------Text Changes by clicking a button---------------------------------
                Console.WriteLine("2.Text Changes by clicking a button");

                AndroidElement changeelement = (AndroidElement)new WebDriverWait(
                 driver, TimeSpan.FromSeconds(10)).Until(
                 SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(
                    MobileBy.Id("Text"))
             );
                changeelement.Click();

                System.Threading.Thread.Sleep(1000);

                //----------------------Toast---------------------------------
                Console.WriteLine("3.Toast");

                AndroidElement toast = (AndroidElement)new WebDriverWait(
                 driver, TimeSpan.FromSeconds(10)).Until(
                 SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(
                    MobileBy.Id("toast"))
             );
                toast.Click();

                System.Threading.Thread.Sleep(1000);

                //----------------------Notification By clicking a button---------------------------------
                Console.WriteLine("4.Notification Button clicked");

                AndroidElement Notification = (AndroidElement)new WebDriverWait(
                 driver, TimeSpan.FromSeconds(10)).Until(
                 SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(
                    MobileBy.Id("notification"))
             );
                Notification.Click();
                System.Threading.Thread.Sleep(2000);

                //----------------------Geolocation button---------------------------------
                Console.WriteLine("5.Geolocation");

                AndroidElement geolocation = (AndroidElement)new WebDriverWait(
                 driver, TimeSpan.FromSeconds(10)).Until(
                 SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(
                    MobileBy.Id("geoLocation"))
             );
                geolocation.Click();
                System.Threading.Thread.Sleep(4000);
                driver.PressKeyCode(AndroidKeyCode.Back);
                System.Threading.Thread.Sleep(1000);



                //----------------------Speed Test Button---------------------------------
                Console.WriteLine("6.Speed Test Button Clicked");

                AndroidElement speed = (AndroidElement)new WebDriverWait(
                 driver, TimeSpan.FromSeconds(10)).Until(
                 SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(
                    MobileBy.Id("speedTest"))
             );
                speed.Click();
                System.Threading.Thread.Sleep(5000);
                driver.PressKeyCode(AndroidKeyCode.Back);
                System.Threading.Thread.Sleep(1000);


                //----------------------Browser Button---------------------------------
                //   Console.WriteLine("Browser Button Clicked");

                AndroidElement BROWSER = (AndroidElement)new WebDriverWait(
                  driver, TimeSpan.FromSeconds(30)).Until(
                  SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(
                     MobileBy.XPath("//android.widget.FrameLayout[@content-desc=\"Browser\"]/android.widget.FrameLayout/android.widget.ImageView"))
                  
              );
                BROWSER.Click();


                AndroidElement url = (AndroidElement)new WebDriverWait(
                driver, TimeSpan.FromSeconds(10)).Until(
                SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(
                   MobileBy.Id("url"))
            );

                //String command = "new UiScrollable(new UiSelector().scrollable(true).instance(0)).scrollIntoView("
                //          + url + ");";

                //driver.FindElementByAndroidUIAutomator(command);


                url.Click();

                url.SendKeys("www.lambdatest.com");

                System.Threading.Thread.Sleep(1000);

                driver.PressKeyCode(AndroidKeyCode.Back);

                System.Threading.Thread.Sleep(3000);






            }
        }

        [TearDown]
        public void Cleanup()

        {
            
            bool passed = TestContext.CurrentContext.Result.Outcome.Status == NUnit.Framework.Interfaces.TestStatus.Passed;
            try
            {
                //-----------------Marking Test status passed or failed -----------------------------------------
                ((IJavaScriptExecutor)driver).ExecuteScript("lambda-status=" + (passed ? "passed" : "failed"));
               
            }
            finally
            {
                //---------------------Quit the session-----------------------
                
                driver.Quit();
            }

            

         






        }
    }
}
