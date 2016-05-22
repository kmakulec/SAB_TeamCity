using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Blog.DAL.Infrastructure;
using Blog.DAL.Model;
using TDD.DbTestHelpers.Yaml;

namespace Blog.DAL.Tests
{
    public class BlogFixtures : YamlDbFixture<BlogContext, BlogFixturesModel>
    {
        public BlogFixtures()
        {
            SetYamlFiles("posts.yml");
        }

    }

    public class BlogFixturesModel
    {
        public FixtureTable<Post> Posts { get; set; }
        public FixtureTable<Comment> Comments { get; set; }
    }
}
