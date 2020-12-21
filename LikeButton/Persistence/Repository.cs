using LikeButton.Models;
using LikeButton.ViewModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LikeButton.Persistence
{
    public class Repository : IRepository
    {
        private readonly LikeDbContext context;

        public Repository(LikeDbContext context) => this.context = context;
        
        public async Task<Article> GetArticleByIdAsync(int articleId) =>
            await context.Set<Article>().Include(l => l.Likes) //eager-loads article
            .FirstOrDefaultAsync(x => x.ArticleId == articleId);

        public async Task<bool> LikeUnlikeArticleAsync(Like like)
        {
            if (like.Liked)
                context.Like.Add(like);
            else
                //if already liked, we remove from db, reducing db data. 
                //Toggling/updating liked column on db would leave unused data
                context.Like.Remove(like);
            return await context.SaveChangesAsync() > 0;
        }
    }

}
