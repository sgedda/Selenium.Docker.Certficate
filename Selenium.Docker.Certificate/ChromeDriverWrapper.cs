using System;
using System.IO;
using System.Reflection;
using Microsoft.AspNetCore.WebUtilities;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;

namespace Selenium.Docker.Certificate
{
    public class ChromeDriverWrapper
    {
        public ChromeDriverWrapper()
        {
            var options = new ChromeOptions();

            //options.AddArgument("--headless");
            options.AddArgument("--disable-extensions");
            options.AddArgument("--disable-gpu");
            options.AddArgument("--no-sandbox");
            options.AddArgument("--browsertime.chrome.args no-sandbox");
            options.AddArgument("--disable-dev-shm-usage");
            options.AddArgument("--ignore-certificate-errors");
            options.AddArgument("--allow-running-insecure-content");
            options.AddArgument("--allow-insecure-localhost");
            options.AddArgument("--unsafely -treat-insecure-origin-as-secure");
            options.AddArgument("--unsafely-treat-insecure-origin-as-secure");
            options.AddArgument("--ignore-urlfetcher-cert-requests");
            options.AddArgument("--shm-size=2g");
            options.AddArgument("--privileged");
            options.AddArgument("--verbose");
            using (var chromeDriver = new ChromeDriver(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), options))
            {
                WebDriverWait wait = new WebDriverWait(chromeDriver, new TimeSpan(0, 0, 10));
                string url = $"https://auth-url.com/oauth2/v1/org/authorize?client_id=YOUR-CLIENT-ID&response_type=code&state=SOME-STATE&redirect_uri=https://localhost:3001&scope=scope";
                try
                {
                    chromeDriver.Navigate().GoToUrl(url);
                    wait.Until(x => x.Url != url);
                }
                catch (Exception e)
                {
                    Screenshot ss = ((ITakesScreenshot)chromeDriver).GetScreenshot();
                    ss.SaveAsFile(@"/seleniumTestingScreenshot.jpg", ScreenshotImageFormat.Png);
                    Console.WriteLine("screenshot taken");
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine(e.Message);
                    Console.WriteLine(e.StackTrace);
                    Console.ResetColor();
                    //throw;
                }

                Console.WriteLine(chromeDriver.Url);
                if (QueryHelpers.ParseQuery(new Uri(chromeDriver.Url).Query).TryGetValue("code", out var code))
                {
                    Console.WriteLine("code successfully retrieved:");
                    Console.WriteLine(code);
                }
            }
        }
    }
}
