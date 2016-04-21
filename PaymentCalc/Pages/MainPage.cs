using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaymentCalc
{
    public class MainPage
    {
        public static void GoTo()
        {
           Driver.Instance.Navigate().GoToUrl(Driver.MainPage);
         //   var alert = Driver.Instance.FindElement(By.ClassName("icone-tuile-x"));
         //   alert.Click();
        }

        public static string CheckLanguage()
        {
           var lang =  Driver.Instance.FindElement(By.Id("topLangMenuItem"));
            if (lang.Text == "FR")
            {
                return "english";
            }
            else
                return "french";
        }

        public static void ChangeLanguage()
        {
            Driver.Instance.FindElement(By.Id("topLangMenuItem")).Click();
            Driver.Wait();
        }
    }

    //public class MainPageNavigation
    //{
    //    public 
    //}

    //public class CalcByMortgage
    //{
    //    private string mortgageAmount;
    //    private string rate;
    //    private string amortization;
    //    private string freq;
    //    public CalcByMortgage()
    //    {

    //    }

    //    public CalcByMortgage WithMortgageAmount(string mortgageAmount)
    //    {
    //        this.mortgageAmount = mortgageAmount;
    //        return this;
    //    }

    //    public CalcByMortgage WithRate(string rate)
    //    {
    //        this.rate = rate;
    //        return this;
    //    }

    //    public CalcByMortgage WithAmortization(string amortization)
    //    {
    //        this.amortization = amortization;
    //        return this;
    //    }

    //    public CalcByMortgage WithFrequency(string freq)
    //    {
    //        this.freq = freq;
    //        return this;
    //    }

    }

