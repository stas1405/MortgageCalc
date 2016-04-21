
using OpenQA.Selenium.Chrome;
using System;
namespace PaymentCalc
{
    public class Driver
    {
        public static ChromeDriver Instance { get; set; }

        public static string MainPage
        {
            get
            {
                return "https://ia.ca/individuals";
            }
        }

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
