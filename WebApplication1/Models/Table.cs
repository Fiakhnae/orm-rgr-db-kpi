using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplication1.Models
{
    public class Table
    {
        public int id { get; set; }
        public int? restaurantid { get; set; }
        [ForeignKey("restaurantid")]
        public Restaurant Restaurant { get; set; }
        public int tablenumber { get; set; }
        public int seats { get; set; }
    }
}
