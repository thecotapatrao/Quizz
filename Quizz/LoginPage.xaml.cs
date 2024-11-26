using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Firebase.Auth;
using Firebase.Auth.Providers;
using Microsoft.Maui.Controls;
using System.Windows.Input;

namespace Quizz
{
    public partial class LoginPage : ContentPage
    {
        private readonly FirebaseAuthClient _client;

        public ICommand GoToSignUpCommand { get; }
        public ICommand LogInCommand { get; }

        private string _email;
        public string Email
        {
            get => _email;
            set
            {
                _email = value;
                OnPropertyChanged();
            }
        }

        private string _password;
        public string Password
        {
            get => _password;
            set
            {
                _password = value;
                OnPropertyChanged();
            }
        }

        public LoginPage()
        {
            InitializeComponent();

            // Inicializar cliente FirebaseAuth
            _client = new FirebaseAuthClient(new FirebaseAuthConfig
            {
                AuthDomain = "quizz-game-4c3d5.firebaseapp.com",
                ApiKey = "AIzaSyC0F2002TUyh3mkMgLXB5RKuri5aNGSlXA",
                Providers = new[] { new EmailProvider() }
            });

            GoToSignUpCommand = new Command(OnSignUpClicked);
            LogInCommand = new AsyncRelayCommand(LogInAsync);

            BindingContext = this;
        }

        private async Task LogInAsync()
        {
            try
            {
                
                await _client.SignInWithEmailAndPasswordAsync(Email, Password);

                await Shell.Current.GoToAsync("//HomePage");
            }
            catch (Exception ex)
            {
                
                await DisplayAlert("Login Failed", $"Error: {ex.Message}", "OK");
            }
        }

        private async void OnSignUpClicked()
        {
            await Navigation.PushAsync(new SignUp());
        }
    }
}
