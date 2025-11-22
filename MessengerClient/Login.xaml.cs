using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace MessengerClient
{
    /// <summary>
    /// Interaction logic for Login.xaml
    /// </summary>
    public partial class Login : Window
    {
        public event EventHandler Authenticated;
        public static Label hiba;
        public Login()
        {
            InitializeComponent();
            hiba = this.helytelenLabel;
        }
        public static void Helytelen()
        {
            hiba.Visibility = Visibility.Visible;
        }
        private async void loginBtn_Click(object sender, RoutedEventArgs e)
        {
            loginBtn.IsEnabled = false;
            loginBorder.Background = Brushes.LightGray;
            SendReceive.User.Username = usernameLogin.Text;
            SendReceive.User.Password = passwordLogin.Text;
            SendReceive.User.IsLogin = true;
            Button b = sender as Button;
            SendReceive.SendCredentials();
            await Task.Delay(4000);
            if (SendReceive.UserAuthenticated)this.Close();
            else
            {
                loginBtn.IsEnabled = true;
                loginBorder.Background = Brushes.CadetBlue;
            }
        }

        private async void registrationBtn_Click(object sender, RoutedEventArgs e)
        {
            registrationBtn.IsEnabled = false;
            registrationBorder.Background = Brushes.LightGray;
            SendReceive.User.Username = usernameRegistration.Text;
            SendReceive.User.Password = passwordRegistration.Text;
            SendReceive.User.Nickname = nickname.Text;
            SendReceive.User.IsLogin = false;
            SendReceive.SendCredentials();
            await Task.Delay(4000);
            if (SendReceive.UserAuthenticated) this.Close();
            else
            {
                registrationBtn.IsEnabled = true;
                registrationBorder.Background = Brushes.CadetBlue;
            }
        }

        private void Valtozatatas(object sender, RoutedEventArgs e)
        {
            if (Bejelentkezes.Visibility == Visibility.Visible)
            {
                Bejelentkezes.Visibility = Visibility.Collapsed;
                Regisztracio.Visibility = Visibility.Visible;
            }
            else
            {
                Bejelentkezes.Visibility = Visibility.Visible;
                Regisztracio.Visibility = Visibility.Collapsed;
            }
        }

        private void Fokusz(object sender, RoutedEventArgs e)
        {
            var txt = (TextBox)sender;
            if (txt != null)
            {
                txt.Text = string.Empty;
            }
        }

        private void FFokusz(object sender, RoutedEventArgs e)
        {
            var txt = (TextBox)sender;
            if (string.IsNullOrEmpty(txt.Text)) txt.Text = "Felhasználónév";
        }

        private void JFokusz(object sender, RoutedEventArgs e)
        {
            var text = (TextBox)sender;
            if (string.IsNullOrEmpty(text.Text)) text.Text = "Jelszó";
        }

        private void BFokusz(object sender, RoutedEventArgs e)
        {
            var text = (TextBox)sender;
            if (string.IsNullOrEmpty(text.Text)) text.Text = "Becenév";
        }
    }
}
