using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace LikeButton.Models
{
    [Table("User")] //all entity configurations are done with fluent api in the persitence folder
    public class User
    {
        public string UserId { get; set; }
        public List<Like> Likes { get; set; }

        //public string Password { get; set; } //this is not important for this system right now
    }
}
