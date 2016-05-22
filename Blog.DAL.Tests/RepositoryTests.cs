using System;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Reflection;
using Blog.DAL.Infrastructure;
using Blog.DAL.Model;
using Blog.DAL.Repository;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Diagnostics;
using TDD.DbTestHelpers.Core;
using TDD.DbTestHelpers.Yaml;

namespace Blog.DAL.Tests
{
    [TestClass]
    public class RepositoryTests: DbBaseTest<BlogFixtures>
    {
        [TestMethod]
        public void GetAllPost_OnePostInDb_ReturnOnePost()
        {
            // arrange
            var context = new BlogContext();
            context.Database.CreateIfNotExists();
            var repository = new BlogRepository();
            // act
            var result = repository.GetAllPosts();
            // assert
            Assert.AreEqual(1, result.Count());
        }

        [TestMethod]
        [ExpectedException(typeof(NullReferenceException))]
        public void AddNewPost_ShouldReturnNullException()
        {
            //arrange
            var repo = new BlogRepository();
            //act
            repo.AddNewPost(new Post
            {
                Author = ""
            });
            //assert

        }

        [TestMethod]
        public void AddNewPost_ShouldAddNewPost()
        {
            //arrange
            var repo = new BlogRepository();
            var context = new BlogContext();
            //act
            repo.AddNewPost(new Post
            {
                Author = "name",
                Content = "test"
            });
            //assert
            var post = context.Posts.FirstOrDefault(a => a.Author == "name" && a.Content == "test");
            Assert.AreNotEqual(null, post);
        }

        [TestMethod]
        public void GetAllPostComments_ShouldReturnOneComment()
        {
            var repo = new BlogRepository();
            var context = new BlogContext();
            var comments = repo.GetAllPostComments(context.Posts.First());
            Assert.AreEqual(1, comments.Count());
        }

        [TestMethod]
        [ExpectedException(typeof(NullReferenceException))]
        public void AddCommentToPost_ShouldReturnException()
        {
            var repo = new BlogRepository();
            repo.AddCommentToPost(null);
        }

        [TestMethod]
        public void AddCommentToPost_ShouldAddNewComment()
        {
            var context = new BlogContext();
            var repo = new BlogRepository();

            var post = context.Posts.First();
            var comment = new Comment
            {
                PostId = post.Id,
                Content = "lalala"
            };

            repo.AddCommentToPost(comment);

            Assert.IsNotNull(context.Comments.FirstOrDefault(a => a.Content == "lalala" && a.PostId == post.Id));

        }
    } 
}
