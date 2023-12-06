using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplication1.Models
{
    public class Menu
    {
        public int id { get; set; }
        public int? restaurantid { get; set; }
        [ForeignKey("restaurantid")]
        public Restaurant Restaurant { get; set; }
        public string dishname { get; set; }
        public int price { get; set; }
    }
}
