using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace AppMasterDetail.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ChatHerkesPage : ContentPage
    {
        public delegate void OnBroadcastMessageSent(string user, string message);
        public event OnBroadcastMessageSent BroadcastMessageSent;

        public delegate void OnBroadcastMessageTextBoxTextChanged(string user);
        public event OnBroadcastMessageTextBoxTextChanged BroadcastMessageTextBoxTextChanged;

        public ChatHerkesPage()
        {
            InitializeComponent();
            lbDate.Text = DateTime.Now.ToString("dd.MM.yyyy");
        }

        private void btnChatSend_Clicked(object sender, EventArgs e)
        {
            BroadcastMessageSent?.Invoke(App.CurrentUser, ChatTextField.Text);
            ChatTextField.Text = string.Empty;
        }

        public void SetText(string userName, string message)
        {
            try
            {
                Device.BeginInvokeOnMainThread(
                () =>
                {
                    spChat.Children.Add(new Label()
                    {
                        Text = string.Format("{0}: {1}", userName, message),
                        HorizontalTextAlignment = TextAlignment.End,
                        HorizontalOptions = LayoutOptions.EndAndExpand,
                        BackgroundColor = Color.LightGreen,
                        TextColor = Color.Black,
                        Margin = new Thickness(15)
                    });
                });
            }
            catch (Exception ex)
            {
                Device.BeginInvokeOnMainThread(
                () =>
                {
                    DisplayAlert("Hata", string.Format("Hata : {0}", ex.ToString()), "Tamam");
                });

            }
        }

        public void SetTypingText(string userName)
        {
            try
            {
                Device.BeginInvokeOnMainThread(
                () =>
                {
                    lbTyping.Text = string.Format("{0} is typing...", userName);
                });


                Device.StartTimer(TimeSpan.FromSeconds(5), () =>
                {
                    lbTyping.Text = string.Empty;
                    return false;
                });

            }
            catch (Exception ex)
            {
                Device.BeginInvokeOnMainThread(
                () =>
                {
                    DisplayAlert("Hata", string.Format("Hata : {0}", ex.ToString()), "Tamam");
                });

            }
        }

        private void ChatTextField_TextChanged(object sender, TextChangedEventArgs e)
        {
            BroadcastMessageTextBoxTextChanged?.Invoke(App.CurrentUser);
        }
    }
}