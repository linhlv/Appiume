var apm = apm || {};
(function ($) {
    if (!sweetAlert || !$) {
        return;
    }

    /* DEFAULTS *************************************************/

    apm.libs = apm.libs || {};
    apm.libs.sweetAlert = {
        config: {
            'default': {

            },
            info: {
                type: 'info'
            },
            success: {
                type: 'success'
            },
            warn: {
                type: 'warning'
            },
            error: {
                type: 'error'
            },
            confirm: {
                type: 'warning',
                title: 'Are you sure?',
                showCancelButton: true,
                cancelButtonText: 'Cancel',
                confirmButtonColor: "#DD6B55",
                confirmButtonText: 'Yes'
            }
        }
    };

    /* MESSAGE **************************************************/

    var showMessage = function (type, message, title) {
        if (!title) {
            title = message;
            message = undefined;
        }

        var opts = $.extend(
            {},
            apm.libs.sweetAlert.config.default,
            apm.libs.sweetAlert.config[type],
            {
                title: title,
                text: message
            }
        );

        return $.Deferred(function ($dfd) {
            sweetAlert(opts, function () {
                $dfd.resolve();
            });
        });
    };

    apm.message.info = function (message, title) {
        return showMessage('info', message, title);
    };

    apm.message.success = function (message, title) {
        return showMessage('success', message, title);
    };

    apm.message.warn = function (message, title) {
        return showMessage('warn', message, title);
    };

    apm.message.error = function (message, title) {
        return showMessage('error', message, title);
    };

    apm.message.confirm = function (message, titleOrCallback, callback) {
        var userOpts = {
            text: message
        };

        if ($.isFunction(titleOrCallback)) {
            callback = titleOrCallback;
        } else if (titleOrCallback) {
            userOpts.title = titleOrCallback;
        };

        var opts = $.extend(
            {},
            apm.libs.sweetAlert.config.default,
            apm.libs.sweetAlert.config.confirm,
            userOpts
        );

        return $.Deferred(function ($dfd) {
            sweetAlert(opts, function (isConfirmed) {
                callback && callback(isConfirmed);
                $dfd.resolve(isConfirmed);
            });
        });
    };

    apm.event.on('getz.dynamicScriptsInitialized', function () {
        apm.libs.sweetAlert.config.confirm.title = apm.localization.getzWeb('AreYouSure');
        apm.libs.sweetAlert.config.confirm.cancelButtonText = apm.localization.getzWeb('Cancel');
        apm.libs.sweetAlert.config.confirm.confirmButtonText = apm.localization.getzWeb('Yes');
    });

})(jQuery);