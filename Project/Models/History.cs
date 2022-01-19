using Project.Infrastructure;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Models
{
    public class History
    {
        [Key]
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public int UserId { get; set; }

        [ForeignKey(nameof(UserId))]
        public virtual User User { get; set; }
        public virtual List<VideoHistory> Videos { get; set; }

        public string PersianDate => Date.ToLongPresianDate();

        public History()
        {

        }
    }
}
