var app = angular.module('FiboGridApp', []);

app.controller('GridController', function ($scope, CellService, $http) {
    $scope.Grid = null;
    $scope.fiboCells = null;

    function Getcells() {
        CellService.GetCells()
            .then(function (response) {
                $scope.Grid = response.data;
            })
            .catch(function (data, status) {
                console.error('Error getting cells for grid', response.status, response.data);
            })
            .finally(function () {
                console.log("Cells succesfully retrieved");
            });
    };
    Getcells();

    $scope.ClickCell = function (clickedCell) {

        IncreaseValue(clickedCell);
                     
        setTimeout(function () {            
                $scope.$apply(function () {
                    ClearChangedColor(clickedCell);
                });
        }, 1000);

        setTimeout(function () {
            {
                $scope.$apply(function () {
                    FiboCheck(clickedCell);
                });
            };
        }, 1000);     

        setTimeout(function () {
            {
                $scope.$apply(function () {
                    ClearClearedColor();
                });
            };
        }, 2000);  
    }

    function IncreaseValue(clickedCell) {


        var grid = $scope.Grid;

        // increasing cell values in the row of the clicked cellvar 
        var cellRow = grid[clickedCell.Horizontal - 1]
        for (cell in cellRow) {
            var rowCell = cellRow[cell];
            rowCell.Value++;
            rowCell.Changed = true;
        }

        // increasing cell values in the column of the clicked cell
        for (row in grid) {
            if (row != clickedCell.Horizontal - 1) {
                columnCell = grid[row][clickedCell.Vertical - 1];
                columnCell.Value++;
                columnCell.Changed = true;
            }
        }
    }

    function ClearChangedColor(clickedCell) {

        var grid = $scope.Grid;
        var cellRow = grid[clickedCell.Horizontal - 1]

        for (cell in cellRow) {
            cellRow[cell].Changed = false;
        }

        for (row in grid) {
            columnCell = grid[row][clickedCell.Vertical - 1].Changed = false;
        }
    }

    function FiboCheck(clickedCell) {
        var grid = $scope.Grid;
        for (row in grid) {                

            var cellValues = [];
            for (cell in $scope.Grid[row]) {
                
                if (cell != "$$hashKey") {
                    cellValues.push(grid[row][cell].Value);
                };
            }
            
            CellService.FiboCheck(cellValues, row)
                .then(function (response) {
                    var processedrow = response.data.Row;
                    var fiboCells = response.data.FiboSerieCells; 

                    for (index in fiboCells) {                        
                        grid[processedrow][fiboCells[index]].Cleared = true;
                        grid[processedrow][fiboCells[index]].Value = 0;
                    }
                })
                .catch(function (data, status) {
                    console.error('Error executing fibocheck', response.status, response.data);
                })
                .finally(function () {
                    console.log("Fibocheck executed succesfully");
                });
        }

    }

    function ClearClearedColor() {
        var grid = $scope.Grid;

        for (row in grid) {
            for (cell in grid[row]) {
                grid[row][cell].Cleared = false;
            }
        }
    }
});