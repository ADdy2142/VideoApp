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
    public class VideoDetailViewModel : BaseViewModel
    {
        // یک شی از نوع Video که قرار است اطلاعات آن در صفحه جزئیات ویدئو نمایش داده شود
        private Video _video = new Video();

        public Video Video
        {
            get => _video;
            set
            {
                _video = value;
                OnPropertyChanged(nameof(Video));
            }
        }

        // تعیین اینکه ویدئو جزوی از نشان شده ها می باشد یا خیر
        private string _favoriteState = string.Empty;

        public string FavoriteState
        {
            get => _favoriteState;
            set
            {
                _favoriteState = value;
                OnPropertyChanged(nameof(FavoriteState));
            }
        }

        // لیست قابل نمایش از ویدئوهای مشابه با ویدئویی که جزئیات آن به نمایش در آمده
        private ObservableCollection<Video> _similarVideos = new ObservableCollection<Video>();

        public ObservableCollection<Video> SimilarVideos
        {
            get => _similarVideos;
            set
            {
                _similarVideos = value;
                OnPropertyChanged(nameof(SimilarVideos));
            }
        }

        // بارگذاری داده های مربوط
        public void LoadData()
        {
            GetSimilarVideos();
            CheckUserFavorites();
            AddVideoToHistory();
            UpdateVideoVisits();
        }

        // تغییر میزان بازدید ویدئو هنگام مشاهده جزئیات ویدئو
        private async void UpdateVideoVisits()
        {
            var video = await AccessData.Videos.GetAsync(Video.Id);
            if (video == null)
                return;

            video.Visits += 1;
            AccessData.Videos.Update(video);
            await AccessData.SaveAsync();

            ReloadEntity(video);
        }

        // پیدا کردن ویدئوهای مشابه و ذخیره کردن آنها در لیست ویدئوها
        private async void GetSimilarVideos()
        {
            SimilarVideos = await AccessData.Videos.GetSimilarVideosAsync(Video);
        }

        // اضافه کردن ویدئو به تاریخچه
        private async void AddVideoToHistory()
        {
            if (CurrentUser.Histories.Count == 0)
            {
                var history = new History() { Date = DateTime.Now, User = CurrentUser };
                AccessData.Histories.Add(history);
                await AccessData.SaveAsync();
            }

            var lastHistory = CurrentUser.Histories.LastOrDefault();
            var date = lastHistory.Date.ToShortDateString();
            if (date == DateTime.Now.ToShortDateString())
            {
                var history = await AccessData.Histories.GetAsync(lastHistory.Id);
                if (history.Videos.Any(vh => vh.VideoId == Video.Id && vh.HistoryId == history.Id))
                {
                    var videoHistory = history.Videos.FirstOrDefault(vh => vh.VideoId == Video.Id && vh.HistoryId == history.Id);
                    AccessData.VideoHistories.Delete(videoHistory);
                    await AccessData.SaveAsync();

                    videoHistory = new VideoHistory() { VideoId = Video.Id, HistoryId = history.Id };
                    AccessData.VideoHistories.Add(videoHistory);
                    await AccessData.SaveAsync();
                }
                else
                {
                    var videoHistory = new VideoHistory() { VideoId = Video.Id, HistoryId = history.Id };
                    AccessData.VideoHistories.Add(videoHistory);
                    await AccessData.SaveAsync();
                }
            }
            else
            {
                var history = new History() { Date = DateTime.Now, User = CurrentUser };
                AccessData.Histories.Add(history);
                await AccessData.SaveAsync();

                var videoHistory = new VideoHistory() { VideoId = Video.Id, HistoryId = history.Id };
                AccessData.VideoHistories.Add(videoHistory);
                await AccessData.SaveAsync();
            }
        }

        // بررسی ویدوئو و تعیین اینکه آیا ویدئو در لیست نشان شده های کاربر می باشد یا خیر
        private async void CheckUserFavorites()
        {
            var isFavorite = await AccessData.Videos.IsFavorite(UserId, Video.Id);
            FavoriteState = isFavorite ? "حذف از علاقه مندی ها" : "افزودن به علاقه مندی ها";
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

            CheckUserFavorites();
        }

        // هدایت به پنجره جزئیات ویدئو هنگام کلیک روی ویدئوهای مشابه در پنجره جزئیات ویدئو
        public async void GoToVideoDetailWindow(int videoId)
        {
            var video = await AccessData.Videos.GetAsync(videoId);
            var dataContext = new VideoDetailViewModel() { Video = video };
            var userControl = new VideoDetailUserControl() { DataContext = dataContext };
            BaseNavigation.Push($"مشخصات {video.Title}", userControl);
        }

        // هدایت به پنجره جستجو هنگام کلیک روی ژانرهای موجود در پنجره جزئیات ویدئو
        public void GoToSearchWindow(string searchText)
        {
            var dataContext = new SearchViewModel() { SearchText = searchText };
            var userControl = new SearchUserControl() { DataContext = dataContext };
            BaseNavigation.Push("نتایج جستجو", userControl);
        }
    }
}