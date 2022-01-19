using Project.Models;
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
    /// Interaction logic for HistoryUserControl.xaml
    /// </summary>
    public partial class HistoryUserControl : UserControl
    {
        // سازنده
        public HistoryUserControl()
        {
            InitializeComponent();
        }

        // متد افزودن یا حذف ویدتو از لیست نشان شده ها
        private void BtnBookmark_Click(object sender, RoutedEventArgs e)
        {
            var tag = ((ToggleButton)sender).Tag.ToString();
            var videoId = int.Parse(tag);
            ViewModel.ToggleFavoriteVideo(videoId);
        }

        // این متد هنگامی فراخوانی می شود که در ویدئویی در لیست تاریخچه انتخاب شود یا روی آن کلیک شود
        private void LstVideos_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var list = sender as ListBox;
            if (list.SelectedItem == null)
                return;

            var videoHistory = list.SelectedItem as VideoHistory;
            ViewModel.GoToVideoDetailWindow(videoHistory.Video.Id);
            list.SelectedItem = null;
        }
    }
}