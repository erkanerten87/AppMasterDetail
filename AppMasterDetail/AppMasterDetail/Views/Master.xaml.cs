using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace AppMasterDetail.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class Master : ContentPage
	{
		public Master ()
		{
			InitializeComponent ();            
		}
        
        private void btnChat_Clicked(object sender, EventArgs e)
        {
            App.NavigateMasterDetail(new ChatMenuPage());
        }

        private void btnOnaylar_Clicked(object sender, EventArgs e)
        {
            App.NavigateMasterDetail(new OnaylarPage());
        }

        private void btnBsm_Clicked(object sender, EventArgs e)
        {
            App.NavigateMasterDetail(new BsmPage());
        }

        private void btnAlarmlar_Clicked(object sender, EventArgs e)
        {
            App.NavigateMasterDetail(new AlarmPage());
        }

        void OnLogOutGestureRecognizerTapped(object sender, EventArgs args)
        {
            Debug.WriteLine("Close Called");
        }
    }
}