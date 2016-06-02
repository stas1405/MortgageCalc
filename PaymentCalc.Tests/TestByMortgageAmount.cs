using NUnit.Framework;

namespace PaymentCalc.Tests
{
    [TestFixture]
    public class TestCalculator:BaseTestClassForCalc
    {
        [Test]

        public void ShouldTestByPriceAmount()
        {
            CalcPage.MoveSlider(100);
            Assert.That(CalcPage.CheckPurchasedPrice(), Is.Not.EqualTo(0));
            //Refactor: for each nav element(button, slider or field) create separate class CalcByPriceWith...? Or split class
            
            CalcPage.CalcByPrice().SetPurchasePriceWithBtn("500000") //FLUENT interface with object commands
                .SetDownPaymentWithBtn("50000")
                .WithAmortization("15")// use attribute "value" to run test in both languages fr and en
                .WithFrequency("weekly") // in this case we can convert periodic values into amount of payments per year
                                        //  or create a comment for QA with description, that weekly and Hebdomadaire are equal 52
                .WithRate("5")
                .Submit();
            Assert.That(CalcPage.MonthlyPaymentResultDisplayed(), Is.True);
   
            Assert.That(CalcPage.MonthlyPaymentResult(), Is.EqualTo("$ 836.75"));

            CalcPage.CalcByPrice().SetPurchasePriceWithBtn("500000")
                .SetDownPaymentWithBtn("50000")
                .WithAmortization("15")
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
                .WithAmortization("15")
                .WithFrequency("Biweekly")
                .Submit();
            Assert.That(CalcPage.MonthlyPaymentResultDisplayed(), Is.True);
        }

        [Test]

        public void ShoudParsePDFforResults()
        {
            //this test has a defect. If not to delete pdf file from prev test, file name will not match
            CalcPage.CalcByMortgage().WithMortgageAmount("444444")
               .WithRate("3,55")
               .WithAmortization("15")
               .WithFrequency("Biweekly")
               .Submit();
            CalcPage.DownloadPDFwithResults();
            Assert.That(CalcPage.ParsePdf(), Is.EqualTo(CalcPage.MonthlyPaymentResult()));
        }
    }
}
