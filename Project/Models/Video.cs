using Project.Data.AccessData;
using Project.Data.Context;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Models
{
    public enum OrderVideosBy : byte
    {
        Newest = 0,
        MostVisited = 1,
        Favorite = 2
    }

    public class Video
    {
        [Key]
        public int Id { get; set; }

        public string Title { get; set; }
        public int GroupId { get; set; }
        public string Season { get; set; }
        public string SeasonState { get; set; }
        public string PublishYear { get; set; }
        public double Score { get; set; }
        public int Visits { get; set; }
        public string Description { get; set; }
        public int Duration { get; set; }
        public string ImageUrl { get; set; }

        [ForeignKey(nameof(GroupId))]
        public virtual Group Group { get; set; }

        public virtual List<VideoGenre> Genres { get; set; }
        public virtual List<UserVideo> FavoriteForUsers { get; set; }
        public virtual List<VideoHistory> Histories { get; set; }

        public bool IsSeries => !string.IsNullOrEmpty(Season);
        public bool IsFavorite => CheckIsFavorite().Result;

        public Video()
        {
        }

        private async Task<bool> CheckIsFavorite()
        {
            bool isFavorite = false;
            using (IAccessData accessData = new AccessData())
            {
                int userId = Properties.Settings.Default.UserId;
                isFavorite = await accessData.UserVideos.IsFavoriteForUserAsync(userId, Id);
            }

            return isFavorite;
        }
    }
}