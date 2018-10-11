using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Realms;

namespace Rental_Cars
{
    class CustomerDetailsModel : RealmObject
    {
        public string CustomerName { set; get; }
        public string CustomerEmail { set; get; }
        public string CustomerLicense { set; get; }
        public string CustomerDropIn { set; get; }
        public string CustomerDropOff { set; get; }
        public string CustomerDays { set; get; }
        public string CustomerAge { set; get; }
        public string CustomerGender { set; get; }
    }
}