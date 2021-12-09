using System;

namespace FinalProject.Models
{
    public class Video
    {
        public int Id { get; set; }
        public string VideoLink { get; set; }
        public bool IsDeactive { get; set; }
        public DateTime? DeactivationTime { get; set; }
        public DateTime? CreatedTime { get; set; }
    }
}
