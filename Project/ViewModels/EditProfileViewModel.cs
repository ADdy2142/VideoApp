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
    public class EditProfileViewModel : BaseViewModel
    {
        // ایمیل
        public string Email { get; set; }

        // نام
        private string _firstName = string.Empty;

        public string FirstName
        {
            get => _firstName;
            set
            {
                _firstName = value;
                OnPropertyChanged(FirstName);
            }
        }

        // نام خانوادگی
        private string _lastName = string.Empty;

        public string LastName
        {
            get => _lastName;
            set
            {
                _lastName = value;
                OnPropertyChanged(LastName);
            }
        }

        // نام کاربری
        private string _userName = string.Empty;

        public string UserName
        {
            get => _userName;
            set
            {
                _userName = value;
                OnPropertyChanged(UserName);
            }
        }

        // کلمه عبور قدیمی
        private string _oldPassword = string.Empty;

        public string OldPassword
        {
            get => _oldPassword;
            set
            {
                _oldPassword = value;
                OnPropertyChanged(nameof(OldPassword));
            }
        }

        // کلمه عبور جدید
        private string _newPassword = string.Empty;

        public string NewPassword
        {
            get => _newPassword;
            set
            {
                _newPassword = value;
                OnPropertyChanged(nameof(NewPassword));
            }
        }

        // تکرار کلمه عبور جدید
        private string _confirmNewPassword = string.Empty;

        public string ConfirmNewPassword
        {
            get => _confirmNewPassword;
            set
            {
                _confirmNewPassword = value;
                OnPropertyChanged(nameof(ConfirmNewPassword));
            }
        }

        // سازنده
        public EditProfileViewModel()
        {
            FirstName = CurrentUser.FirstName;
            LastName = CurrentUser.LastName;
            Email = CurrentUser.Email;
            UserName = CurrentUser.UserName;
        }

        // ویرایش اطلاعات شخصی
        public async void UpdatePersonalInfo()
        {
            if (string.IsNullOrEmpty(FirstName))
            {
                ShowAlert("اطلاعات شما کامل نیست.", "لطفا نام خود را وارد کنید.", AlertWindowType.Info);
                return;
            }

            if (string.IsNullOrEmpty(LastName))
            {
                ShowAlert("اطلاعات شما کامل نیست.", "لطفا نام خانوادگی خود را وارد کنید.", AlertWindowType.Info);
                return;
            }

            if (string.IsNullOrEmpty(UserName))
            {
                ShowAlert("اطلاعات شما کامل نیست.", "لطفا نام کاربری خود را وارد کنید.", AlertWindowType.Info);
                return;
            }

            var user = await AccessData.Users.GetAsync(CurrentUser.Id);
            user.FirstName = FirstName;
            user.LastName = LastName;
            user.UserName = UserName;

            AccessData.Users.Update(user);
            await AccessData.SaveAsync();

            ShowAlert("عملیات با موفقیت انجام شد.", "اطلاعات شخصی شما با موفقیت تغییر یافت.", AlertWindowType.Success);
        }

        // ویرایش کلمه عبور
        public async void UpdatePassword()
        {
            if (string.IsNullOrEmpty(OldPassword))
            {
                ShowAlert("اطلاعات شما کامل نیست.", "لطفا کلمه عبور قبلی خود را وارد کنید.", AlertWindowType.Info);
                return;
            }

            if (!await AccessData.Users.CheckOldPasswordAsync(UserId, OldPassword))
            {
                ShowAlert("کلمه عبور صحیح نمی باشد.", "لطفا از درستی کلمه عبور خود اطمینان حاصل کنید.", AlertWindowType.Info);
                return;
            }

            if (string.IsNullOrEmpty(NewPassword))
            {
                ShowAlert("اطلاعات شما کامل نیست.", "لطفا کلمه عبور جدید خود را وارد کنید.", AlertWindowType.Info);
                return;
            }

            if (!NewPassword.Equals(ConfirmNewPassword))
            {
                ShowAlert("اطلاعات شما تطابق ندارد.", "لطفا کلمه عبور جدید خود را بررسی کنید.", AlertWindowType.Info);
                return;
            }

            var result = ShowAlert("تغییر کلمه عبور", "آیا از تغییر کلمه عبور خود اطمینان دارید؟", AlertWindowType.Warning);
            if (result == true)
            {
                CurrentUser.Password = NewPassword;

                AccessData.Users.Update(CurrentUser);
                await AccessData.SaveAsync();

                ShowAlert("عملیات با موفقیت انجام شد.", "کلمه عبور شما با موفقیت تغییر یافت.", AlertWindowType.Success);
            }
        }
    }
}