using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FundsApplication.HelpClasses
{
    public class RandomFund
    {

        public string Name { get; set; }
        public string Description { get; set; }

        public RandomFund(Random rand)
        {
            this.Name = getRandomName(rand);
            this.Description = getRandomDescription(rand);
        }

        string getRandomName(Random rand)
        {

            switch (rand.Next(10))
            {
                case 0:
                    return "School";
                case 1:
                    return "Credit";
                case 2:
                    return "Trening";
                case 3:
                    return "Fun";
                case 4:
                    return "Secret";
                case 5:
                    return "Events";
                case 6:
                    return "Property";
                case 7:
                    return "Kids";
                case 8:
                    return "Programming";
                case 9:
                    return "Default";
                default:
                    return "";
            }
        }
        string getRandomDescription(Random rand)
        {

            switch (rand.Next(10))
            {
                case 0:
                    return "First Fund Description";
                case 1:
                    return "Secound Fund Description";
                case 2:
                    return "Third Fund Description";
                case 3:
                    return "Fourth Fund Description";
                case 4:
                    return "Fifth Fund Description";
                case 5:
                    return "Sixth Fund Description";
                case 6:
                    return "Seventh Fund Description";
                case 7:
                    return "Eighth Fund Description";
                case 8:
                    return "Nineth Fund Description";
                case 9:
                    return "Tenth Fund Description";
                default:
                    return "";
            }
        }

    }
}