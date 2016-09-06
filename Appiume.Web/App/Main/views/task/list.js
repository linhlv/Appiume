(function() {
    var app = angular.module('app');

    var controllerId = 'app.views.task.list';
    app.controller(controllerId, [
        '$scope', 'apm.services.dewey.task',
        function($scope, taskService) {
            var vm = this;

            vm.localize = apm.localization.getSource('TaskCloud');

            vm.tasks = [];

            $scope.selectedTaskState = 0;

            $scope.$watch('selectedTaskState', function(value) {
                vm.refreshTasks();
            });

            vm.refreshTasks = function() {
                apm.ui.setBusy( //Set whole page busy until getTasks complete
                    null,
                    taskService.getTasks({ //Call application service method directly from javascript
                        state: $scope.selectedTaskState > 0 ? $scope.selectedTaskState : null
                    }).success(function(data) {
                        vm.tasks = data.tasks;
                    })
                );
            };

            vm.changeTaskState = function(task) {
                var newState;
                if (task.state == 1) {
                    newState = 2; //Completed
                } else {
                    newState = 1; //Active
                }

                taskService.updateTask({
                    taskId: task.id,
                    state: newState
                }).success(function() {
                    task.state = newState;
                    apm.notify.info(vm.localize('TaskUpdatedMessage'));
                });
            };

            vm.getTaskCountText = function() {
                return apm.utils.formatString(vm.localize('Xtasks'), vm.tasks.length);
            };
        }
    ]);
})();