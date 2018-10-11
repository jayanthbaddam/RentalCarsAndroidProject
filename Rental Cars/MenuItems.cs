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
    class MenuItems
    {
        public String MenuIxt;
        public int MenuImage;

        public MenuItems(int imageIdInfo, string textInfo)
        {
            MenuImage = imageIdInfo;
            MenuIxt = textInfo;
        }
    }
}