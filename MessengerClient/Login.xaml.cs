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
        }

        private void registrationBtn_Click(object sender, RoutedEventArgs e)
        {
            SendReceive.User.Username = usernameRegistration.Text;
            SendReceive.User.Password = passwordRegistration.Text;
            SendReceive.User.Nickname = nickname.Text;
            SendReceive.User.IsLogin = false;
            SendReceive.SendCredentials();
        }
    }
}
