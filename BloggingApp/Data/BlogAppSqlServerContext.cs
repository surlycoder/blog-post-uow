using System.Data;
using System.Data.SqlClient;

namespace BloggingApp.Data
{
	public class BlogAppSqlServerContext : IBlogAppContext
	{
		private readonly string _connectionString;

		public BlogAppSqlServerContext( string connectionString )
		{
			_connectionString = connectionString;
		}

		public IDbConnection CreateConnection()
		{
			return new SqlConnection( _connectionString );
		}
	}
}
