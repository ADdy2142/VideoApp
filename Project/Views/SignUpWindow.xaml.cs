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
    /// Interaction logic for SignUpWindow.xaml
    /// </summary>
    public partial class SignUpWindow : Window
    {
        // سازنده
        public SignUpWindow()
        {
            InitializeComponent();
        }

        // انجام عملیات ثبت نام
        private void BtnSignUp_Click(object sender, RoutedEventArgs e)
        {
            ViewModel.SignUpUser();
        }

        // هدایت به پنجره ورود
        private void BtnSignIn_Click(object sender, RoutedEventArgs e)
        {
            ViewModel.GoToSignInWindow();
        }

        // اتمام کار برنامه و بستن آن
        private void BtnClose_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        // تغییر مقدار موجود در ViewModel با تغییر مقدار آن در پنجره
        private void PsbPassword_PasswordChanged(object sender, RoutedEventArgs e)
        {
            ViewModel.Password = PsbPassword.Password;
        }

        // تغییر مقدار موجود در ViewModel با تغییر مقدار آن در پنجره
        private void PsbConfirmPassword_PasswordChanged(object sender, RoutedEventArgs e)
        {
            ViewModel.ConfirmPassword = PsbConfirmPassword.Password;
        }

        // این متد هنگامی اجرا می شود که پنجره به صورت کامل بارگذاری شده باشد
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            TxtEmail.Focus();
            ViewModel.Owner = this;
        }

        // تغییر موقعیت پنجره با نگه داشتن موس کلیک موس
        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }
    }
}