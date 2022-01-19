using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Models
{
    public class Genre
    {
        [Key]
        public int Id { get; set; }
        public string Title { get; set; }

        public virtual ICollection<VideoGenre> Videos { get; set; }

        public Genre()
        {

        }
    }
}
