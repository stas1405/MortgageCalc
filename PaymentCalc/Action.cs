using OpenQA.Selenium.Interactions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaymentCalc
{
    public class Action
    {
        public static Actions Instance { get; set; }

        public static void Initialize()
        {
            Instance = new Actions(Driver.Instance);
        }
    }
}
