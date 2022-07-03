using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Calipso.Models
{
    public class Review
    {
        public int reviewId { set; get; }
        [Required]
        public int userId { set; get; }
        [Required]
        public int place_id { set; get; }

        public string experience { set; get; }
        public int love { set; get; }
        public int rating { set; get; }
        public int wifi { set; get; }
        public int cost { set; get; }
        public int fun { set; get; }
        public int air_quanity { set; get; }
        public int people_desity { set; get; }
        public int trafic { set; get; }
        public int huminity { set; get; }
    }
}
