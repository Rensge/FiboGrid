angular.module('FiboGridApp')
    .factory('CellService', function ($http) {

        var data = {};

        data.GetCells = function () {
            return $http.get('/Grid/GetCells');
        };
    
        data.FiboCheck = function (cellValues, row) {
            return $http.get('/Grid/FiboCheck?cellValues=' + cellValues + '&row='+ row);
        };

        return data;        
    });

