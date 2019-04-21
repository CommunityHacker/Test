using FundsApplication.Interface;
using FundsApplication.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FundsApplication.Database
{
    public class DatabaseLogic
    {
        private readonly IDatabaseConnection _databaseConnection;

        public DatabaseLogic(DatabaseConnection databaseConnection)
        {
            _databaseConnection = databaseConnection;
        }



        public List<Fund> GetDataWithName(string fundName,int offset)
        {
            return _databaseConnection.GetDataWithName(fundName, offset);
        }

        public int GetFundCount(string fundName)
        {
            return _databaseConnection.GetFundCount(fundName);
        }

        public List<Value> GetDetails(int fundId)
        {
            return _databaseConnection.GetValueDataWithId(fundId);
        }

        public string InsertRandomFund(Random rand)
        {
           return _databaseConnection.InsertRandimFund(rand);
        }

        public string InsertRandomValue(int fundId,Random rand)
        {
            return _databaseConnection.InsertRandomValue(fundId,rand);
        }

        public void InsertMillionData()
        {
            //INFO:: Run it ONLY on empty database !

            Random rand = new Random();

            for (int i = 0; i < 1000000; i++)
            {
                InsertRandomFund(rand);
                for (int j = 0; j < 3; j++)
                {
                    InsertRandomValue(i, rand);
                }
            }         
        }

    }
}