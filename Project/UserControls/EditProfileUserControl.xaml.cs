using Project.ViewModels;
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

namespace Project.UserControls
{
    /// <summary>
    /// Interaction logic for EditProfileUserControl.xaml
    /// </summary>
    public partial class EditProfileUserControl : UserControl
    {
        public EditProfileUserControl()
        {
            InitializeComponent();
        }

        // با اجرای این متد اطلاعات شخصی کاربر ویرایش می شود
        private void SavePersonalInfo_Click(object sender, RoutedEventArgs e)
        {
            ViewModel.UpdatePersonalInfo();
        }

        // با اجرای این متد کلمه عبور کاربر ذخیره می شود
        private void SavePassword_Click(object sender, RoutedEventArgs e)
        {
            ViewModel.UpdatePassword();
            PsbOldPassword.Password = string.Empty;
            PsbNewPassword.Password = string.Empty;
            PsbConfirmNewPassword.Password = string.Empty;
        }

        // این متد هنگامی اجرا می شود که مقدار فیلد کلمه عبور قدیمی در پنجره ویرایش پروفایل تغییر یابد
        private void PsbOldPassword_PasswordChanged(object sender, RoutedEventArgs e)
        {
            ViewModel.OldPassword = PsbOldPassword.Password;
        }

        // این متد هنگامی اجرا می شود که مقدار فیلد کلمه عبور جدید در پنجره ویرایش پروفایل تغییر یابد
        private void PsbNewPassword_PasswordChanged(object sender, RoutedEventArgs e)
        {
            ViewModel.NewPassword = PsbNewPassword.Password;
        }

        // این متد هنگامی اجرا می شود که مقدار فیلد تکرار کلمه عبور جدید در پنجره ویرایش پروفایل تغییر یابد
        private void PsbConfirmNewPassword_PasswordChanged(object sender, RoutedEventArgs e)
        {
            ViewModel.ConfirmNewPassword = PsbConfirmNewPassword.Password;
        }
    }
}