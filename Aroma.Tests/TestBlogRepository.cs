using Aroma_Shop.Data.Context;
using Aroma_Shop.Data.Repositories;
using Aroma_Shop.Domain.Interfaces;
using Aroma_Shop.Domain.Models.BlogModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aroma.Tests
{
    public class TestBlogRepository : IBlogRepository
    {
        private List<Blog> _blogList = new List<Blog>();
        public Task AddBlogAsync(Blog blog)
        {
            _blogList.Add(blog);
            return Task.CompletedTask;
        }

        public Task AddBlogCategoryAsync(BlogCategory blogCategory)
        {
            throw new NotImplementedException();
        }

        public void DeleteBlog(Blog blog)
        {
            _blogList.Remove(blog);
        }

        public void DeleteBlogCategory(BlogCategory blogCategory)
        {
            throw new NotImplementedException();
        }

        public Task<Blog> GetBlogAsync(int blogId)
        {
            throw new NotImplementedException();
        }
        public Blog GetBlog(int blogId)
        {
            return _blogList.Where(x=>x.BlogId==blogId).FirstOrDefault();
        }

        public Task<IEnumerable<BlogCategory>> GetBlogCategoriesAsync()
        {
            throw new NotImplementedException();
        }

        public Task<BlogCategory> GetBlogCategoryAsync(int blogCategoryId)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Blog>> GetBlogsAsync()
        {
            throw new NotImplementedException();
        }

        public Task SaveAsync()
        {
            throw new NotImplementedException();
        }

        public void UpdateBlog(Blog blog)
        {
            throw new NotImplementedException();
        }

        public void UpdateBlogCategory(BlogCategory blogCategory)
        {
            throw new NotImplementedException();
        }
    }
}
