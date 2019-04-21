using FundsApplication.HelpClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FundsApplication.Models
{
    public class Fund
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal LatestValue { get; set; }


        public Fund()
        {

        }

        public Fund(int id,string name,string description,decimal latestValue)
        {
            this.Id = id;
            this.Name = name;
            this.Description = description;
            this.LatestValue = latestValue;
        }

        public Fund(RandomFund randFund)
        {
            this.Name = randFund.Name;
            this.Description = randFund.Description;
        }

    }
}