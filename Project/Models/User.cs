using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace Project.Models
{
    public class User
    {
        [Key]
        public int Id { get; set; }

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Image { get; set; }
        public string Role { get; set; }

        public virtual ICollection<UserVideo> FavoriteVideos { get; set; }
        public virtual ICollection<History> Histories { get; set; }

        public BitmapImage BitmapImage => new BitmapImage(new Uri($"{Environment.CurrentDirectory}\\Images\\{Image}.png"));
        public bool IsAdmin => Role == "Admin";

        public User()
        {
        }
    }
}