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
    /// Interaction logic for VideoDetailUserControl.xaml
    /// </summary>
    public partial class VideoDetailUserControl : UserControl
    {
        // سازنده
        public VideoDetailUserControl()
        {
            InitializeComponent();
        }

        // با این متد ویدئو به لیست نشان شده ها اضافه یا از آن حذف می شود
        private void BtnAddToFavorite_Click(object sender, RoutedEventArgs e)
        {
            var viewModel = DataContext as VideoDetailViewModel;
            var tag = ((Button)sender).Tag.ToString();
            var videoId = int.Parse(tag);
            viewModel.ToggleFavoriteVideo(videoId);
        }

        // این متد هنگامی اجرا می شود که روی یکی از ویدئوهای مشابه کلیک شود و به پنجره حزئیات آن ویدئو هدایت می شوید
        private void BtnVideoDetail_Click(object sender, RoutedEventArgs e)
        {
            var viewModel = DataContext as VideoDetailViewModel;
            var tag = ((Button)sender).Tag.ToString();
            var videoId = int.Parse(tag);
            viewModel.GoToVideoDetailWindow(videoId);
        }

        // این متد هنگامی فراخوانی می شود که UserControl به صورت کامل بارگذاری شده باشد
        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            var viewModel = DataContext as VideoDetailViewModel;
            viewModel.LoadData();
        }

        // با اجرای این متد به صفحه جستجو هدایت می شوید و متن جستجو شده عنوان ژانر کلیک شده می باشد
        private void BtnGenre_Click(object sender, RoutedEventArgs e)
        {
            var viewModel = DataContext as VideoDetailViewModel;
            var button = sender as Button;
            var searchText = button.Content.ToString();
            viewModel.GoToSearchWindow(searchText);
        }
    }
}