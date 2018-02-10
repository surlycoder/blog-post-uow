using BloggingApp.Data;
using BloggingApp.Data.Entities;
using Xunit;

namespace BloggingApp.Tests
{
	public class BlogRepositoryIntegrationTests
	{
		private const string DbConnectionString = "Server=(local);Integrated Security=SSPI;" +
			"Initial Catalog=EFGetStarted.AspNetCore.NewDb";

		[Fact]
		public void GetById_Returns_Blog()
		{
			// Arrange
			var repository = new BlogRepository( new BlogAppSqlServerContext( DbConnectionString ) );

			// Act
			var blog = repository.GetById( 1 );

			// Assert
			Assert.NotNull( blog );
		}

		[Fact]
		public void GetAll_Returns_List_Of_Blogs()
		{
			// Arrange
			var repository = new BlogRepository( new BlogAppSqlServerContext( DbConnectionString ) );

			// Act
			var blogs = repository.GetAll();

			// Assert
			Assert.NotNull( blogs );
			Assert.NotEmpty( blogs );
		}

		[Fact]
		public void Create_Inserts_New_Blog()
		{
			// Arrange
			var repository = new BlogRepository( new BlogAppSqlServerContext( DbConnectionString ) );

			var blogToCreate = new BlogDto()
			{
				Url = "https://not.arealblog.fake"
			};

			// Act
			repository.Create( blogToCreate );

			// Assert
			Assert.True( blogToCreate.Id > 0 );
		}
	}
}
