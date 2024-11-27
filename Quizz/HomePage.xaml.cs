using Microsoft.Maui.Controls;

namespace Quizz
{
    [QueryProperty(nameof(Email), "email")]
    [QueryProperty(nameof(Username), "username")]
    public partial class HomePage : ContentPage
    {
        public string Email { get; set; }

        private string _username;
        public string Username
        {
            get => _username;
            set
            {
                _username = value;
                WelcomeMessage = $"Bem-vindo, {_username}!";
            }
        }

        private string _welcomeMessage;
        public string WelcomeMessage
        {
            get => _welcomeMessage;
            set
            {
                _welcomeMessage = value;
                OnPropertyChanged(nameof(WelcomeMessage)); 
            }
        }

        public HomePage()
        {
            InitializeComponent();
            BindingContext = this; // Set the BindingContext to the page
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            // Log or use the data as needed
            Console.WriteLine($"Logged in as: {Username} ({Email})");
        }
    }
}
