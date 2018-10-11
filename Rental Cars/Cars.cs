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
    public class Cars : RealmObject
    {
        [PrimaryKey]
        public string CarModelName { set; get; } //= Guid.NewGuid().ToString();
        public string CarDoors { set; get; }
        public string CarMileage { set; get; }
        public int NumberOfCars { set; get; }
        public int CarImageId { set; get; }
        public string CarType { set; get; }
    }
}