using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FinalProject.Models
{
    public class Event
    {
        public int ID { get; set; }
        [Required]
        public string Image { get; set; }
        [Required]
        public string Title { get; set; }
        public DateTime Date { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        [Required]
        public string Venue { get; set; }
        public EventDetail EventDetail { get; set; }
        public List<EventEventSpeaker> eventEventSpeakers { get; set; }
        public bool IsDeactive { get; set; }
        public DateTime? DeactivationTime { get; set; }
        public DateTime? CreatedTime { get; set; }
        [NotMapped]
        public IFormFile ImageFile { get; set; }
    }
}
