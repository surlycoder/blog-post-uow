using System.Data;
using System.Data.SqlClient;

namespace BlogUoW.Data
{
	public class BlogSqlServerContext:IBlogContext
	{
		private readonly string _connectionString;

		public BlogSqlServerContext( string connectionString )
		{
			_connectionString = connectionString;
		}

		public IDbConnection CreateConnection()
		{
			return new SqlConnection( _connectionString );
		}
	}
}
