
using AppMasterDetail.Views;
using SignalRManagement;
using System;
using System.Diagnostics;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace AppMasterDetail
{
	public partial class App : Application
	{
        public static SignalRClient SignalRClientInstance { get; set; }
        public static string CurrentUser { get; set; }
        public bool IsInBackground { set; get; }

        public static LoginPage LoginPageInstance { get; set; }

        public static ChatHerkesPage ChatHerkesPageInstance { get; set; }

        public static bool IsUserLoggedIn { get; set; }
        public static MasterDetailPage MasterDetail { get; set; }

        public static void NavigateMasterDetail(Page page)
        {
            App.MasterDetail.IsPresented = false;
            //await App.MasterDetail.Detail.Navigation.PushAsync(page);
            App.MasterDetail.Detail = page;
        }

        public App()
		{
            InitializeComponent();
            LoginPageInstance = new LoginPage();
            ChatHerkesPageInstance = new ChatHerkesPage();

            if (!IsUserLoggedIn)
            {
                Current.MainPage = LoginPageInstance;
            }
            else
            {
                SetMainPage();
            }

            LoginPageInstance.ConnectSent += (userName) =>
            {
                Connect(userName);

                //try to reconnect every 5 seconds, just in case
                Device.StartTimer(TimeSpan.FromSeconds(5), () =>
                {
                    if (!SignalRClientInstance.IsConnectedOrConnecting && !IsInBackground)
                    {
                        Connect(userName);
                    }
                    return true;
                });
            };
            
            ChatHerkesPageInstance.BroadcastMessageSent += (user, message) =>
            {
                SignalRClientInstance.SendBroadcastMessage(user, message);
            };

            ChatHerkesPageInstance.BroadcastMessageTextBoxTextChanged += (user) =>
            {
                SignalRClientInstance.SendIsTypingMessage(user);
            };
        }

        public static void SetMainPage()
        {
            Current.MainPage = new MainPage();            
        }

        protected override void OnStart()
        {
            Debug.WriteLine("OnStart...");

            IsInBackground = false;
        }
        protected override void OnSleep()
        {
            Debug.WriteLine("OnSleep...");
            SignalRClientInstance.Stop();

            IsInBackground = true;
        }

        protected override void OnResume()
        {
            Debug.WriteLine("OnResume...");
            SignalRClientInstance.Start();

            IsInBackground = false;
        }

        private void Connect(string userName)
        {
            SignalRClientInstance = new SignalRClient("http://10.75.5.201:63172", userName);

            SignalRClientInstance.Start().ContinueWith(task =>
            {
                if (task.IsFaulted)
                    MainPage.DisplayAlert("Error", "An error occurred when trying to connect to SignalR: " + task.Exception.InnerExceptions[0].Message, "OK");
            });

            registerEvents();

            CurrentUser = userName;
        }

        private void registerEvents()
        {
            SignalRClientInstance.OnBroadcastMessageReceived += (senderUserName, message) =>
            {
                ChatHerkesPageInstance.SetText(senderUserName, message);
            };

            SignalRClientInstance.OnGroupMessageReceived += (senderUserName, message, groupName) =>
            {
                //chatListView.AddText(string.Format("GroupName: {0} : {1} : {2} ", groupName, senderUserName, message));
            };

            SignalRClientInstance.OnUserMessageReceived += (senderUserName, message) =>
            {
                //chatListView.AddText(string.Format("UserMessage: {0} : {1} ", senderUserName, message));
            };

            SignalRClientInstance.OnUserTypingReceived += (user) =>
            {
                ChatHerkesPageInstance.SetTypingText(user);
            };
        }

    }
}
