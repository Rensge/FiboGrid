angular.module('FiboGridApp')
    .factory('CellService', function ($http) {

        var data = {};

        // get a list of rows with cells
        data.GetCells = function () {
            return $http.get('/Grid/GetCells');
        };

        // check a row of cell values on existence of fibonacci sequences
        data.FiboCheck = function (cellValues, row) {
            return $http.get('/Grid/FiboCheck?cellValues=' + cellValues + '&row='+ row);
        };

        return data;        
    });

