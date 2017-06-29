using System;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace AppMasterDetail.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class LoginPage : ContentPage
	{
        public delegate void OnConnectSent(string userName);
        public event OnConnectSent ConnectSent;

        public LoginPage ()
		{
			InitializeComponent ();
            NavigationPage.SetHasNavigationBar(this, false);            
        }
        
       void OnTapGestureRecognizerTapped(object sender, EventArgs args)
        {
            //var user = new User
            //{
            //    Username = usernameEntry.Text,
            //    Password = passwordEntry.Text
            //};

            var isValid = true;// AreCredentialsCorrect(user);
            if (isValid)
            {
                App.IsUserLoggedIn = true;
                Application.Current.MainPage = new MainPage();
                //Navigation.InsertPageBefore(new MasterMenuPage(), this);
                //await Navigation.PopAsync();
            }
            else
            {
                messageLabel.Text = "Login failed";
                passwordEntry.Text = string.Empty;
            }

            App.LoginPageInstance = this;

            ConnectSent?.Invoke(usernameEntry.Text);
        }

        void OnTapAboutGestureRecognizerTapped(object sender, EventArgs args)
        {
            Navigation.PushModalAsync(new AboutPage());
        }

        //bool AreCredentialsCorrect(User user)
        //{
        //    return user.Username == Constants.Username && user.Password == Constants.Password;
        //}
    }
}