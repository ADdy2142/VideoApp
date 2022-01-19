using Project.Infrastructure;
using Project.Models;
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
    /// Interaction logic for NavigationWindow.xaml
    /// </summary>
    public partial class NavigationWindow : Window
    {
        // مشخص می کند که آیا پنجره نمایش داده شده است یا خیر
        public bool IsShown { get; set; } = false;

        // ساخت یک شی از کلاس Navigation برای انجام عملیات مربوط به Navigation
        private Navigation _navigation;

        public Navigation Navigation
        {
            get => _navigation;
            set
            {
                _navigation = value;
                LoadNewContent();
            }
        }

        // سازنده
        public NavigationWindow()
        {
            InitializeComponent();
        }

        // بازگشت به محتوی قبلی پنجره
        private void BtnBack_Click(object sender, RoutedEventArgs e)
        {
            BaseNavigation.Pop();
        }

        // تغییر موقعیت پنجره با نگه داشتن موس
        private void BrdNavbar_MouseDown(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }

        // بازگذاری محتوی جدید روی صفحه
        private void LoadNewContent()
        {
            GrdContent.Children.Clear();
            TxbWindowTitle.Text = Navigation.Title;
            GrdContent.Children.Add(Navigation.Control);
        }

        // هنگامی که پنجره به صورت کامل بارگذاری می شود این متد نیز فراخوانی می شود
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            IsShown = true;
        }
    }
}