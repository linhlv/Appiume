(function () {
    angular.module('app').factory('appSession', [
            function () {

                var _session = {
                    user: null,
                    tenant: null
                };

                apm.services.dewey.session.getCurrentLoginInformations({ async: false }).done(function (result) {
                    _session.user = result.user;
                    _session.tenant = result.tenant;
                });

                return _session;
            }
        ]);
})();