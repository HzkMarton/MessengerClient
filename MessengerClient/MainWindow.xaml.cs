using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Runtime.InteropServices.ComTypes;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace MessengerClient
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private SendReceive sendReceive;
        public static StackPanel messageContainer;
        public static ScrollViewer scrollViewerForMessages;
        public MainWindow()
        {
            InitializeComponent();
            messageContainer = messageBubbleContainer;
            sendReceive = new SendReceive();
            scrollViewerForMessages = scrollViewerMessages;
            
                Login l = new Login();
                l.ShowDialog();
            if(!SendReceive.UserAuthenticated) Application.Current.Shutdown();
            
        }
        public static void AddBubble(string text, string nickname, bool isSelf)
        {
            if (text != null)
            {
                Label l = new Label
                {
                    Content = text,
                    FontSize = 13,
                    HorizontalAlignment = HorizontalAlignment.Center
                };
                Label n = new Label
                {
                    Content = nickname,
                    FontSize = 12,
                    Foreground = Brushes.Gray,
                    Margin = new Thickness(5, 10, 5, -3)
                };
                l.Measure(new Size(Double.PositiveInfinity, Double.PositiveInfinity));
                double bubbleWidth = l.DesiredSize.Width +30;

                if (bubbleWidth < 50) bubbleWidth = 50;
                Border b = new Border
                {
                    Width = bubbleWidth,
                    Margin = new Thickness(10, 0, 10, 0),

                };
                if (isSelf)
                {
                    b.HorizontalAlignment = HorizontalAlignment.Right;
                    b.CornerRadius = new CornerRadius(15, 0, 15, 15);
                    b.Background = Brushes.CadetBlue;
                    n.HorizontalAlignment = HorizontalAlignment.Right;
                    l.Foreground = Brushes.White;
                }
                else
                {
                    b.HorizontalAlignment = HorizontalAlignment.Left;
                    b.CornerRadius = new CornerRadius(0, 15, 15, 15);
                    b.Background = (Brush)new BrushConverter().ConvertFrom("#DCDCDC");
                    n.HorizontalAlignment = HorizontalAlignment.Left;
                    l.Foreground = Brushes.Black;
                }

                b.Child = l;
                messageContainer.Children.Add(n);
                messageContainer.Children.Add(b);
                scrollViewerForMessages.ScrollToEnd();
            }
        }
        private void SendMessage(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtBox.Text)) return;
            SendReceive.Message = txtBox.Text;
            txtBox.Text = "";
        }

        private void LightenBtn(object sender, MouseEventArgs e)
        {
            Border b = sender as Border;
            b.Background = new SolidColorBrush(Color.FromRgb(112, 167, 169));
        }

        private void DarkenBtn(object sender, MouseEventArgs e)
        {
            Border b = sender as Border;
            b.Background = Brushes.CadetBlue;
        }

        private void txtBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key.Equals(Key.Enter)) SendMessage(sender, null);
        }
    }
}
