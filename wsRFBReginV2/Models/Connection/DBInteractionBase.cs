////////////////////////////////////////////////////////////////////////////////
// Description: Base class for Database Interaction.                       
// Generated by DALGen32 v1.0.1041.23898 on: Thursday, November 14, 2002, 10:06:04 AM
// Because this class implements IDisposable, derived classes shouldn't do so.
///////////////////////////////////////////////////////////////////////////////
using System;
using System.Configuration;
using System.Data;
using MySql.Data.MySqlClient;
using System.Diagnostics;

namespace psc.Receita.ConnectionBase
{
	/// <summary>
	/// Purpose: Error Enums used by this DAL library.
	/// </summary>
	public enum DataFactoryError
	{
		AllOk
		// Add more here
	}


	/// <summary>
	/// Purpose: General interface of the API generated. Contains only common methods of all classes.
	/// </summary>
	public interface ICommonDBAccess
	{
		bool		Insert();
		bool		Update();
		bool		Delete();
		DataTable	SelectOne();
		DataTable	SelectAll();
	}


	/// <summary>
	/// Purpose: Abstract base class for Database Interaction classes.
	/// </summary>
	public abstract class DBInteractionBase : IDisposable//, ICommonDBAccess
	{
		#region Class Member Declarations
			protected	MySqlConnection	    _mainConnection;
			protected	decimal				_errorCode;
			protected	bool				_mainConnectionIsCreatedLocal;
			protected	ConnectionProvider	_mainConnectionProvider;
			private		bool				_isDisposed;
		#endregion


		/// <summary>
		/// Purpose: Class constructor.
		/// </summary>
		public DBInteractionBase()
		{
			// Initialize the class' members.
			InitClass();
		}


		/// <summary>
		/// Purpose: Initializes class members.
		/// </summary>
		private void InitClass()
		{
			// create all the objects and initialize other members.
			_mainConnection = new MySqlConnection();
			_mainConnectionIsCreatedLocal = true;
			_mainConnectionProvider = null;
			//AppSettingsReader _configReader = new AppSettingsReader();

			// Set connection string of the sqlconnection object
            try
            {
                _mainConnection.ConnectionString = ConfigurationManager.AppSettings["Main.ConnectionStringMYSQL"].ToString();
            }
            catch (Exception ex)
            {
                throw new Exception("Key Main.ConnectionStringMYSQL not exist in Web.config \n\t" + ex.ToString());
            }
						//_configReader.GetValue("Main.ConnectionString", typeof(string)).ToString();
			_errorCode = (int)DataFactoryError.AllOk;
			_isDisposed = false;
		}


		/// <summary>
		/// Purpose: Implements the IDispose' method Dispose.
		/// </summary>
		public void Dispose()
		{
			Dispose(true);
			GC.SuppressFinalize(this);
		}


		/// <summary>
		/// Purpose: Implements the Dispose functionality.
		/// </summary>
		protected virtual void Dispose(bool isDisposing)
		{
			// Check to see if Dispose has already been called.
			if (!_isDisposed)
			{
				if (isDisposing)
				{
					// Dispose managed resources.
					if (_mainConnectionIsCreatedLocal)
					{
						// Object is created in this class, so destroy it here.
						_mainConnection.Close();
						_mainConnection.Dispose();
						_mainConnectionIsCreatedLocal = false;
					}
					_mainConnectionProvider = null;
					_mainConnection = null;
				}
			}
			_isDisposed = true;
		}


		

		#region Class Property Declarations
		public ConnectionProvider MainConnectionProvider
		{
			get
			{
				if (_mainConnectionProvider == null)
				{
					throw new NullReferenceException("MainConnectionProvider is null");
				}

				return _mainConnectionProvider;
			}
			set
			{
				if (value==null)
				{
					// Invalid value
					throw new ArgumentNullException("MainConnectionProvider", "Null passed as value to this property which is not allowed.");
				}

				// A connection provider object is passed to this class.
				// Retrieve the SqlConnection object, if present and create a
				// reference to it. If there is already a MainConnection object
				// referenced by the membervar, destroy that one or simply 
				// remove the reference, based on the flag.
				if (_mainConnection!=null)
				{
					// First get rid of current connection object. Caller is responsible
					if (_mainConnectionIsCreatedLocal)
					{
						// Is local created object, close it and dispose it.
						_mainConnection.Close();
						_mainConnection.Dispose();
					}
					// Remove reference.
					_mainConnection = null;
				}
				_mainConnectionProvider = (ConnectionProvider)value;
				_mainConnection = _mainConnectionProvider.DBConnection;
				_mainConnectionIsCreatedLocal = false;
			}
		}


		public decimal ErrorCode
		{
			get
			{
				return _errorCode;
			}
		}
		#endregion
	}
}
