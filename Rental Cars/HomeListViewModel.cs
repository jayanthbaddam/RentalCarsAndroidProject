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

namespace Rental_Cars
{
    public class HomeListViewModel
    {
        public String text;
        public int imageId;

        public HomeListViewModel(int imageIdInfo, string textInfo)
        {
            imageId = imageIdInfo;
            text = textInfo;
        }
    }
}