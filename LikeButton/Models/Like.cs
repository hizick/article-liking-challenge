using System.ComponentModel.DataAnnotations.Schema;

namespace LikeButton.Models
{
    [Table("Like")] //all entity configurations are done with fluent api in the persitence folder
    public class Like
    {
        public int ArticleId { get; set; }
        public string UserId { get; set; }
        public bool Liked { get; set; }
        
    }
}
