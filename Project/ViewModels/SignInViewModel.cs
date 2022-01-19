using Project.Infrastructure;
using Project.Models;
using Project.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Project.ViewModels
{
    public class SignInViewModel : BaseViewModel
    {
        // ایمیل
        public string Email { get; set; }

        // کلمه عبور
        private string _password = string.Empty;

        public string Password
        {
            get => _password;
            set
            {
                _password = value;
                OnPropertyChanged(nameof(Password));
            }
        }

        // انجام عملیات ورود به برنامه و ثبت کاربر
        public async void SignInUser()
        {
            if (string.IsNullOrEmpty(Email))
            {
                ShowAlert("اطلاعات شما کامل نیست.", "لطفا ایمیل خود را وارد کنید.", AlertWindowType.Info);
                return;
            }

            if (string.IsNullOrEmpty(Password))
            {
                ShowAlert("اطلاعات شما کامل نیست.", "لطفا کلمه عبور خود را وارد کنید.", AlertWindowType.Info);
                return;
            }

            var user = await AccessData.Users.SignInAsync(Email, Password);
            if (user == null)
            {
                ShowAlert("اطلاعات وارد شده صحیح نمی باشد.", "نام کاربری یا کلمه عبور اشتباه است.", AlertWindowType.Danger);
                return;
            }

            if (user.IsAdmin)
            {
                GoToAdminWindow();
            }
            else
            {
                Properties.Settings.Default.UserId = user.Id;
                Properties.Settings.Default.Save();
                GoToMainWindow();
            }
        }

        // هدایت به پنجره مدیریت
        private void GoToAdminWindow()
        {
            var window = new Admin.Views.MainWindow();
            GoToWindow(window);
        }

        // هدایت به پنجره اصلی
        private void GoToMainWindow()
        {
            var window = new MainWindow();
            GoToWindow(window);
        }

        // هدایت به پنجره ثبت نام
        public void GoToSignUpWindow()
        {
            var window = new SignUpWindow();
            GoToWindow(window);
        }

        // هدایت به پنجره ای که به عنوان ورودی دریافت می کند
        private void GoToWindow(Window window)
        {
            var mainWindow = Application.Current.MainWindow;
            mainWindow.Hide();
            window.ShowDialog();
            mainWindow.Show();
        }
    }
}