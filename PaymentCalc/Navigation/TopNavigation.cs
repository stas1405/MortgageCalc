using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaymentCalc.Navigation
{
    public class TopNavigation
    {
        public class Loans
        {
            public class Mortgages
            {
                public static void Select()
                {
                    MenuSelector.Select("loans", "mortgage_loan");
                }
            }
        }
    }
}
