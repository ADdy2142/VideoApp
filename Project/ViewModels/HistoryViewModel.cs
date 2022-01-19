using Project.Infrastructure;
using Project.Models;
using Project.UserControls;
using Project.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.ViewModels
{
    public class HistoryViewModel : BaseViewModel
    {
        // لیست قابل نمایش از تاریخچه یک کاربر
        private ObservableCollection<History> _histories = new ObservableCollection<History>();

        public ObservableCollection<History> Histories
        {
            get => _histories;
            set
            {
                _histories = value;
                OnPropertyChanged(nameof(Histories));
            }
        }

        // سازنده
        public HistoryViewModel()
        {
            GetHistories();
        }

        // بارگذاری تاریخچه کاربر از پایگاه داده و ذخیره آن در لیست تاریخچه
        private async void GetHistories()
        {
            Histories = await AccessData.Histories.GetAllAsync(UserId);
        }

        // رفتن به پنجره جزئیات ویدئو
        public async void GoToVideoDetailWindow(int videoId)
        {
            var video = await AccessData.Videos.GetAsync(videoId);
            var dataContext = new VideoDetailViewModel() { Video = video };
            var userControl = new VideoDetailUserControl() { DataContext = dataContext };
            BaseNavigation.Push($"مشخصات {video.Title}", userControl);
        }

        // افزودن ویدوئو یا حذف آن از لیست نشان شده ها
        public async void ToggleFavoriteVideo(int videoId)
        {
            var video = await AccessData.Videos.GetAsync(videoId);
            if (video == null)
                return;

            if (CurrentUser == null)
                return;

            int userId = Properties.Settings.Default.UserId;
            if (await AccessData.UserVideos.IsFavoriteForUserAsync(userId, video.Id))
            {
                var userVideo = await AccessData.UserVideos.GetAsync(userId, video.Id);
                AccessData.UserVideos.Delete(userVideo);
            }
            else
            {
                var userVideo = new UserVideo() { UserId = userId, VideoId = video.Id };
                AccessData.UserVideos.Add(userVideo);
            }

            await AccessData.SaveAsync();
        }
    }
}