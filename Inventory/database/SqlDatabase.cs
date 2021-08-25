﻿using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Controls;

namespace Inventory.database
{
	public class SqlDatabase : IDisposable
	{
		private string ServerIp = string.IsNullOrEmpty(Properties.Settings.Default.DatabaseIp) ? "192.168.0.254" : Properties.Settings.Default.DatabaseIp;
		private const string ServerPort = "1433";
		private const string DatabaseName = "pruebas";
		private const string IntegratedSecurity = "False";
		private const string UserId = "sa";
		private const string Password = "Tlacua015";
		private const string MultipleActiveResultSets = "True";
		private const string ConnectionTimeoutInSeconds = "10";
		private string _databaseConnectionString;
		private SqlConnection _databaseConnection;
		public SqlDatabase()
		{
			DatabaseConnectionString = "Server=" + ServerIp + "," + ServerPort + 
			                           ";Database=" + DatabaseName + 
			                           ";Integrated Security=" + IntegratedSecurity + 
			                           ";User Id=" + UserId + 
			                           ";Password=" + Password + 
			                           ";MultipleActiveResultSets=" + MultipleActiveResultSets + 
			                           ";Connection Timeout=" + ConnectionTimeoutInSeconds;
				DatabaseConnection = new SqlConnection(DatabaseConnectionString);
		}
		public SqlDatabase(string databaseConnectionString)
		{
			DatabaseConnectionString = databaseConnectionString;
			DatabaseConnection = new SqlConnection(DatabaseConnectionString);
		}
		public string DatabaseConnectionString
		{
			get
			{
				return _databaseConnectionString;
			}
			set
			{
				if (!string.IsNullOrEmpty(value))
				{
					_databaseConnectionString = value;
				}
			}
		}
		public SqlConnection DatabaseConnection
		{
			get
			{
				return _databaseConnection;
			}
			set
			{
				_databaseConnection = value;
			}
		}
		public SqlDataReader Read(string query)
		{
			SqlCommand sqlCommand = GetSqlCommandWithQuery(query);
			SqlDataReader dataReader = sqlCommand.ExecuteReader();
			return dataReader;
		}
		public SqlDataReader Read(SqlCommand sqlCommand)
		{
			DatabaseConnection.Open();
			SqlDataReader dataReader = sqlCommand.ExecuteReader();
			return dataReader;
		}
		public SqlCommand GetSqlCommandWithQuery(string query)
		{
			DatabaseConnection.Open();
			SqlCommand sqlCommand = new SqlCommand(query, DatabaseConnection);
			return sqlCommand;
		}
		public void FillDataGridWithQuery(string query, DataGrid dataGrid)
		{
			DataTable dataTable = GetFilledDataTableWithSqlDataAdapter(query);
			dataGrid.ItemsSource = dataTable.DefaultView;
			DatabaseConnection.Close();
		}
		public DataTable GetFilledDataTableWithSqlDataAdapter(string query)
		{
			DatabaseConnection.Open();
			DataTable dataTable = new DataTable();
			SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(query, DatabaseConnection);
			sqlDataAdapter.Fill(dataTable);
			return dataTable;
		}

		public void Dispose()
		{
			DatabaseConnection.Close();
		}
	}
}	
