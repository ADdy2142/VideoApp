using Project.ViewModels;
using System;
using System.Collections.Generic;
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
    /// Interaction logic for SearchUserControl.xaml
    /// </summary>
    public partial class SearchUserControl : UserControl
    {
        // متن جستجو شده توسط کاربر
        private string _searchText = string.Empty;

        //سازنده
        public SearchUserControl()
        {
            InitializeComponent();
        }

        // با کلیک روی دکمه جستجو در پنجره نتایج جستجو این متد فراخوانی می شود
        private void BtnSearch_Click(object sender, RoutedEventArgs e)
        {
            var viewModel = DataContext as SearchViewModel;
            viewModel.SearchText = _searchText;
            viewModel.Search();
        }

        // این متد هنگامی فراخوانی می شود که ویدئویی از لیست ویدئوها انتخاب شود یعنی روی آن کلیک شود
        private void LstVideos_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var viewModel = DataContext as SearchViewModel;
            viewModel.GoToVideoDetailWindow();
        }

        // با اجرای این متد ویدئو به لیست نشان شده ها اضافه می شود و در صورتی که در لیست وجود داشت حذف می شود
        private void BtnBookmark_Click(object sender, RoutedEventArgs e)
        {
            var viewModel = DataContext as SearchViewModel;
            var tag = (sender as ToggleButton).Tag.ToString();
            int videoId = int.Parse(tag);
            viewModel.ToggleFavoriteVideo(videoId);
        }

        // این متد هنگامی فراخوانی می شود که متن درون فیلد جسجتو تغییر کند
        private void TxtSearch_PreviewKeyUp(object sender, KeyEventArgs e)
        {
            _searchText = TxtSearch.Text;
            BtnSearch_Click(null, null);
        }

        // این متد هنگامی فرخوانی می شود که UserControl به صورت کامل بارگذاری شده باشد
        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            var viewModel = DataContext as SearchViewModel;
            TxtSearch.Text = viewModel.SearchText;
            viewModel.Search();
        }
    }
}