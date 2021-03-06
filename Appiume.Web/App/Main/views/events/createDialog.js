﻿(function () {
    angular.module('app').controller('app.views.events.createDialog', [
        'apm.services.dewey.event', '$modalInstance',
        function (eventService, $modalInstance) {
            var vm = this;

            vm.event = {
                title: '',
                description: '',
                date: moment().add('day', 1).format('YYYY-MM-DD') + ' 09:00',
                maxRegistrationCount: 0
            };

            vm.save = function() {
                eventService
                    .create(vm.event)
                    .success(function () {
                        apm.notify.success("Successfully saved.");
                        $modalInstance.close();
                    });
            };

            vm.cancel = function () {
                $modalInstance.dismiss('cancel');
            };
        }
    ]);
})();