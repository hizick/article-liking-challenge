using LikeButton.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LikeButton.Persistence
{
    public interface IRepository
    {
        Task<Article> GetArticleByIdAsync(int articleId);
        Task<bool> LikeUnlikeArticleAsync(Like like);
    }
}
