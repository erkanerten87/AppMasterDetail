using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace AppMasterDetail.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class Detail : ContentPage
	{
		public Detail ()
		{
			InitializeComponent ();
        }

        void OnImgMenuTapGestureRecognizerTapped(object sender, EventArgs args)
        {
            App.MasterDetail.IsPresented = true;
        }
    }
}