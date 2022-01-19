using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Models
{
    public class UserVideo
    {
        [Key, Column(Order = 1)]
        public int UserId { get; set; }
        [Key, Column(Order = 2)]
        public int VideoId { get; set; }

        [ForeignKey(nameof(UserId))]
        public virtual User User { get; set; }
        [ForeignKey(nameof(VideoId))]
        public virtual Video Video { get; set; }

        public UserVideo()
        {

        }
    }
}
