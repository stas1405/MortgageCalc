using NUnit.Framework;
using PaymentCalc.Pages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaymentCalc.Tests
{
    public class BaseTestClassForCalc:BaseTestClass
    {
        [SetUp]
        public void Load()
        {
            MortgagePage.GoTo();
            // Refactor: CalcPage.GoTo() instead of MortgagePage.NavigateToPaymentCalculator();
            MortgagePage.NavigateToPaymentCalculator();

        }
    }
}
