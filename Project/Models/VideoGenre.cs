using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Models
{
    public class VideoGenre
    {
        [Key, Column(Order = 1)]
        public int VideoId { get; set; }
        [Key, Column(Order = 2)]
        public int GenreId { get; set; }

        [ForeignKey(nameof(VideoId))]
        public virtual Video Video { get; set; }
        [ForeignKey(nameof(GenreId))]
        public virtual Genre Genre { get; set; }

        public VideoGenre()
        {

        }
    }
}
