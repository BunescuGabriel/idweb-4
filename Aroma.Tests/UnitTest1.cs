using Microsoft.VisualBasic;
using Xunit;

namespace Aroma.Tests
{
    public class UnitTest1
    {
        TestBlogRepository repository = new TestBlogRepository();
        [Fact]
        public void FindAnExistingTest()
        {
            repository.AddBlogAsync(new Aroma_Shop.Domain.Models.BlogModels.Blog()
            {
                BlogId = 5
            }) ;
            var result = repository.GetBlog(5);
            Assert.NotNull(result);
        }
        [Fact]
        public void FindAnInexistent()
        {
            repository.AddBlogAsync(new Aroma_Shop.Domain.Models.BlogModels.Blog()
            {
                BlogId = 5
            });
            var result = repository.GetBlog(6);
            Assert.Null(result);
        }
        [Fact]
        public void DeleteAndFindAnInexistent()
        {
            repository.AddBlogAsync(new Aroma_Shop.Domain.Models.BlogModels.Blog()
            {
                BlogId = 5
            });
            repository.DeleteBlog(repository.GetBlog(5));
            var result = repository.GetBlog(5);
            Assert.Null(result);
        }
    }
}