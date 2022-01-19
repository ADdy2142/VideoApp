using Project.Infrastructure;
using Project.Models;
using Project.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Project.ViewModels
{
    public class SignUpViewModel : BaseViewModel
    {
        // مالک یا دارنده این ViewModel
        public Window Owner { get; set; }

        // تصویر پروفایل انتخاب شده
        public Avatar SelectedAvatar { get; set; }

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

        // تایید کلمه عبور
        private string _confirmPassword = string.Empty;

        public string ConfirmPassword
        {
            get => _confirmPassword;
            set
            {
                _confirmPassword = value;
                OnPropertyChanged(nameof(ConfirmPassword));
            }
        }

        // لیست قابل نمایش از تصاویر پروفایل برای انتخاب آن توسط کاربر
        private ObservableCollection<Avatar> _avatars = new ObservableCollection<Avatar>();

        public ObservableCollection<Avatar> Avatars
        {
            get => _avatars;
            set
            {
                _avatars = value;
                OnPropertyChanged(nameof(Avatars));
            }
        }

        // سازنده
        public SignUpViewModel()
        {
            GetAvatars();
        }

        // تمام تصاویر پروفایل را در لیست موجود ذخیره می کند
        private void GetAvatars()
        {
            Avatars = AccessData.Avatars.GetAll();
        }

        // انجام عملیات ثبت نام و ثبت کاربر جدید در پایگاه داده
        public async void SignUpUser()
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

            if (Password.Length < 8)
            {
                ShowAlert("کلمه عبور شما کوتاه است.", "کلمه عبور حداقل باید 8 کاراکتر باشد.", AlertWindowType.Danger);
                return;
            }

            if (!Password.Equals(ConfirmPassword))
            {
                ShowAlert("اطلاعات شما تطابق ندارد.", "لطفا کلمه عبور خود را بررسی کنید.", AlertWindowType.Info);
                return;
            }

            if (await AccessData.Users.IsEmailExistAsync(Email))
            {
                ShowAlert("ایمیل خود را تغییر دهید.", "این ایمیل قبلا استفاده شده.", AlertWindowType.Danger);
                return;
            }

            var user = new User()
            {
                Email = Email,
                Password = Password,
                Image = SelectedAvatar.Name
            };

            var result = AccessData.Users.SignUp(user);
            if (result)
            {
                await AccessData.SaveAsync();
                ShowAlert("خوش آمدید.", "ثبت نام شما موفقیت آمیز بود.", AlertWindowType.Success);
                GoToSignInWindow();
            }
            else
                ShowAlert("ثبت نام با خطا مواجه شد.", "مشکلی هنگام ثبت نام به وجود آمد.", AlertWindowType.Danger);
        }

        // رفتن به پنجره ورود کاربران
        public void GoToSignInWindow()
        {
            Owner.DialogResult = true;
        }
    }
}