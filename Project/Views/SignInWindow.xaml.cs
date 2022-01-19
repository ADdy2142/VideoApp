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

namespace Project.Views
{
    /// <summary>
    /// Interaction logic for SignInWindow.xaml
    /// </summary>
    public partial class SignInWindow : Window
    {
        // سازنده
        public SignInWindow()
        {
            InitializeComponent();
        }

        // اتمام کار برنامه با اجرای این متد
        private void BtnClose_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        // تغییر مقدار موجود در ViewModel هنگام تغییر کلمه عبور
        private void PsbPassword_PasswordChanged(object sender, RoutedEventArgs e)
        {
            ViewModel.Password = PsbPassword.Password;
        }

        // انجام عملیات ورود به برنامه
        private void BtnSignIn_Click(object sender, RoutedEventArgs e)
        {
            ViewModel.SignInUser();
        }

        // هدایت به پنجره ثبت نام
        private void BtnSignUp_Click(object sender, RoutedEventArgs e)
        {
            ViewModel.GoToSignUpWindow();
        }

        // این متد زمانی فراخوانی می شود که پنجره فعال شود
        private void Window_Activated(object sender, EventArgs e)
        {
            TxtEmail.Text = string.Empty;
            PsbPassword.Password = string.Empty;
            TxtEmail.Focus();
        }

        // تغییر موقعیت پنجره با نگه داشتن دکمه موس
        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }
    }
}