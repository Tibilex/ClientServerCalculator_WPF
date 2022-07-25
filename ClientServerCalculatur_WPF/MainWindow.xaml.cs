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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ClientServerCalculatur_WPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private static MainWindow? s_window;
        public MainWindow()
        {
            InitializeComponent();
            s_window = this;
        }

        #region --- Header ---
        private void MinimiseBtn_Click(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }

        private void CloseBtn_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void MovingWindow(object sender, MouseButtonEventArgs e)
        {
            if (Mouse.LeftButton == MouseButtonState.Pressed)
            {
                s_window.DragMove();
            }
        }
        #endregion

        #region --- Opertions ---
        private void SumButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void SubtractButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void DevideButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void MultiplyButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void ResultButton_Click(object sender, RoutedEventArgs e)
        {

        }
        #endregion
    }
}
