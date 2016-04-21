using NUnit.Framework;
using PaymentCalc.Pages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaymentCalc.Tests
{
    [TestFixture]
    public class TestCalculator:BaseClassForCalc
    {
        [Test]

        public void ShouldTestByPriceAmount()
        {
            CalcPage.MoveSlider(100);
            Assert.That(CalcPage.CheckPurchasePrice(), Is.Not.EqualTo(0));
            CalcPage.CalcByPrice().SetPurchasePriceWithBtn("500000")
                .SetDownPaymentWithBtn("50000")
                .WithAmortization("15 years")
                .WithFrequency("weekly")
                .WithRate("5")
                .Submit();
            Assert.That(CalcPage.MonthlyPaymentResultDisplayed(), Is.True);
   
            Assert.That(CalcPage.MonthlyPaymentResult(), Is.EqualTo("$ 836.75"));

            CalcPage.CalcByPrice().SetPurchasePriceWithBtn("500000")
                .SetDownPaymentWithBtn("50000")
                .WithAmortization("15 years")
                .WithFrequency("weekly")
                .WithRate("3")
                .Submit();
            Assert.That(CalcPage.MonthlyPaymentResult(), Is.EqualTo("$ 732.70"));

        }

        [Test]
        public void ShouldTestByMortgageAmount()
        {
            //MortgagePage.GoTo();
            //MortgagePage.NavigateToPaymentCalculator();
            CalcPage.CalcByMortgage().WithMortgageAmount("444444")
                .WithRate("3,54")
                .WithAmortization("15 years")
                .WithFrequency("Biweekly")
                .Submit();
            Assert.That(CalcPage.MonthlyPaymentResultDisplayed(), Is.True);
        }

        [Test]

        public void ShoudParsePDFforResults()
        {
            CalcPage.CalcByMortgage().WithMortgageAmount("444444")
               .WithRate("3,54")
               .WithAmortization("15 years")
               .WithFrequency("Biweekly")
               .Submit();
            CalcPage.DownloadPDFwithResults();
            Assert.That(CalcPage.ParsePdf(), Is.EqualTo(CalcPage.MonthlyPaymentResult()));
        }
        
    }
}
