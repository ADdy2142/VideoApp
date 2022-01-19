using Project.Infrastructure;
using Project.Models;
using Project.UserControls;
using Project.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media.Imaging;

namespace Project.ViewModels
{
    public class MainViewModel : BaseViewModel
    {
        // پنجره دارنده این ViewModel
        public Window Owner { get; set; }

        // نام کاربری
        private string _userName = string.Empty;

        public string UserName
        {
            get => _userName;
            set
            {
                _userName = value;
                OnPropertyChanged(nameof(UserName));
            }
        }

        // تصویر پروفایل کاربر
        private BitmapImage _userImage = new BitmapImage();

        public BitmapImage UserImage
        {
            get => _userImage;
            set
            {
                _userImage = value;
                OnPropertyChanged(nameof(UserImage));
            }
        }

        // سازنده
        public MainViewModel()
        {
            LoadData();
        }

        // بارگذاری اطلاعات کاربر
        private void LoadData()
        {
            if (CurrentUser == null)
                return;

            UserName = string.IsNullOrEmpty(CurrentUser.UserName) ? CurrentUser.Email : CurrentUser.UserName;
            UserImage = CurrentUser.BitmapImage;
        }

        // خروج کاربر از حساب کاربری
        public void SignOutUser()
        {
            Properties.Settings.Default.UserId = 0;
            Properties.Settings.Default.Save();

            Owner.DialogResult = true;
        }

        // هدایت به پنجره جستجو
        public void GoToSearchWindow(string searchText)
        {
            var dataContext = new SearchViewModel() { SearchText = searchText };
            var userControl = new SearchUserControl() { DataContext = dataContext };
            BaseNavigation.Push("نتایج جستجو", userControl);
        }

        // هدایت کاربر به پنجره ویرایش پروفایل
        public void GoToEditProfileWindow()
        {
            var userControl = new EditProfileUserControl();
            BaseNavigation.Push("ویرایش پروفایل", userControl);

            ReloadEntity(CurrentUser);
            LoadData();
        }

        // هدایت کاربر به پنجره تاریخچه
        public void GoToHistoryWindow()
        {
            var userControl = new HistoryUserControl();
            BaseNavigation.Push("تاریخچه", userControl);
        }
    }
}