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
    class CarsCart : RealmObject
    {
        [PrimaryKey]
        public string CarCartModelName { set; get; } //= Guid.NewGuid().ToString();
        public string CarCartDoors { set; get; }
        public string CarCartMileage { set; get; }
        public int NumberOfCarsCart { set; get; }
        public int CarCartImageId { set; get; }
        public string CarCartType { set; get; }
    }
}