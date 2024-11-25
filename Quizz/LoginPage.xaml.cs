using Microsoft.Maui.Controls;
using System.Windows.Input;

namespace Quizz
{
    public partial class LoginPage : ContentPage
    {
        public ICommand SignUpCommand { get; }

        public LoginPage()
        {
            InitializeComponent();
            SignUpCommand = new Command(OnSignUpClicked);
            BindingContext = this;
        }

        private async void OnSignUpClicked()
        {
            
            await Navigation.PushAsync(new SignUp());
        }
    }
}
