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

namespace Project.Admin.UserControls
{
    /// <summary>
    /// Interaction logic for VideosUserControl.xaml
    /// </summary>
    public partial class VideosUserControl : UserControl
    {
        // سازنده
        public VideosUserControl()
        {
            InitializeComponent();
        }

        // اضافه کردن یک ویدئو جدید
        private void BtnAdd_Click(object sender, RoutedEventArgs e)
        {
            ViewModel.GoToAddEditWindow(0);
        }

        // ویرایش یک ویدئو
        private void BtnEditGroup_Click(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;
            var tag = button.Tag.ToString();
            var videoId = int.Parse(tag);
            ViewModel.GoToAddEditWindow(videoId);
        }

        // حذف یک ویدئو
        private void BtnDeleteGroup_Click(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;
            var tag = button.Tag.ToString();
            var videoId = int.Parse(tag);
            ViewModel.DeleteVideo(videoId);
        }

        // این متد هنگامی اجرا می شود که مدیر اقدام به جستجو یک ویدئو کند
        private void TxtSearch_PreviewKeyUp(object sender, KeyEventArgs e)
        {
            string searchText = (sender as TextBox).Text;
            ViewModel.SearchText = searchText;
        }
    }
}