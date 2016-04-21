using NUnit.Framework;
using PaymentCalc.Pages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaymentCalc.Tests
{
    public class BaseTestClass
    {
        [SetUp]
        public void Init()
        {
            Driver.Initialize();
            Action.Initialize();
            MainPage.GoTo();

        }

        [TearDown]
        public void CleanUp()
        {
            Driver.Close();
        }
    }
}
