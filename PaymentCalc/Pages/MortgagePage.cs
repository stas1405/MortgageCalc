using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using PaymentCalc.Navigation;

namespace PaymentCalc.Pages
{
    public class MortgagePage
    {
        public static void GoTo()
        {
            TopNavigation.Loans.Mortgages.Select();
        }

        public static void NavigateToPaymentCalculator()
        {
            var calcBtn = Driver.Instance.FindElement(By.LinkText("Calculate your payments"));
            Actions action = new Actions(Driver.Instance);
            action.MoveToElement(calcBtn);
            action.Click();
            action.Perform();
        }
    }
}
