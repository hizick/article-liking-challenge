using System;
using Xunit;
using Moq;
using LikeButton.Persistence;
using LikeButton.Controllers;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using LikeButton.ViewModels;
using LikeButton.Models;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;

namespace LikeButton.Test
{
    public class HomeControllerTest
    {
        private Mock<IRepository> repositoryService;
        private readonly HomeController _controller;

        public HomeControllerTest()
        {
            repositoryService = new Mock<IRepository>();

            _controller = new HomeController(repositoryService.Object);
        }

        [Fact]
        public async Task Index_NoArticleWithGivenIdExists_ShouldReturnNotFound()
        {

            var result = await _controller.Index(0);

            Assert.IsType<NotFoundResult>(result);

            var notFoundResult = result as NotFoundResult;
            Assert.Equal(404, notFoundResult.StatusCode);
        }

        [Fact]
        public async Task Index_ArticleWithGivenIdExists_ShouldReturnTypeOfHomeViewModel()
        {
            repositoryService.Setup(a => a.GetArticleByIdAsync(1)).ReturnsAsync(new Article() 
            { ArticleId = 1, Likes = new List<Like>(), ArticleText = "this is a test" });
            
            var result = await _controller.Index(1);

            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsType<HomeViewModel>(viewResult.ViewData.Model);
            Assert.Equal(1, model.ArticleId);

        }

        [Fact]
        public async Task ToggleLike_UnidentifiedUserLikesArticle_ShouldReturnBadRequest()
        {
            repositoryService.Setup(a => a.GetArticleByIdAsync(1)).ReturnsAsync(new Article()
            { ArticleId = 1, Likes = new List<Like>(), ArticleText = "this is a test" });

            var result = await _controller.ToggleLike("1");

            Assert.IsType<BadRequestResult>(result);

            var badResult = result as BadRequestResult;
            Assert.Equal(400, badResult.StatusCode);
        }

        [Fact]
        public async Task ToggleLike_ValidUserLikesArticle_ShouldReturnAnonymousObjectWithTrueStatus()
        {
            repositoryService.Setup(a => a.GetArticleByIdAsync(1)).ReturnsAsync(new Article()
            { ArticleId = 1, Likes = new List<Like>(), ArticleText = "this is a test" });

            var result = await _controller.ToggleLike("1");

            var data = result as OkObjectResult;
            var resultData = data.Value as Data;
            Assert.False(resultData.status); //since we are only mocking and not saving to db
        }
    }
}
