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
    [Activity(Label = "ViewProfile")]
    public class ViewProfile : Activity
    {
        TextView ProfileFname, ProfileLname, ProfileEmail, ProfileUsername, ProfilePassword;
        Button Update,back;
        Realm realmObj;
    
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.ProfileView);

            string UserFname = Intent.GetStringExtra("FIRSTNAME");
            string UserLname = Intent.GetStringExtra("LASTNAME");
            string UserEmail = Intent.GetStringExtra("USERID");

            ProfileFname = FindViewById<TextView>(Resource.Id.ProfileFirstNameId);
            ProfileLname = FindViewById<TextView>(Resource.Id.ProfileLastNameId);
            ProfileEmail = FindViewById<TextView>(Resource.Id.ProfileEmailId);
            ProfileUsername = FindViewById<TextView>(Resource.Id.ProfileUsernameId);
            ProfilePassword = FindViewById<TextView>(Resource.Id.ProfilePasswordId);
            Update = FindViewById<Button>(Resource.Id.UpdateId);
            back = FindViewById<Button>(Resource.Id.BackId);
            
            realmObj = Realm.GetInstance();
            var userInfo = realmObj.All<User>();
            
                foreach (var temp in userInfo)
                {
                    if (temp.Email.Equals(UserEmail))
                    {
                        ProfileFname.Text = temp.FirstName.ToString();
                        ProfileLname.Text = temp.LastName.ToString();
                        ProfileEmail.Text = temp.Email.ToString();
                        ProfileUsername.Text = temp.Username.ToString();
                        ProfilePassword.Text = temp.Password.ToString();
                    }
                }
            Update.Click += delegate
            {
                AlertDialog.Builder alertBuilder = new AlertDialog.Builder(this);
                
                LayoutInflater layoutInflate = LayoutInflater.From(this);
                View view = layoutInflate.Inflate(Resource.Layout.ProfileUpdatePopUp, null);
                alertBuilder.SetView(view);
                var Fname = view.FindViewById<TextView>(Resource.Id.PopUpFirstNameId);
                var Lname = view.FindViewById<EditText>(Resource.Id.PopUpLastNameId);
                var email = view.FindViewById<TextView>(Resource.Id.PopUpEmailId);
                var userName = view.FindViewById<EditText>(Resource.Id.PopUpUsernameId);
                var pass = view.FindViewById<EditText>(Resource.Id.PopUpPasswordId);

                email.Text = UserEmail.ToString();
                Fname.Text = UserFname.ToString();

                alertBuilder.SetCancelable(false)
                .SetPositiveButton("Update", delegate
                {
                    var user = realmObj.All<User>();
                        foreach (var temp in user)
                        {
                            if (temp.Email.Equals(UserEmail))
                            {
                                var updateInfo = new User();
                                updateInfo.FirstName = Fname.Text;
                                updateInfo.LastName = Lname.Text;
                                updateInfo.Email = email.Text;
                                updateInfo.Username = userName.Text;
                                updateInfo.Password = pass.Text;

                                realmObj.Write(() =>
                                {
                                    realmObj.Add(updateInfo, update : true);
                                });
                            }
                        }
                    Intent newScreen = new Intent(this, typeof(ViewProfile));
                    newScreen.PutExtra("FIRSTNAME", UserFname);
                    newScreen.PutExtra("LASTNAME", UserLname);
                    newScreen.PutExtra("USERID", UserEmail);
                    StartActivity(newScreen);
                })
                .SetNegativeButton("Cancel", delegate
                {
                    alertBuilder.Dispose();
                });
                AlertDialog dialog = alertBuilder.Create();
                dialog.Show();
            };

            back.Click += delegate
            {
                Intent newScreen = new Intent(this, typeof(Main_Page));
                newScreen.PutExtra("FIRSTNAME", UserFname);
                newScreen.PutExtra("LASTNAME", UserLname);
                newScreen.PutExtra("EMAIL", UserEmail);
                StartActivity(newScreen);
            };
            
        }
    }
}