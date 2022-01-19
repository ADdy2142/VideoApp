using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Models
{
    public class Avatar
    {
        public string Name { get; set; }
        public bool IsSelected { get; set; }

        public string ImageUrl => $"{Environment.CurrentDirectory}\\Images\\{Name}.png";
    }
}