using Project.Infrastructure;
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
    /// Interaction logic for VideoPanelUserControl.xaml
    /// </summary>
    public partial class VideoPanelUserControl : UserControl
    {
        // سازنده
        public VideoPanelUserControl(OrderVideosBy orderVideosBy)
        {
            InitializeComponent();

            var dataContext = new VideoListViewModel() { OrderVideosBy = orderVideosBy };
            var control = new VideoListUserControl() { DataContext = dataContext };
            GrdVideos.Children.Add(control);
        }

        // این متد هنگامی فراخوانی می شود که کاربر یک گروه یا ژانر را برای فیلتر کردن ویدئوها انتخاب کند
        private void CtlFilters_FilterChanged(object sender, EventArgs e)
        {
            var groupIds = CtlFilters.GroupIds;
            var genreIds = CtlFilters.GenreIds;
            var videoListUserControl = GrdVideos.Children[0] as VideoListUserControl;
            var videoListViewModel = videoListUserControl.DataContext as VideoListViewModel;
            videoListViewModel.FilterGroupIds = groupIds;
            videoListViewModel.FilterGenreIds = genreIds;
            videoListViewModel.GetVideosByPage();
        }
    }
}