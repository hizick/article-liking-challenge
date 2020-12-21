using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LikeButton.ViewModels
{
    public class HomeViewModel
    {
        public string ArticleText { get; set; }
        public int ArticleId { get; set; }
        public long LikeCount { get; set; }
        public bool LikedByUser { get; set; }
        public string UserId { get; set; }
    }

    public class Data
    {
        public bool liked { get; set; }
        public bool status { get; set; }
    }
}
