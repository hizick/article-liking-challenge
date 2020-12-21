using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace LikeButton.Models
{
    [Table("Article")] //all entity configurations are done with ef core fluent api in the persitence folder
    public class Article
    {
        public int ArticleId { get; set; } 
        public string Title { get; set; }
        public string ArticleText { get; set; }
        public List<Like> Likes { get; set; }
        public Article()
        {
            Likes = new List<Like>();
        }
    }
}
