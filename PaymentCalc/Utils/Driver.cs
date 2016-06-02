
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
namespace PaymentCalc
{
    //Singlton instance
    public class Driver
    {
        public static IWebDriver Instance { get; set; }

        public static string MainPage
        {
            get
            {
                return "https://ia.ca/individuals";
            }
        }
        //Refactor: for each browser create own Initialize method(InitializeFireFox, InitializeChtome, etc)
        public static void Initialize()
        {
            Instance = new ChromeDriver();
            Instance.Manage().Window.Maximize();
        }

        public static void Wait()
        {
            Instance.Manage().Timeouts().ImplicitlyWait(TimeSpan.FromSeconds(5));
        }

        public static void Close()
        {
           // Instance.Close();
        }
    }
}
