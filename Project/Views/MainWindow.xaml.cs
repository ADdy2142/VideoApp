using Project.Models;
using Project.UserControls;
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
using System.Windows.Shapes;

namespace Project.Views
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private int _currentContentId = -1;

        public MainWindow()
        {
            InitializeComponent();
        }

        // کوچک کردن پنجره و مخفی کردن آن در تسک بار
        private void BtnMinimize_Click(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }

        // این متد پنجره را تمام صفحه می کند
        private void BtnMaxNorm_Click(object sender, RoutedEventArgs e)
        {
            this.WindowState = this.WindowState == WindowState.Normal ? WindowState.Maximized : WindowState.Normal;
        }

        // با اجرای این متد اجرای برنامه خاتمه می یابد
        private void BtnClose_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        // از این متد برای جابجایی پنجره با موس استفاده می شود
        private void BrdNavBar_MouseDown(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }

        // از این متد برای تغییر محتوای صفحه با کلیک روی دکمه های موجود در نوار ناوبری استفاده می شود
        private async void NavigationButton_Click(object sender, RoutedEventArgs e)
        {
            var button = sender as RadioButton;
            var tag = button.Tag.ToString();
            int contentId = int.Parse(tag);
            if (contentId == _currentContentId)
                return;

            _currentContentId = contentId;
            await LoadContentAsync(contentId);
        }

        // محتوای جدید را بارگذاری می کند
        private async Task LoadContentAsync(int contentId)
        {
            await Task.Run(() =>
            {
                Dispatcher.Invoke(() =>
                {
                    GrdContent.Children.Clear();

                    switch (contentId)
                    {
                        case 0:
                            GrdContent.Children.Add(new VideoPanelUserControl(OrderVideosBy.Newest));
                            break;

                        case 1:
                            GrdContent.Children.Add(new VideoPanelUserControl(OrderVideosBy.MostVisited));
                            break;

                        case 2:
                            var dataContext = new VideoListViewModel() { OrderVideosBy = OrderVideosBy.Favorite };
                            GrdContent.Children.Add(new VideoListUserControl() { DataContext = dataContext, Padding = new Thickness(5, 20, 20, 20) });
                            break;
                    }
                });
            });
        }

        // این متد هنگامی اجرا می شود که پنجره به صورت کامل بارگذاری شده باشد
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            NavigationButton_Click(new RadioButton() { Tag = "0" }, null);
            ViewModel.Owner = this;
        }

        // این متد مربوط به بستن محتوی جستجو است
        private void GrdSearch_MouseDown(object sender, MouseButtonEventArgs e)
        {
            TxtSearch.Text = string.Empty;
            BtnToggleSearch.IsChecked = false;
        }

        // با اجرای این متد به پنجره جستجو هدایت می شوید
        private void BtnSearch_Click(object sender, RoutedEventArgs e)
        {
            ViewModel.GoToSearchWindow(TxtSearch.Text);
            BtnToggleSearch.IsChecked = false;
        }

        // برای ویرایش پروفایل استفاده می شود
        private void BtnEditProfile_Click(object sender, RoutedEventArgs e)
        {
            ViewModel.GoToEditProfileWindow();
        }

        // هدایت به پنجره تاریخچه
        private void BtnHistory_Click(object sender, RoutedEventArgs e)
        {
            ViewModel.GoToHistoryWindow();
        }

        // خروج از حساب کاربری
        private void BtnSignOut_Click(object sender, RoutedEventArgs e)
        {
            ViewModel.SignOutUser();
        }

        // بستن محتوی جسجتو
        private void BrdMainContent_PreviewMouseUp(object sender, MouseButtonEventArgs e)
        {
            if (BtnToggleProfileMenu.IsChecked == true)
                BtnToggleProfileMenu.IsChecked = false;
        }

        // جستجو با زدن کلید اینتر
        private void TxtSearch_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
                BtnSearch_Click(null, null);
        }
    }
}