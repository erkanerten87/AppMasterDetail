using AppMasterDetail.ViewModels;
using System;
using Xamarin.Forms;

namespace AppMasterDetail.Views
{
    public partial class AboutPage : ContentPage
	{
		ItemsViewModel viewModel;

		public AboutPage()
		{
			InitializeComponent();

			BindingContext = viewModel = new ItemsViewModel();
		}
        
		
		protected override void OnAppearing()
		{
			base.OnAppearing();

			if (viewModel.Items.Count == 0)
				viewModel.LoadItemsCommand.Execute(null);
		}

        private void Button_Clicked(object sender, EventArgs e)
        {
            Navigation.PopModalAsync();
        }
    }
}
