using FundsApplication.Database;
using FundsApplication.Models;
using NUnit.Framework;
using System;

namespace FundsApplicationTest
{
    [TestFixture]
    public class UnitTest
    {
        [Test]
        public void Get_Ten_Items_Test()
        {
            DatabaseLogic datbaseLogic = new DatabaseLogic(new DatabaseConnection());
            int? i = datbaseLogic.GetDataWithName("School",0).Count;
            Assert.AreEqual(10,i);

        }

        [Test]
        public void Get_Details_Test()
        {
            DatabaseLogic datbaseLogic = new DatabaseLogic(new DatabaseConnection());
            int? i = datbaseLogic.GetDetails(23).Count;
            Assert.AreNotEqual(null, i);
        }

        [Test]
        public void Get_Count_Test()
        {
            DatabaseLogic datbaseLogic = new DatabaseLogic(new DatabaseConnection());
            int? i = datbaseLogic.GetFundCount("Gym");
            Assert.AreNotEqual(null, i);
        }

        [Test]
        public void Insert_Fund_Test()
        {
            DatabaseLogic datbaseLogic = new DatabaseLogic(new DatabaseConnection());
            Random rand = new Random();
            string status = datbaseLogic.InsertRandomFund(rand);
            Assert.AreEqual("Complete", status);
        }

        [Test]
        public void Insert_Value_Test()
        {
            DatabaseLogic datbaseLogic = new DatabaseLogic(new DatabaseConnection());
            Random rand = new Random();
            int testFundId = 23;
            string status = datbaseLogic.InsertRandomValue(testFundId,rand);
            Assert.AreEqual("Complete", status);
        }
    }
}
