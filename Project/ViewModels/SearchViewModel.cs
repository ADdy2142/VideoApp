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
    public class SearchViewModel : BaseViewModel
    {
        // متن جستجو شده
        public string SearchText { get; set; }

        // لیست قابل نمایش از ویدئوها
        private ObservableCollection<Video> _videos = new ObservableCollection<Video>();

        public ObservableCollection<Video> Videos
        {
            get => _videos;
            set
            {
                _videos = value;
                OnPropertyChanged(nameof(Videos));
            }
        }

        // ویدئو انتخاب شده جهت نمایش
        private Video _selectedVideo = new Video();

        public Video SelectedVideo
        {
            get => _selectedVideo;
            set
            {
                _selectedVideo = value;
                OnPropertyChanged(nameof(SelectedVideo));
            }
        }

        // جسجتو بین ویدئوها برای به دست آوردن ویدئوهای مشابه با متن جستجو شده
        public async void Search()
        {
            Videos = await AccessData.Videos.GetVideosAsync(SearchText.ToLower());
        }

        // هدایت به پنجره جرئیات ویدئو
        public async void GoToVideoDetailWindow()
        {
            if (SelectedVideo == null)
                return;

            var video = await AccessData.Videos.GetAsync(SelectedVideo.Id);
            var dataContext = new VideoDetailViewModel() { Video = video };
            var userControl = new VideoDetailUserControl() { DataContext = dataContext };
            BaseNavigation.Push($"مشخصات {SelectedVideo.Title}", userControl);
            SelectedVideo = null;
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