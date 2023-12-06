using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplication1.Models
{
    public class TableReservation
    {
        public int id { get; set; }
        public int? tableid { get; set; }
        [ForeignKey("tableid")]
        public Table Table { get; set; }
        public int? userid { get; set; }
        [ForeignKey("userid")]
        public User User { get; set; }
        public DateTime reservationdatetime { get; set; }
    }
}
