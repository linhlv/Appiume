(function() {
    var app = angular.module('app');

    var controllerId = 'app.views.task.new';
    app.controller(controllerId, [
        '$scope', '$location', 'apm.services.dewey.task', 'apm.services.dewey.person',
        function($scope, $location, taskService, personService) {
            var vm = this;

            vm.task = {
                description: '',
                assignedPersonId: null
            };

            var localize = apm.localization.getSource('TaskCloud');

            vm.people = []; //TODO: Move Person combo to a directive?

            personService.getAllPeople().success(function(data) {
                vm.people = data.people;
            });

            vm.saveTask = function() {
                apm.ui.setBusy(
                    null,
                    taskService.createTask(
                        vm.task
                    ).success(function() {
                        apm.notify.info(apm.utils.formatString(localize("TaskCreatedMessage"), vm.task.description));
                        $location.path('/tasks');
                    })
                );
            };
        }
    ]);
})();