using FundsApplication.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FundsApplication.HelpClasses
{
    public class RandomValue
    {
        public DateTime Date { get; set; }
        public int FundId { get; set; }
        public decimal FundValue { get; set; }

        public RandomValue(int fundId,Random rand)
        {
            this.Date = RandomDay(rand);
            this.FundValue = rand.Next(999999);
            this.FundId = fundId;

        }

        DateTime RandomDay(Random rnd)
        {
            DateTime start = new DateTime(1992, 08, 12);
            int range = (DateTime.Today - start).Days;
            return start.AddDays(rnd.Next(range));
        }
    }
}