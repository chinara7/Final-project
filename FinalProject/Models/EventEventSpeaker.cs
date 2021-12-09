namespace FinalProject.Models
{
    public class EventEventSpeaker
    {
        public int Id { get; set; }

        public int EventID { get; set; }
        public Event Event { get; set; }

        public int EventSpeakerId { get; set; }
        public EventSpeaker EventSpeaker { get; set; }
    }
}
