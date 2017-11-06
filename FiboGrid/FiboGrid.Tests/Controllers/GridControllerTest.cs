using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using FiboGrid;
using FiboGrid.Controllers;

namespace FiboGrid.Tests.Controllers
{
    [TestClass]
    public class GridControllerTest
    {
        [TestMethod]
        public void GetCellsTest()
        {
            // Arrange
            GridController controller = new GridController();

            // Act
            JsonResult result = controller.GetCells() as JsonResult;

            // Assert
            Assert.IsNotNull(result);
        }
        
    }
}
