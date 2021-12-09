using System.ComponentModel.DataAnnotations.Schema;

namespace FinalProject.Models
{
    public class BlogDetail
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public Blog Blog { get; set; }
        [ForeignKey("Blog")]
        public int BlogId { get; set; }
    }
}
