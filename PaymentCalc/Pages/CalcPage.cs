using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using System;
using iTextSharp.text.pdf;
using iTextSharp.text.pdf.parser;
using System.IO;
using System.Text;

namespace PaymentCalc
{
    public class CalcPage
    {
       // String DownloadfilePath = "c:\\";
        public static void MoveSlider(int step)
        {
            var slider = Driver.Instance.FindElement(By.CssSelector("*[class^='slider']"));
            Action.Instance.MoveToElement(slider).ClickAndHold().MoveByOffset(0, step).Release().Perform();
        }

        public static int CheckPurchasedPrice()
        {
            return int.Parse(Driver.Instance.FindElement(By.Id("PrixPropriete")).GetAttribute("value"));
        }

        public static int CheckDownPayment()
        {
            return int.Parse(Driver.Instance.FindElement(By.Id("MiseDeFond")).GetAttribute("value"));
        }


        //Check if results of calculation are displayed on a right side hand of the screen
        public static Boolean MonthlyPaymentResultDisplayed()
        {
            var calcResults = Driver.Instance.FindElement(By.Id("phrase-resultats"));
            if(calcResults != null)
            {
                return true;
            }
            return false;
        }

        //Check results of calculation, displayed on a right side hand of the screen
        public static string MonthlyPaymentResult()
        {
            var calcResults = Driver.Instance.FindElement(By.Id("paiement-resultats")).Text;
            Console.WriteLine(calcResults);
            return calcResults.ToString();
        }

        //Calculates mortgage when Purchase price radio-btn is selected.
        public static CalcByPrice CalcByPrice()
        {
            return new CalcByPrice();
        }

        //Calculates mortgage when Mortgage amount radio-btn is selected.
        public static CalcByMortgage CalcByMortgage()
        {
            return new CalcByMortgage();
        }

        public static void DownloadPDFwithResults()
        {
            Driver.Instance.FindElement(By.Id("imprimer")).Click();
            WebDriverWait waitObj = new WebDriverWait(Driver.Instance, TimeSpan.FromSeconds(5));
            waitObj.Until(ExpectedConditions.InvisibilityOfElementLocated(By.CssSelector("*[class^='loadingGif']")));
        }

        public static string ParsePdf()
        {
            var path = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);
            path += "\\Downloads\\iA_Mortgage_Payment_Calculator.pdf";
            PdfReader reader = new PdfReader(path);
            String PDFContents = PdfTextExtractor.GetTextFromPage(reader, 1);
            Console.WriteLine("File length:" + reader.FileLength);
            Console.WriteLine("No. Of pages" + reader.NumberOfPages);

            StringBuilder sb = new StringBuilder();
            ITextExtractionStrategy its = new SimpleTextExtractionStrategy();

            int i =  reader.NumberOfPages;         
            sb.Clear();
            sb = sb.Append(PdfTextExtractor.GetTextFromPage(reader, i, its));

            string[] tokens = sb.ToString().Split(new string[] { "\r\n", "\n" }, StringSplitOptions.None);
            
            try
            {
                reader.Close();
                
            }
            catch
            {
                Console.WriteLine("Exception Occured while reading PDF File....!");
            }
            File.Delete(path);
            return tokens[11];
        }
    }
    

    //This is a class, wich describes functionality of calculator, when Mortgage amount radio-btn is selected
    public class CalcByMortgage
    {
        private string mortgageAmount;
        private string rate;
        private string amortization;
        private string freq;
        private string purchasePrice;

        public CalcByMortgage WithMortgageAmount(string mortgageAmount)
        {
            this.mortgageAmount = mortgageAmount;
            return this;
        }

        public CalcByMortgage WithRate(string rate)
        {
            this.rate = rate;
            return this;
        }

        public CalcByMortgage WithAmortization(string amortization)
        {
            this.amortization = amortization;
            return this;
        }

        public CalcByMortgage WithFrequency(string freq)
        {
            this.freq = freq;
            return this;
        }

        public CalcByMortgage SetPurchasePriceWithBtn(string purchasePrice)
        {
            this.purchasePrice = purchasePrice;
            return this;
        }

        public void Submit()
        {
            var mortgage = Driver.Instance.FindElement(By.Id("par_pret"));
            mortgage.SendKeys(Keys.Space);

            var amount = Driver.Instance.FindElement(By.Id("PrixPropriete"));
            amount.SendKeys(Keys.Backspace);
            amount.SendKeys(mortgageAmount);

            var interestRate = Driver.Instance.FindElement(By.Id("TauxInteret"));
            interestRate.SendKeys(Keys.Control + "a");
            interestRate.SendKeys(rate);

            var selectAmort = Driver.Instance.FindElement(By.Id("Amortissement"));
            var selectElementAmort = new SelectElement(selectAmort);
            selectElementAmort.SelectByValue(amortization);

            var selectFreq = Driver.Instance.FindElement(By.Id("FrequenceVersement"));
            var selectElementFreq = new SelectElement(selectFreq);
            selectElementFreq.SelectByText(freq);

            var submit = Driver.Instance.FindElement(By.Id("btn_calculer"));
            submit.Click();

            //Refactor: create a method wait() in Driver class
            WebDriverWait waitObj = new WebDriverWait(Driver.Instance, TimeSpan.FromSeconds(15));
            waitObj.Until(ExpectedConditions.InvisibilityOfElementLocated(By.CssSelector("*[class^='loadingGif']")));

        }
    }

    //This is a class, wich describes functionality of calculator, when Purchase price radio-btn is selected
    public class CalcByPrice
    {
        private string mortgageAmount;
        private string rate;
        private string amortization;
        private string freq;
        private string purchasePrice;
        private string downPayment;

        public CalcByPrice WithMortgageAmount(string mortgageAmount)
        {
            this.mortgageAmount = mortgageAmount;
            return this;
        }

        public CalcByPrice WithRate(string rate)
        {
            this.rate = rate;
            return this;
        }

        public CalcByPrice WithAmortization(string amortization)
        {
            this.amortization = amortization;
            return this;
        }

        public CalcByPrice WithFrequency(string freq)
        {
            // if freq == "weekly" or freq== "Hebdomadaire"  , than freq = "52" and so on
            this.freq = freq;
            return this;
        }

        public CalcByPrice SetPurchasePriceWithBtn(string purchasePrice)
        {
            this.purchasePrice = purchasePrice;
            return this;
        }

        public CalcByPrice SetDownPaymentWithBtn(string downPayment)
        {
            this.downPayment = downPayment;
            return this;
        }

        public void Submit()
        {          
            for(int i = 0; i < 5; i++)
            {
                int price = CalcPage.CheckPurchasedPrice();
                if (price < int.Parse(purchasePrice))
                {
                    var plusBtn = Driver.Instance.FindElement(By.Id("PrixProprietePlus"));
                    plusBtn.Click();
                }
                if (price > int.Parse(purchasePrice))
                {
                    var minusBtn = Driver.Instance.FindElement(By.Id("PrixProprieteMinus"));
                    minusBtn.Click();
                }
                else
                {
                    continue;
                }
            }

            for (int i = 0; i < 5; i++)
            {
                int price = CalcPage.CheckDownPayment();
                if (price < int.Parse(downPayment))
                {
                    var downPayBtnPlus = Driver.Instance.FindElement(By.Id("MiseDeFondPlus"));
                    downPayBtnPlus.Click();
                }
                if (price > int.Parse(downPayment))
                {
                    var downPayBtnMinus = Driver.Instance.FindElement(By.Id("MiseDeFondMinus"));
                    downPayBtnMinus.Click();
                }
                else
                {
                    continue;
                }
            }

            var interestRate = Driver.Instance.FindElement(By.Id("TauxInteret"));
            interestRate.SendKeys(Keys.Control + "a");
            interestRate.SendKeys(rate);

            var selectAmort = Driver.Instance.FindElement(By.Id("Amortissement"));
            var selectElementAmort = new SelectElement(selectAmort);
            selectElementAmort.SelectByValue(amortization); //.SelectByText(amortization);

            var selectFreq = Driver.Instance.FindElement(By.Id("FrequenceVersement"));
            var selectElementFreq = new SelectElement(selectFreq);
            selectElementFreq.SelectByText(freq);

            var submit = Driver.Instance.FindElement(By.Id("btn_calculer"));
            submit.Click();

            //Refactor: create a method wait() in Driver class
            WebDriverWait waitObj = new WebDriverWait(Driver.Instance, TimeSpan.FromSeconds(15));
            waitObj.Until(ExpectedConditions.InvisibilityOfElementLocated(By.CssSelector("*[class^='loadingGif']")));

        }
    }
}
