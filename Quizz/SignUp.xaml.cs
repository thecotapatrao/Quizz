using CommunityToolkit.Mvvm.Input;
using Firebase.Auth;
using Firebase.Auth.Providers;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace Quizz
{
    public partial class SignUp : ContentPage, INotifyPropertyChanged
    {
        private string email;
        private string password;
        private string username;
        private readonly FirebaseAuthClient _client;

        // Properties with Bindable implementation
        public string Email
        {
            get => email;
            set => SetProperty(ref email, value);
        }

        public string Password
        {
            get => password;
            set => SetProperty(ref password, value);
        }

        public string Username
        {
            get => username;
            set => SetProperty(ref username, value);
        }

        // Commands
        public Command TogglePasswordVisibilityCommand { get; }
        public Command ToggleConfirmPasswordVisibilityCommand { get; }
        public Command SignUpNewCommand { get; }

        public SignUp()
        {
            InitializeComponent();

            // Firebase client initialization
            var config = new FirebaseAuthConfig
            {
                ApiKey = "AIzaSyC0F2002TUyh3mkMgLXB5RKuri5aNGSlXA",
                AuthDomain = "quizz-game-4c3d5.firebaseapp.com",
                Providers = new[] { new EmailProvider() }
            };
            _client = new FirebaseAuthClient(config);

            // Initialize commands
            TogglePasswordVisibilityCommand = new Command(() => ToggleVisibility(PasswordEntry, PasswordEyeIcon));
            ToggleConfirmPasswordVisibilityCommand = new Command(() => ToggleVisibility(ConfirmPasswordEntry, ConfirmPasswordEyeIcon));
            SignUpNewCommand = new Command(async () => await SignUpNew());

            BindingContext = this;
        }

        private async void OnBackToLoginTapped(object sender, EventArgs e)
        {
            await Navigation.PopAsync();
        }

        private async Task SignUpNew()
        {
            try
            {
                var result = await _client.CreateUserWithEmailAndPasswordAsync(Email, Password, Username);
                await Shell.Current.GoToAsync("//LoginPage");
            }
            catch (Exception ex)
            {
                await DisplayAlert("Error", $"Failed to sign up: {ex.Message}", "OK");
            }
        }

        private void ToggleVisibility(Entry entry, Image icon)
        {
            entry.IsPassword = !entry.IsPassword;
            icon.Source = entry.IsPassword ? "eye_closed.png" : "eye_open.png";
        }

        // INotifyPropertyChanged implementation
        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        protected bool SetProperty<T>(ref T storage, T value, [CallerMemberName] string propertyName = null)
        {
            if (Equals(storage, value)) return false;

            storage = value;
            OnPropertyChanged(propertyName);
            return true;
        }
    }
}
