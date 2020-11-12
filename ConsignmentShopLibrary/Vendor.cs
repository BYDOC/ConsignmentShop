using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsignmentShopLibrary
{
    public class Vendor
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public double Commission { get; set; }
        public decimal PaymentDue { get; set; }


        public string Display
        {
            get
            {
                return String.Format("{0} {1} - ${2}", FirstName, LastName, PaymentDue);
            }
        }


        //constructor is a method that run when a new instance of a class is created
        public Vendor()
        {
            Commission = 0.5;
        }
    }
}
