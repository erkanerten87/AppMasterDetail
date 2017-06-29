using Android.App;
using Android.Content.PM;
using Android.OS;
using AppMasterDetail;
using AppMasterDetail.Views;

namespace ZiraatTeknoloji.Kurumsal
{
    [Activity(Theme = "@style/MyTheme", MainLauncher = false, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        protected override void OnCreate(Bundle bundle)
        {
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;

            base.OnCreate(bundle);

            global::Xamarin.Forms.Forms.Init(this, bundle);

            LoadApplication(new App());
        }
        public override void OnBackPressed()
        {
            App.NavigateMasterDetail(new Detail());
        }

        

    }
}