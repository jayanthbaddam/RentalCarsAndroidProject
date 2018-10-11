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
    class User : RealmObject
    {
        public string FirstName { set; get; }
        public string LastName { set; get; }
        [PrimaryKey]
        public string Email { set; get; }
        public string Username { set; get; } //= Guid.NewGuid().ToString();
        public string Password { set; get; }

    }
}