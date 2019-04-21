using FundsApplication.HelpClasses;
using FundsApplication.Interface;
using FundsApplication.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace FundsApplication.Database
{
    public class DatabaseConnection: IDatabaseConnection
    {
        
        SqlConnection _connection()
        {
            string connectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=funds_db;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
            return new SqlConnection(connectionString);
        }

        #region Get Data
        public List<Value> GetValueDataWithId(int id)
        {
            List<Value> returnValue = new List<Value>();
            try
            {
                using (SqlConnection connection = _connection())
                {
                    connection.Open();
                    using (SqlCommand cmd = new SqlCommand(@"SELECT * From Value 
                                                             WHERE FundId = @id
                                                             ORDER BY date DESC; ", connection))
                    {
                        cmd.Parameters.AddWithValue("@id", id);
                        var dr = cmd.ExecuteReader();
                        while (dr.Read())
                        {
                            returnValue.Add(new Value(dr.GetInt32(dr.GetOrdinal("Id")), 
                                dr.GetInt32(dr.GetOrdinal("FundId")), 
                                dr.GetDateTime(dr.GetOrdinal("Date")), 
                                dr.GetDecimal(dr.GetOrdinal("FundValue"))));
                        }
                        dr.Close();
                    }
                    if (connection.State == System.Data.ConnectionState.Open)
                        connection.Close();
                }
            }
            catch (Exception e) { throw e; }

            return returnValue;
        }

        public List<Fund> GetDataWithName(string name,int offset)
        {
            if (string.IsNullOrEmpty(name)) { return null; }

            offset -= 1;


            offset *= 10;
            List<Fund> returnList = new List<Fund>();
            try
            {
                using (SqlConnection connection = _connection())
                {
                    connection.Open();
                    using (SqlCommand cmd = new SqlCommand(@"SELECT * From Fund INNER JOIN  Value ON Fund.Id = Value.FundId
                                                             WHERE CONVERT(NVARCHAR(MAX), Name) = @name
                                                             ORDER BY Value.Date DESC OFFSET @offset ROWS
                                                             FETCH NEXT 10 ROW ONLY ;", connection))
                    {
                        cmd.Parameters.AddWithValue("@name", name);
                        cmd.Parameters.AddWithValue("@offset", offset);
                        var dr = cmd.ExecuteReader();
                        while (dr.Read())
                        {
                            returnList.Add(new Fund(dr.GetInt32(dr.GetOrdinal("Id")), dr.GetString(dr.GetOrdinal("Name")), dr.GetString(dr.GetOrdinal("Description")), dr.GetDecimal(dr.GetOrdinal("FundValue"))));
                        }
                        dr.Close();
                    }
                    if (connection.State == System.Data.ConnectionState.Open)
                        connection.Close();
                }
            }
            catch (Exception e) { throw e; }
            return returnList;
        }

        public int GetFundCount(string name)
        {
            if (string.IsNullOrEmpty(name)) { return 0; }
            int count = 0;
            try
            {
                using (SqlConnection connection = _connection())
                {
                    connection.Open();
                    using (SqlCommand cmd = new SqlCommand(@"SELECT COUNT(*) FROM Fund where CONVERT(NVARCHAR(MAX), Name) = @name;", connection))
                    {
                        cmd.Parameters.AddWithValue("@name", name);
                        var dr = cmd.ExecuteReader();
                        while (dr.Read())
                        {
                            count = dr.GetInt32(0);
                        }
                        dr.Close();
                    }
                    if (connection.State == System.Data.ConnectionState.Open)
                        connection.Close();
                }
            }
            catch (Exception e) { throw e; }
            return count;
        }

        #endregion

        #region Insert Data
        public string InsertRandimFund(Random rand)
        {
            string state = "Failed";
            Fund fund = GetRandomFund(rand);

            try
            {
                using (SqlConnection connection = _connection())
                {
                    connection.Open();
                    using (SqlCommand cmd = new SqlCommand("INSERT INTO Fund (Name,Description) VALUES (@name,@description);", connection))
                    {
                        cmd.Parameters.AddWithValue("@name", fund.Name);
                        cmd.Parameters.AddWithValue("@description", fund.Description);
                        cmd.ExecuteReader();

                    }
                    if (connection.State == System.Data.ConnectionState.Open)
                        connection.Close();

                    state = "Complete";
                }
            }
            catch (Exception e) { throw e; }

            return state;
        }

        public string InsertRandomValue(int id,Random rand)
        {          
            Value value = GetRandomValue(id,rand);
            string state = "Failed";
            try
            {
                using (SqlConnection connection = _connection())
                {
                    connection.Open();
                    using (SqlCommand cmd = new SqlCommand("INSERT INTO Value (FundId,Date,FundValue) VALUES (@fundId,@date,@fundValue);", connection))
                    {
                        cmd.Parameters.AddWithValue("@fundId", value.FundId);
                        cmd.Parameters.AddWithValue("@date", value.Date);
                        cmd.Parameters.AddWithValue("@fundValue", value.FundValue);
                        cmd.ExecuteReader();                        
                    }
                    if (connection.State == System.Data.ConnectionState.Open)
                            connection.Close();

                    state = "Complete";
                }
            }
            catch (Exception e) {throw e;}
            return state;            
        }

        Fund GetRandomFund(Random rand)
        {
            RandomFund rndFund = new RandomFund(rand);
            Fund randomFund = new Fund(rndFund);

            return randomFund;
        }
        Value GetRandomValue(int fundId,Random rand)
        {
            RandomValue rndValue = new RandomValue(fundId, rand);
            Value randomValue = new Value(rndValue);

            return randomValue;

        }
        #endregion

    }
}