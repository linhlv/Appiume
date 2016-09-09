(function () {
    var app = angular.module('app');

    var controllerId = 'app.views.users.index';
    app.controller(controllerId, [
        'apm.services.dewey.user',
        function (userService) {
            var vm = this;

            vm.users = [];

            apm.ui.setBusy(
                null,
                userService.getUsers().success(function(data) {
                    vm.users = data.items;
                })
            );
        }
    ]);
})();