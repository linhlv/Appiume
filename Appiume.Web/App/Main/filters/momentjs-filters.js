(function () {

    angular.module('app').filter('momentFormat', function () {
        return function (date, formatStr) {
            if (!date) {
                return '-';
            }

            return moment(date).format(formatStr);
        };
    })
    .filter('momentFromNow', function () {
        return function (date) {
            return moment(date).fromNow();
        };
    });

})();