using Project.Data.AccessData;
using Project.Models;
using Project.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
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
    /// Interaction logic for VideoListUserControl.xaml
    /// </summary>
    public partial class VideoListUserControl : UserControl
    {
        // سازنده
        public VideoListUserControl()
        {
            InitializeComponent();
        }

        // این متد ویدئو را به لیست نشان شده اضافه یا از آن حذف می کند
        private void BtnBookmark_Click(object sender, RoutedEventArgs e)
        {
            var viewModel = DataContext as VideoListViewModel;
            var tag = (sender as ToggleButton).Tag.ToString();
            var videoId = int.Parse(tag);
            viewModel.ToggleFavoriteVideo(videoId);
        }

        // این متد هنگامی فراخوانی می شود که روی یک ویدئو از داخل لیست ویدئو کلیک شود
        private void LstVideos_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var viewModel = DataContext as VideoListViewModel;
            viewModel.GoToVideoDetailWindow();
        }

        // این متد هنگامی اجرا می شود که UserControl به صورت کامل بارگذاری شده باشد
        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            var viewModel = DataContext as VideoListViewModel;
            viewModel.LoadData();
        }

        // با کلیک روی صفحه بندی پایین لیست ویدئوها این متد فراخوانی می شود و به صفحه انتخاب شده هدایت می شوید
        private void BtnPagination_Click(object sender, RoutedEventArgs e)
        {
            var viewModel = DataContext as VideoListViewModel;
            var tag = (sender as RadioButton).Tag.ToString();
            var pageNumber = int.Parse(tag);
            viewModel.GetVideosByPage(pageNumber);
        }
    }
}