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
    [Activity(Label = "Login")]
    public class Login : Activity
    {
        EditText user, pass;
        Button loginBtn;
        TextView RegisterButton;
        Realm realmObj;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.Login);
            user = FindViewById<EditText>(Resource.Id.userId);
            pass = FindViewById<EditText>(Resource.Id.passId);
            loginBtn = FindViewById<Button>(Resource.Id.LoginId);
            RegisterButton = FindViewById<TextView>(Resource.Id.registerLinkId);
            
            realmObj = Realm.GetInstance();
            var userInfo = realmObj.All<User>();

            RegisterButton.Click += delegate
            {
                Intent newScreen = new Intent(this, typeof(Register));
                StartActivity(newScreen);
            };

            loginBtn.Click += delegate
            {
                AlertDialog.Builder Dialog = new AlertDialog.Builder(this);
                if(user.Text.Equals("") || pass.Text.Equals(""))
                {
                    AlertDialog alert = Dialog.Create();
                    alert.SetMessage("Please enter username and password");
                    alert.SetButton("OK", (c, ev) => { });
                    alert.Show();
                } else if (user.Text.Equals("admin") && pass.Text.Equals("admin"))
                {
                    Intent newScreen = new Intent(this, typeof(Main_Page));
                    newScreen.PutExtra("ADMIN", user.Text);
                    StartActivity(newScreen);
                } else
                {
                    foreach (var temp in userInfo)
                    {
                        if (temp.Username == user.Text && temp.Password == pass.Text)
                        {
                            System.Console.WriteLine("Login successfull");
                            System.Console.WriteLine("Username is : " + temp.Username + " Password is : " + temp.Password);
                            Intent newScreen = new Intent(this, typeof(Main_Page));
                            newScreen.PutExtra("USERNAME", temp.Username);
                            newScreen.PutExtra("FIRSTNAME", temp.FirstName);
                            newScreen.PutExtra("LASTNAME", temp.LastName);
                            newScreen.PutExtra("EMAIL", temp.Email);
                            StartActivity(newScreen);
                        }
                    }
                    AlertDialog alert = Dialog.Create();
                    alert.SetMessage("Invalid credentials!!");
                    alert.SetButton("OK", (c, ev) => { });
                    alert.Show();
                }
                
            };
        }
    }
}