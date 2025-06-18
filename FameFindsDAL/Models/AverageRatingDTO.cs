using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FameFindsDAL.Models
{
    public class AverageRatingDTO
    {
        public int ShopId { get; set; }
        public double AverageRating { get; set; }
        public int TotalRatings { get; set; }
    }

}
