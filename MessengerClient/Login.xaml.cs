using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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

        public Login()
        {
            InitializeComponent();
        }

        private void loginBtn_Click(object sender, RoutedEventArgs e)
        {
            SendReceive.User.Username = usernameLogin.Text;
            SendReceive.User.Password = passwordLogin.Text;
            SendReceive.User.IsLogin = true;
            SendReceive.SendCredentials();
            do
            {
            } while (!SendReceive.UserAuthenticated);
            this.Close();
        }

        private void registrationBtn_Click(object sender, RoutedEventArgs e)
        {
            SendReceive.User.Username = usernameRegistration.Text;
            SendReceive.User.Password = passwordRegistration.Text;
            SendReceive.User.Nickname = nickname.Text;
            SendReceive.User.IsLogin = false;
            SendReceive.SendCredentials();
            registrationBtn.IsEnabled = false;
            do
            {
            } while (!SendReceive.UserAuthenticated);
            this.Close();
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
