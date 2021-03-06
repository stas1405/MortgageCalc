﻿using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaymentCalc.Navigation
{
    public class MenuSelector
    {
        public static void Select(string topLevelMenu, string subMenuLink)
        {
            Driver.Instance.FindElement(By.CssSelector("a[data-utag-name= " + topLevelMenu + "]")).Click();
            Driver.Instance.FindElement(By.CssSelector("a[data-utag-name= " + subMenuLink + "]")).Click();
        }
    }
}
