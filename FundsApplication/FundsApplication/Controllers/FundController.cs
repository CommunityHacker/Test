using FundsApplication.Database;
using FundsApplication.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FundsApplication.Controllers
{
    public class FundController : Controller
    {
        
        int _selectedPage = 1;
        int _rowsCount = 0;
        int _pagesCount = 0;

        List<Fund> _foundsList = new List<Fund>();
        List<Value> _details = new List<Value>();

        DatabaseLogic _databaseLogic = new DatabaseLogic(new DatabaseConnection());

        static string _searchValue = "";


        #region Index View

        public ActionResult Index(string fundName,int selectedPage = 1)
        {
            //INFO:: Uncomment line below on FIRST RUN !!
           // _databaseLogic.InsertMillionData();

            if (fundName != _searchValue) { selectedPage = 1; }

            _selectedPage = selectedPage;
            _searchValue = fundName;
            _foundsList = GetTenFounds(fundName);

            var tupleData = new Tuple<IEnumerable<Fund>,int[]>(_foundsList, new int[] { _rowsCount, _pagesCount });
            return View(tupleData);
        }

        List<Fund> GetTenFounds(string fundName)
        {          
            _foundsList = _databaseLogic.GetDataWithName(fundName,_selectedPage);
            SetDropdown();
            return _foundsList;
        }

        void SetDropdown()
        {

            int pageCount = (PagesCount(_searchValue));
            var Values = SetDropdownRange(pageCount);
            var aList = Values.Select((x, i) => new { Value = x, Data = x }).ToList();            
            ViewBag.PageNums = new SelectList(aList, "Value", "Data", _selectedPage);        
        }

        int PagesCount(string fundName)
        {
            int pageCount = 0;
            pageCount = _databaseLogic.GetFundCount(fundName);
            _rowsCount = pageCount;
            pageCount = (pageCount % 10) == 0 ? pageCount / 10 : (pageCount / 10) + 1;
            _pagesCount = pageCount;

            return pageCount;
        }

        int[] SetDropdownRange(int pageCount)
        {
            int i = (_selectedPage > 5) ? (_selectedPage - 4) : 1;
            i = (i + 9) > pageCount ? (pageCount - 8) : i;

            int j = pageCount > 9 ? 9 : pageCount;          
            i = j < 5 ? pageCount - 9 : i;
            i = i < 1 ? 1 : i;

            return Enumerable.Range(i,j).ToArray();
        }


        #endregion

        #region DetailsView

        public ActionResult Details(int? id, string name, string description)
        {
            _details = GetDetails((int)id);
            var tupleData = new Tuple<IEnumerable<Value>, string, string>(_details, name, description);
            return View(tupleData);
        }

        public List<Value> GetDetails(int fundId)
        {
            return _databaseLogic.GetDetails(fundId);
        }

        #endregion
    }
}