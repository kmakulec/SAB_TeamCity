using System.Collections.Generic;
using Blog.DAL.Infrastructure;
using Blog.DAL.Model;
using System.Linq;
using System;

namespace Blog.DAL.Repository
{
    public class BlogRepository
    {
        private readonly BlogContext _context;

        public BlogRepository()
        {
            _context = new BlogContext();
        }

        public IEnumerable<Post> GetAllPosts()
        {
            return _context.Posts;
        }

        public void AddNewPost(Post _post)
        {
            if (_post == null || _post.Author == null || _post.Content == null || _post.Author == string.Empty || _post.Content == string.Empty)
                throw new NullReferenceException();
            _context.Posts.Add(_post);
            _context.SaveChanges();
        }

        public void AddCommentToPost(Comment _comment)
        {
            if (_comment.PostId == 0 || _comment.Content == string.Empty || _comment.Content == null || _comment == null)
                throw new NullReferenceException();

            _context.Comments.Add(_comment);
            _context.SaveChanges();
        }

        public IEnumerable<Comment> GetAllPostComments(Post _post)
        {
            return _context.Comments.Where(x => x.PostId == _post.Id);
        }
    }
}
