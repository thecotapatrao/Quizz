using CommunityToolkit.Mvvm.Input;
using Firebase.Auth;
using Firebase.Auth.Providers;
using Firebase.Database.Query;
using Microsoft.Maui.Controls;
using System.Threading.Tasks;
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

            // Initialize FirebaseAuth Client
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
               
                var userCredential = await _client.SignInWithEmailAndPasswordAsync(Email, Password);

               
                var userId = userCredential.User.Uid;
                var username = await FetchUsernameFromDatabase(userId);

                // Navigate to HomePage with Email and Username
                await Shell.Current.GoToAsync($"//HomePage?email={Email}&username={username}");
            }
            catch (Exception ex)
            {
                await DisplayAlert("Login Failed", $"Error: {ex.Message}", "OK");
            }
        }

        private async Task<string> FetchUsernameFromDatabase(string userId)
        {
            // Replace this with Firebase Database client initialization
            var dbClient = new Firebase.Database.FirebaseClient("https://quizz-game-4c3d5-default-rtdb.europe-west1.firebasedatabase.app");

            var user = await dbClient
                .Child("users")
                .Child(userId)
                .OnceSingleAsync<dynamic>();

            return user?.Username ?? "Unknown User";
        }

        private async void OnSignUpClicked()
        {
            await Navigation.PushAsync(new SignUp());
        }
    }
}
