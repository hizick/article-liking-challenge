using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using LikeButton.Models;
using LikeButton.ViewModels;
using Microsoft.AspNetCore.Http;
using LikeButton.Persistence;

namespace LikeButton.Controllers
{
    public class HomeController : Controller
    {
        private readonly IRepository repository;
        public HomeController(IRepository repository) => this.repository = repository;

        public async Task<IActionResult> Index(int articleId = 1) //using a default articleId here, just for example.
        {
            var model = new HomeViewModel();

            //get article with its likes
            var article = await repository.GetArticleByIdAsync(articleId);
            if (article == null) return NotFound();

            string user = HttpContext.Session.GetString("user") ?? string.Empty;

            //automapper should do this mapping but we have only five props, so I think it is not that bad mapping manually
            model.ArticleId = articleId;
            model.ArticleText = article.ArticleText;
            model.LikeCount = article.Likes.Count();
            model.UserId = user;
            model.LikedByUser = article.Likes.Any(x => x.UserId.ToLower() == model.UserId);
            
            return View(model);
        }

        [HttpPost]
        public ActionResult Login(string username)
        {
            //only using username because the main focus is the likes for now.
            if (string.IsNullOrEmpty(username))
                return BadRequest("Invalid user");

            HttpContext.Session.SetString("user", username.ToLower());
            return Ok(true);
        }

        [HttpPost]
        public async Task<IActionResult> ToggleLike(string postId)
        {
            var articleId = Convert.ToInt32(postId);
            var article = await repository.GetArticleByIdAsync(articleId);
            if (article == null) return NotFound();

            string user = HttpContext.Session.GetString("user");
            if (string.IsNullOrEmpty(user))
                return BadRequest();

            var like = article.Likes.Find(x => x.ArticleId == articleId && x.UserId.ToLower() == user);
            if (like == null)
            {
                like = new Like();

                //automapper should do this mapping but this is still couple ones

                like.ArticleId = articleId;
                like.UserId = user;
                
            }
            like.Liked = !like.Liked; //toggle like

            return Ok( new Data
            {
                liked = like.Liked,
                status = await repository.LikeUnlikeArticleAsync(like)
            });
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Remove("user");
            return RedirectToAction("Index");
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
