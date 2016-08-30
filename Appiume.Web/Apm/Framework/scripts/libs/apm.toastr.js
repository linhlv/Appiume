﻿var apm = apm || {};
(function () {

    if (!toastr) {
        return;
    }

    /* DEFAULTS *************************************************/

    toastr.options.positionClass = 'toast-bottom-right';

    /* NOTIFICATION *********************************************/

    var showNotification = function (type, message, title, options) {
        toastr[type](message, title, options);
    };

    apm.notify.success = function (message, title, options) {
        showNotification('success', message, title, options);
    };

    apm.notify.info = function (message, title, options) {
        showNotification('info', message, title, options);
    };

    apm.notify.warn = function (message, title, options) {
        showNotification('warning', message, title, options);
    };

    apm.notify.error = function (message, title, options) {
        showNotification('error', message, title, options);
    };

})();