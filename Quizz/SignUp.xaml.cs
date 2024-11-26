using Microsoft.Maui.Controls;

namespace Quizz
{
    public partial class SignUp : ContentPage
    {
        public Command TogglePasswordVisibilityCommand { get; }
        public Command ToggleConfirmPasswordVisibilityCommand { get; }

        public SignUp()
        {
            InitializeComponent();

            // Inicializar comandos
            TogglePasswordVisibilityCommand = new Command(() => ToggleVisibility(PasswordEntry, PasswordEyeIcon));
            ToggleConfirmPasswordVisibilityCommand = new Command(() => ToggleVisibility(ConfirmPasswordEntry, ConfirmPasswordEyeIcon));

            BindingContext = this;
        }
        private async void OnBackToLoginTapped(object sender, EventArgs e)
        {
            await Navigation.PopAsync();
        }

        /// <summary>
        /// Alterna a visibilidade da senha e atualiza o ícone correspondente.
        /// </summary>
        /// <param name="entry">Campo de entrada (password ou confirm password).</param>
        /// <param name="icon">Ícone de visibilidade associado.</param>
        private void ToggleVisibility(Entry entry, Image icon)
        {
            entry.IsPassword = !entry.IsPassword;

           
            icon.Source = entry.IsPassword ? "eye_crossed.png" : "eye.png";
        }
    }
}
