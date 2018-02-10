using System.Data;

namespace BloggingApp.Data
{
	public interface IBlogAppContext
	{
		IDbConnection CreateConnection();
	}
}