﻿(function () {
    var controllerId = 'app.views.about';
    angular.module('app').controller(controllerId, [
        '$scope', 'apm.services.dewey.statistics',
        function ($scope, statisticsService) {
            var vm = this;

            vm.statisticItems = [];

            statisticsService.getStatistics().success(function (result) {
                vm.statisticItems = result.items;
            });
        }
    ]);
})();