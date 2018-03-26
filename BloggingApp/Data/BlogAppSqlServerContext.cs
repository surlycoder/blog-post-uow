using System;
using System.Data;
using System.Data.SqlClient;

namespace BloggingApp.Data
{
	public class BlogAppSqlServerContext : IBlogAppContext, IDisposable
	{
		private readonly string _connectionString;
		private IDbConnection _connection;
		private IDbTransaction _transaction;

		public BlogAppSqlServerContext(string connectionString)
		{
			_connectionString = connectionString;
		}

		private IDbConnection Connection
		{
			get
			{
				if ( _connection == null )
					_connection = new SqlConnection(_connectionString);

				if ( _connection.State != ConnectionState.Open )
					_connection.Open();

				return _connection;
			}
		}

		public IDbCommand CreateCommand()
		{
			var command = Connection.CreateCommand();

			if ( _transaction != null )
				command.Transaction = _transaction;

			return command;
		}

		public void StartSession()
		{
			if ( Connection.State != ConnectionState.Open )
				Connection.Open();

			if ( _transaction != null )
				throw new Exception("Could not start session. Session already open.");

			_transaction = Connection.BeginTransaction();
		}

		public void CompleteSession()
		{
			if ( _connection == null )
				throw new Exception("Could not complete session. No connection initialized.");

			if ( _connection.State != ConnectionState.Open )
				throw new Exception("Could not complete session. Connection not open.");

			if ( _transaction == null )
				throw new Exception("Could not complete session. Session not started.");

			_transaction.Commit();
			_transaction.Dispose();
		}

		public void RollbackSession()
		{
			_transaction.Rollback();
			_transaction.Dispose();
		}

		#region IDisposable Support
		private bool disposedValue = false; // To detect redundant calls

		protected virtual void Dispose(bool disposing)
		{
			if ( !disposedValue )
			{
				if ( disposing )
				{
					if ( _transaction != null )
					{
						_transaction.Dispose();
					}

					if ( _connection != null )
						_connection.Dispose();
				}

				disposedValue = true;
			}
		}

		// TODO: override a finalizer only if Dispose(bool disposing) above has code to free unmanaged resources.
		// ~BlogAppSqlServerContext() {
		//   // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
		//   Dispose(false);
		// }

		// This code added to correctly implement the disposable pattern.
		public void Dispose()
		{
			// Do not change this code. Put cleanup code in Dispose(bool disposing) above.
			Dispose(true);
			// TODO: uncomment the following line if the finalizer is overridden above.
			// GC.SuppressFinalize(this);
		}
		#endregion
	}
}
