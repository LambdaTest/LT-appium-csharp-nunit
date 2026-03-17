# C# NUnit With Appium


<p align="center">
  <a href="https://www.lambdatest.com/blog/?utm_source=github&utm_medium=repo&utm_campaign=LT-appium-CSharp" target="_bank">Blog</a>
  &nbsp; &#8901; &nbsp;
  <a href="https://www.lambdatest.com/support/docs/?utm_source=github&utm_medium=repo&utm_campaign=LT-appium-CSharp" target="_bank">Docs</a>
  &nbsp; &#8901; &nbsp;
  <a href="https://www.lambdatest.com/learning-hub/?utm_source=github&utm_medium=repo&utm_campaign=LT-appium-CSharp" target="_bank">Learning Hub</a>
  &nbsp; &#8901; &nbsp;
  <a href="https://www.lambdatest.com/newsletter/?utm_source=github&utm_medium=repo&utm_campaign=LT-appium-CSharp" target="_bank">Newsletter</a>
  &nbsp; &#8901; &nbsp;
  <a href="https://www.lambdatest.com/certifications/?utm_source=github&utm_medium=repo&utm_campaign=LT-appium-CSharp" target="_bank">Certifications</a>
  &nbsp; &#8901; &nbsp;
  <a href="https://www.youtube.com/c/LambdaTest" target="_bank">YouTube</a>
</p>
&emsp;
&emsp;
&emsp;

*Appium is a tool for automating native, mobile web, and hybrid applications on iOS, Android, and Windows platforms. It supports iOS native apps written in Objective-C or Swift and Android native apps written in Java or Kotlin. It also supports mobile web apps accessed using a mobile browser (Appium supports Safari on iOS and Chrome or the built-in 'Browser' app on Android). Perform Appium automation tests on [LambdaTest's online cloud](https://www.lambdatest.com/appium-mobile-testing).*

*Learn the basics of [Appium testing on the LambdaTest platform](https://www.lambdatest.com/support/docs/getting-started-with-appium-testing/).*

[<img height="53" width="200" src="https://user-images.githubusercontent.com/70570645/171866795-52c11b49-0728-4229-b073-4b704209ddde.png">](https://accounts.lambdatest.com/register)

## Table of Contents

* [Pre-requisites](#pre-requisites)
* [Run Your First Test](#run-your-first-test)
* [Executing The Tests](#executing-the-tests)

## Pre-requisites

Before you can start performing App automation testing with Appium, you need to set up the **.NET 10 SDK**. Optionally you can also use Visual Studio:

<p align="center">
<img height="500" src="https://user-images.githubusercontent.com/109070745/180055052-c0761088-eaa1-48f3-abac-521ca8c3458b.png">
</p>

- Install **.NET 10 SDK** from: https://dotnet.microsoft.com/download/dotnet/10.0

- Clone/Download the Github Repository.

- Open the `NUnitAppium.sln` solution file in Visual Studio (optional — tests can also be run directly from the command line using `run-tests.bat` on Windows or `run-tests.sh` on macOS/Linux).

> **Note:** Visual Studio is **not required**. The `.NET 10 SDK` alone is enough to build and run tests from the command line.

### Setting Up Your Authentication

Make sure you have your LambdaTest credentials with you to run test automation scripts on LambdaTest. To obtain your access credentials, [purchase a plan](https://billing.lambdatest.com/billing/plans) or access the [Automation Dashboard](https://appautomation.lambdatest.com/).

Set LambdaTest `Username` and `Access Key` in environment variables.

**For Linux/macOS:**

```bash
export LT_USERNAME=YOUR_LAMBDATEST_USERNAME
export LT_ACCESS_KEY=YOUR_LAMBDATEST_ACCESS_KEY
```

**For Windows:**

```cmd
set LT_USERNAME=YOUR_LAMBDATEST_USERNAME
set LT_ACCESS_KEY=YOUR_LAMBDATEST_ACCESS_KEY
```

### Upload Your Application

Upload your **_iOS_** application (.ipa file) or **_android_** application (.apk file) to the LambdaTest servers using our **REST API**. You need to provide your **Username** and **AccessKey** in the format `Username:AccessKey` in the **cURL** command for authentication. Make sure to add the path of the **appFile** in the cURL request. Here is an example cURL request to upload your app using our REST API:

**Using App File:**

**For Linux/macOS:**

```bash
curl -u "YOUR_LAMBDATEST_USERNAME:YOUR_LAMBDATEST_ACCESS_KEY" \
--location --request POST 'https://manual-api.lambdatest.com/app/upload/realDevice' \
--form 'name="Android_App"' \
--form 'appFile=@"/Users/macuser/Downloads/proverbial_android.apk"'
```

**For Windows:**

```cmd
curl -u "YOUR_LAMBDATEST_USERNAME:YOUR_LAMBDATEST_ACCESS_KEY" ^
  -X POST "https://manual-api.lambdatest.com/app/upload/realDevice" ^
  -F "appFile=@C:\Users\Downloads\proverbial_android.apk"
```

**Using App URL:**

**For Linux/macOS:**

```bash
curl -u "YOUR_LAMBDATEST_USERNAME:YOUR_LAMBDATEST_ACCESS_KEY" \
--location --request POST 'https://manual-api.lambdatest.com/app/upload/realDevice' \
--form 'name="Android_App"' \
--form 'url="https://prod-mobile-artefacts.lambdatest.com/assets/docs/proverbial_android.apk"'
```

**For Windows:**

```cmd
curl -u "YOUR_LAMBDATEST_USERNAME:YOUR_LAMBDATEST_ACCESS_KEY" ^
  -X POST "https://manual-api.lambdatest.com/app/upload/realDevice" ^
  -d "{\"url\":\"https://prod-mobile-artefacts.lambdatest.com/assets/docs/proverbial_android.apk\",\"name\":\"sample.apk\"}"
```

**Tip:**

- If you do not have any **.apk** or **.ipa** file, you can run your sample tests on LambdaTest by using our sample :link: [Android app](https://prod-mobile-artefacts.lambdatest.com/assets/docs/proverbial_android.apk) or sample :link: [iOS app](https://prod-mobile-artefacts.lambdatest.com/assets/docs/proverbial_ios.ipa).
- Response of above cURL will be a **JSON** object containing the `App URL` of the format - `lt://APP123456789123456789` and will be used in the next step.

## Run Your First Test

**Test Scenario:** Check out [NUnitAppiumTests.cs](https://github.com/LambdaTest/LT-appium-csharp-nunit/blob/master/NUnitAppium/NUnitAppiumTests.cs) file to view the sample test script.

### Configuring Your Test Capabilities

Open `NUnitAppium/NUnitAppiumTests.cs` and update the `[TestFixture]` lines at the top to select your target device and app URL:

```csharp
// Each line = one device. Comment/uncomment to control what runs.
// To run ONLY Android : keep Android line, comment out iOS line
// To run ONLY iOS     : keep iOS line, comment out Android line
// To run BOTH         : keep both lines (they run in parallel)

[TestFixture("Galaxy S24", "14", "Android", "lt://APP_URL")]  // Android
[TestFixture("iPhone 15",  "17", "iOS",     "lt://APP_URL")]  // iOS
```

The capabilities are configured using the modern **W3C AppiumOptions** format. `deviceName`, `platformVersion` and `app` are passed inside `lt:options` to guarantee LambdaTest always allocates the exact requested device:

```csharp
var options = new AppiumOptions
{
    PlatformName   = platformName,                          // "Android" or "iOS"
    AutomationName = isIOS ? "XCUITest" : "UiAutomator2"   // automation engine
};

options.AddAdditionalAppiumOption("lt:options", new Dictionary<string, object>
{
    { "username",        "LT_USERNAME"    },   // LambdaTest username (from env variable)
    { "accessKey",       "LT_ACCESS_KEY"  },   // LambdaTest access key (from env variable)
    { "deviceName",      "Galaxy S24"     },   // exact device — inside lt:options for guaranteed allocation
    { "platformVersion", "14"             },   // OS version
    { "app",             "lt://APP_URL"   },   // app URL from upload step
    { "build",           "CSharp NUnit"   },
    { "name",            "NUnit Test"     },
    { "isRealMobile",    true             },
    { "w3c",             true             }
});

// Appium-level capabilities
options.AddAdditionalAppiumOption("autoGrantPermissions", true);  // Android: auto grant runtime permissions
options.AddAdditionalAppiumOption("autoAcceptAlerts",     true);  // iOS: auto accept system alert popups
```

**Info Note:**

- You must add the generated **APP_URL** to the `"app"` key inside `lt:options`.
- `deviceName`, `platformVersion` and `app` are passed inside `lt:options` — **not** at the Appium level. Passing `deviceName` only at the Appium level can result in a random device being allocated instead of the requested one.
- `autoGrantPermissions` automatically handles runtime permission dialogs on Android (camera, location etc.) so you do not need to handle them in your test steps.
- `autoAcceptAlerts` automatically taps Allow on iOS system alert popups (notifications, location etc.).
- You can generate capabilities for your test requirements with the help of our inbuilt **[Capabilities Generator tool](https://www.lambdatest.com/capabilities-generator/)**. A more detailed Capability Guide is available [here](https://www.lambdatest.com/support/docs/desired-capabilities-in-appium/).

## Executing The Tests

### Option 1 — One-Click Script (Recommended)

**Windows** — Open Command Prompt and run:

```cmd
set LT_USERNAME=YOUR_LAMBDATEST_USERNAME
set LT_ACCESS_KEY=YOUR_LAMBDATEST_ACCESS_KEY
run-tests.bat
```

**macOS / Linux** — Open Terminal and run:

```bash
export LT_USERNAME=YOUR_LAMBDATEST_USERNAME
export LT_ACCESS_KEY=YOUR_LAMBDATEST_ACCESS_KEY
bash run-tests.sh
```

The script automatically restores NuGet packages, builds the solution in Release mode, and runs the tests.

### Option 2 — dotnet CLI

```bash
dotnet build NUnitAppium.sln --configuration Release
dotnet test NUnitAppium.sln --configuration Release --no-build
```

### Option 3 — Visual Studio

Go to **Build** menu and click **Build Solution**. After the solution is built, navigate to the **Test** menu and click **Run All Tests**.

**Info:** Your test results will be displayed on the test console (or command-line interface if you are using terminal/cmd) and on the :link: [LambdaTest App Automation Dashboard](https://appautomation.lambdatest.com/build).

## Additional Links

- [How to test locally hosted apps](https://www.lambdatest.com/support/docs/testing-locally-hosted-pages/)
- [How to integrate LambdaTest with CI/CD](https://www.lambdatest.com/support/docs/integrations-with-ci-cd-tools/)

## Documentation & Resources :books:
      
Visit the following links to learn more about LambdaTest's features, setup and tutorials around test automation, mobile app testing, responsive testing, and manual testing.

* [LambdaTest Documentation](https://www.lambdatest.com/support/docs/?utm_source=github&utm_medium=repo&utm_campaign=LT-appium-CSharp)
* [LambdaTest Blog](https://www.lambdatest.com/blog/?utm_source=github&utm_medium=repo&utm_campaign=LT-appium-CSharp)
* [LambdaTest Learning Hub](https://www.lambdatest.com/learning-hub/?utm_source=github&utm_medium=repo&utm_campaign=LT-appium-CSharp)    

## LambdaTest Community :busts_in_silhouette:

The [LambdaTest Community](https://community.lambdatest.com/) allows people to interact with tech enthusiasts. Connect, ask questions, and learn from tech-savvy people. Discuss best practises in web development, testing, and DevOps with professionals from across the globe 🌎

## What's New At LambdaTest ❓

To stay updated with the latest features and product add-ons, visit [Changelog](https://changelog.lambdatest.com/) 
      
## About LambdaTest

[LambdaTest](https://www.lambdatest.com/?utm_source=github&utm_medium=repo&utm_campaign=LT-appium-CSharp) is a leading test execution and orchestration platform that is fast, reliable, scalable, and secure. It allows users to run both manual and automated testing of web and mobile apps across 3000+ different browsers, operating systems, and real device combinations. Using LambdaTest, businesses can ensure quicker developer feedback and hence achieve faster go to market. Over 500 enterprises and 1 Million + users across 130+ countries rely on LambdaTest for their testing needs.    

### Features

* Run Selenium, Cypress, Puppeteer, Playwright, and Appium automation tests across 3000+ real desktop and mobile environments.
* Real-time cross browser testing on 3000+ environments.
* Test on Real device cloud
* Blazing fast test automation with HyperExecute
* Accelerate testing, shorten job times and get faster feedback on code changes with Test At Scale.
* Smart Visual Regression Testing on cloud
* 120+ third-party integrations with your favorite tool for CI/CD, Project Management, Codeless Automation, and more.
* Automated Screenshot testing across multiple browsers in a single click.
* Local testing of web and mobile apps.
* Online Accessibility Testing across 3000+ desktop and mobile browsers, browser versions, and operating systems.
* Geolocation testing of web and mobile apps across 53+ countries.
* LT Browser - for responsive testing across 50+ pre-installed mobile, tablets, desktop, and laptop viewports
    
[<img height="53" width="200" src="https://user-images.githubusercontent.com/70570645/171866795-52c11b49-0728-4209ddde.png">](https://accounts.lambdatest.com/register)

      
## We are here to help you :headphones:

* Got a query? we are available 24x7 to help. [Contact Us](support@lambdatest.com)
* For more info, visit - [LambdaTest](https://www.lambdatest.com/?utm_source=github&utm_medium=repo&utm_campaign=LT-appium-CSharp)
