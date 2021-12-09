using System.ComponentModel.DataAnnotations.Schema;

namespace FinalProject.Models
{
    public class EventDetail
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public Event Event { get; set; }
        [ForeignKey(nameof(Event))]
        public int EventID { get; set; }
    }
}
