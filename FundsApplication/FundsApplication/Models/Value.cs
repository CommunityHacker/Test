using FundsApplication.HelpClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FundsApplication.Models
{
    public class Value
    {
        public int Id { get; set; }
        public int FundId { get; set; }
        public DateTime Date { get; set; }
        public decimal FundValue { get; set; }

        public Value()
        {

        }
        public Value(int id,int fundId,DateTime date,decimal fundValue)
        {
            this.Id = id;
            this.FundId = fundId;
            this.Date = date;
            this.FundValue = fundValue;
        }

        public Value(RandomValue randValue)
        {
            this.FundId = randValue.FundId;
            this.Date = randValue.Date;
            this.FundValue = randValue.FundValue;
        }
    }
}