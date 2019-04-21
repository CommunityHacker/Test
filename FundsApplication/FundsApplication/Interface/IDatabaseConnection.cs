using FundsApplication.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FundsApplication.Interface
{
    interface IDatabaseConnection
    {
        string InsertRandimFund(Random rand);
        string InsertRandomValue(int id, Random rand);
        int GetFundCount(string name);
        List<Fund> GetDataWithName(string name, int offset);
        List<Value> GetValueDataWithId(int id);
    }
}
