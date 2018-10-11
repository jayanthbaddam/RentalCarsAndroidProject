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
    [Activity(Label = "Register")]
    public class Register : Activity
    {
        EditText Fname, Lname, Email, Username, Password;
        Button register;
        Realm realmObj;
        List<User> listOfUsers = new List<User>();
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.Register);

            Fname = FindViewById<EditText>(Resource.Id.FirstNameId);
            Lname = FindViewById<EditText>(Resource.Id.LastNameId);
            Email = FindViewById<EditText>(Resource.Id.EmailId);
            Username = FindViewById<EditText>(Resource.Id.UsernameId);
            Password = FindViewById<EditText>(Resource.Id.PasswordId);
            register = FindViewById<Button>(Resource.Id.RegisterId);

            realmObj = Realm.GetInstance();

            register.Click += delegate
            {
                var userInfo = new User();
                userInfo.FirstName = Fname.Text;
                userInfo.LastName = Lname.Text;
                userInfo.Email = Email.Text;
                userInfo.Username = Username.Text;
                userInfo.Password = Password.Text;

                realmObj.Write(() =>
                {
                    realmObj.Add(userInfo, update : true);
                });
                AlertDialog.Builder Dialog = new AlertDialog.Builder(this);
                AlertDialog alert = Dialog.Create();
                alert.SetMessage("Successfully Registered!!");
                alert.SetButton("OK", (c, ev) => { });
                alert.Show();
                Intent newScreen = new Intent(this, typeof(Login));
                StartActivity(newScreen);
            };
        }
    }
}