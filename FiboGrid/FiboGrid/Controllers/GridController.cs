using FiboGrid.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Helpers;
using System.Web.Mvc;

namespace FiboGrid.Controllers
{
    public class GridController : Controller
    {
        private static int gridSize = Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings["GridSize"]);
        
        public JsonResult GetCells()
        {
            var cellList = new List<List<Cell>>();

            for (int i = 1; i <= gridSize; i++)
            {
                var rowCellList = new List<Cell>();

                for (int a = 1; a <= gridSize; a++)
                {
                    var columnCell = new Cell()
                    {
                        Horizontal = i,
                        Vertical = a,
                        Value = 0
                    };
                    rowCellList.Add(columnCell);
                }

                cellList.Add(rowCellList);

            }
            return new JsonResult { Data = cellList, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }

        // receives a string with comma seperated values and the rowindex
        // returns a list with indexes where fibonacci sequences are found
        public JsonResult FiboCheck(string cellValues, int row) {

            var fiboSerieEndValues = new List<int>();

            var numberList = cellValues.Trim('[',']').Split(',').Select(Int32.Parse).ToList();

            for (int i = 4; i <= numberList.Count - 1; i++)
            {
                var var = IsFiboNumber(numberList[i]);
                if (!IsFiboNumber(numberList[i])) {
                    continue;
                }
                    
                if (IsFiboNumber(numberList[i - 4]) &&
                    IsFiboNumber(numberList[i - 3]) &&
                    IsFiboNumber(numberList[i - 2]) &&
                    IsFiboNumber(numberList[i - 1]) &&
                    IsInCorrectOrder(numberList[i - 4], numberList[i - 3], numberList[i - 2], numberList[i - 1], numberList[i]))
                {
                    fiboSerieEndValues.AddRange(new List<int> () { i - 4, i - 3, i - 2, i - 1, i });
                }
            }
            
            var result = new FiboCheckResult () { Row = row, FiboSerieCells = fiboSerieEndValues };
            

            return new JsonResult { Data = result, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }

        //Equation modified from http://www.geeksforgeeks.org/check-number-fibonacci-number/
        private bool IsFiboNumber (int numberValue)
        {   
            // return true when number belongs to fibonacci sequence
            return IsPerfectSquare(5 * numberValue * numberValue + 4) ||
                   IsPerfectSquare(5 * numberValue * numberValue - 4);
        }

        private bool IsPerfectSquare(int number)
        {
            return number > 0 && Math.Sqrt(number) % 1 == 0;
        }

        private bool IsInCorrectOrder(int number1, int number2, int number3, int number4, int number5)
        {
            // number values should be in ascending order
            return (number5 == number4 + number3 && number3 < number4) &&
                   (number4 == number3 + number2 && number2 <= number3) &&
                   (number3 == number2 + number1 && number1 <= number2);
        }
    }
}