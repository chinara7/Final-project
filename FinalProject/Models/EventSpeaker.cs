using System.Collections.Generic;

namespace FinalProject.Models
{
    public class EventSpeaker
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Photo { get; set; }
        public string Profession { get; set; }
        public List<EventEventSpeaker> eventEventSpeakers { get; set; }
    }
}
