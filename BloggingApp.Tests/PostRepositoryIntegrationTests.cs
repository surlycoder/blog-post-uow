using AutoMapper;
using BloggingApp.Data;
using BloggingApp.Data.Entities;
using Xunit;

namespace BloggingApp.Tests
{
	public class PostRepositoryIntegrationTests
	{
		private const string DbConnectionString = "Server=(local);Integrated Security=SSPI;" +
			"Initial Catalog=BlogDB";

		private readonly IMapper _mapper;

		public PostRepositoryIntegrationTests()
		{
			var config = new MapperConfiguration(cfg =>
			{
				cfg.AddProfile<MappingProfile>();
			});
			_mapper = config.CreateMapper();
		}

		[Fact]
		public void GetById_Returns_Post()
		{
			// Arrange
			var repository = new PostRepository(new BlogAppSqlServerContext(DbConnectionString), _mapper);

			// Act
			var post = repository.GetById(1);

			// Assert
			Assert.NotNull(post);
		}

		[Fact]
		public void GetList_Returns_List_Of_Posts()
		{
			// Arrange
			var repository = new PostRepository(new BlogAppSqlServerContext(DbConnectionString), _mapper);

			// Act
			var posts = repository.GetListByBlogId(1);

			// Assert
			Assert.NotNull(posts);
			Assert.NotEmpty(posts);
		}

		[Fact]
		public void Create_Inserts_New_Post()
		{
			// Arrange
			var repository = new PostRepository(new BlogAppSqlServerContext(DbConnectionString), _mapper);

			var postToCreate = new PostDto()
			{
				BlogId = 1,
				Title = "Some fake title",
				Content = "Some fake content"
			};

			// Act
			repository.Create(postToCreate);

			// Assert
			Assert.True(postToCreate.Id > 0);
		}
	}
}
