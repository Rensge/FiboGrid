using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using FiboGrid;
using FiboGrid.Controllers;
using FiboGrid.Models;

namespace FiboGrid.Tests.Controllers
{
    [TestClass]
    public class GridControllerTest
    {
        private static int gridSize = Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings["GridSize"]);

        [TestMethod]
        public void GetCellsTest()
        {
            // Arrange
            GridController controller = new GridController();

            // Act
            var result = controller.GetCells() as JsonResult;
            var list = result.Data as List<List<Cell>>;

            // Assert
            Assert.AreEqual(list.Count, gridSize);
            foreach (List<Cell> row in list)
            {
                Assert.AreEqual(row.Count, gridSize);
            }

            //random check
            Assert.AreEqual(list[4][3].Horizontal, 5);
            Assert.AreEqual(list[4][3].Vertical, 4);
            Assert.AreEqual(list[4][3].Value, 0);
            Assert.AreEqual(list[24][0].Horizontal, 25);
            Assert.AreEqual(list[24][0].Vertical, 1);
            Assert.AreEqual(list[24][0].Value, 0);
        }

        [TestMethod]
        public void FiboCheckWithFiboSequenceTest()
        {
            // Arrange
            GridController controller = new GridController();
            var cellValues = "[0,1,2,0,0,0,1,1,1,2,3,5]";
            var row = 2;
            var expectedResult = new List<int>() { 7, 8, 9, 10, 11 };

            // Act
            var result = controller.FiboCheck(cellValues, row) as JsonResult;
            var fiboResult = result.Data as FiboCheckResult;

            // Assert
            Assert.AreEqual(fiboResult.Row, row);
            Assert.AreEqual(fiboResult.FiboSerieCells.Count, expectedResult.Count);
            foreach (int value in fiboResult.FiboSerieCells) {
                Assert.IsTrue(expectedResult.Contains(value));
            }
        }

        [TestMethod]
        public void FiboCheckWithoutFiboSequenceTest()
        {
            // Arrange
            GridController controller = new GridController();
            var cellValues = "[0,1,2,3,8,0,1,1,1,9,3,5]";
            var row = 2;

            // Act
            var result = controller.FiboCheck(cellValues, row) as JsonResult;
            var fiboResult = result.Data as FiboCheckResult;

            // Assert
            Assert.AreEqual(fiboResult.FiboSerieCells.Count, 0);
        }
    }
}
