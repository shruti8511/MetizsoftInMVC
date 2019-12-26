﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Data.Common;

namespace Metizsoft.Service
{
    public class BaseSqlManager
    {
        public static SqlConnection sCon;

        #region Connectionstring
        public static string ConnectionString()
        {
            return System.Configuration.ConfigurationManager.ConnectionStrings["DBConnection"].ConnectionString;
        }
        #endregion

        #region OpenConnection
        public static void OpenConnection(SqlCommand command)
        {
            sCon = new SqlConnection();
            sCon.ConnectionString = ConnectionString();
            command.Connection = sCon;
            command.CommandTimeout = 3000;
            sCon.Open();
        }
        #endregion

        #region ForceCloseconnection
        public void ForceCloseConnection()
        {
            if (sCon.State == System.Data.ConnectionState.Open)
            {
                sCon.Close();
            }
        }

        #endregion

        #region ExecuteDataReader
        public SqlDataReader ExecuteDataReader(SqlCommand command)
        {
            OpenConnection(command);
            return command.ExecuteReader();
        }

        #endregion

        public DbCommand CreateCommand()
        {
            sCon = new SqlConnection();
            sCon.ConnectionString = ConnectionString();

            DbCommand comm = sCon.CreateCommand();

            comm.CommandType = CommandType.StoredProcedure;

            return comm;
        }

        public static DataTable ExecuteSelectCommand(DbCommand command)
        {
            // The DataTable to be returned 
            DataTable table;
            // Execute the command making sure the connection gets closed in the end
            //try
            //{
            // Open the data connection 
            command.Connection.Open();
            // Execute the command and save the results in a DataTable
            DbDataReader reader = command.ExecuteReader();
            table = new DataTable();
            table.Load(reader);
            // Close the reader 
            reader.Close();

            command.Connection.Close();
            //}
            return table;
        }

        #region ExecuteNonQuery
        public int ExecuteNonQuery(SqlCommand command)
        {
            OpenConnection(command);
            return command.ExecuteNonQuery();
        }
        #endregion

        #region ExecuteScalar
        public static object ExecuteScalar(SqlCommand command)
        {
            OpenConnection(command);
            return command.ExecuteScalar();
        }
        #endregion

        #region ExecuteReader
        public SqlDataReader ExecuteReader(SqlCommand command)
        {
            OpenConnection(command);
            return command.ExecuteReader();
        }
        #endregion

        public decimal GetDecimal(SqlDataReader dr, string fieldname)
        {
            if (dr[fieldname] != DBNull.Value)
                return Convert.ToDecimal(dr[fieldname]);
            else
                return 0;
        }
        public float GetFloat(SqlDataReader dr, string fieldname)
        {
            if (dr[fieldname] != DBNull.Value)
                return Convert.ToUInt64(dr[fieldname]);
            else
                return 0;
        }
        public string GetTextValue(SqlDataReader dr, string fieldname)
        {
            if (dr[fieldname] != DBNull.Value)
                return Convert.ToString(dr[fieldname]);
            else
                return "";
        }

        public int GetInt32(SqlDataReader dr, string fieldname)
        {
            if (dr[fieldname] != DBNull.Value)
                return Convert.ToInt32(dr[fieldname]);
            else
                return 0;
        }

        public long GetInt64(SqlDataReader dr, string fieldname)
        {
           if (dr[fieldname] != DBNull.Value)
                return Convert.ToInt64(dr[fieldname]);
            else
                return 0;
        }

        public  DateTime GetDateTime(SqlDataReader dr, string fieldname)
        {
            if (dr[fieldname] != DBNull.Value)
                return Convert.ToDateTime(dr[fieldname]);
            else
                return Convert.ToDateTime("01/01/0001");
        }

        public  bool GetBoolean(SqlDataReader dr, string fieldname)
        {
            if (dr[fieldname] != DBNull.Value)
                return Convert.ToBoolean(dr[fieldname]);
            else
                return false;
        }

    }
}
