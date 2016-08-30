var apm = apm || {};
(function ($) {

    if (!$) {
        return;
    }

    /* JQUERY ENHANCEMENTS ***************************************************/

    // apm.ajax -> uses $.ajax ------------------------------------------------

    apm.ajax = function (userOptions) {
        userOptions = userOptions || {};

        var options = $.extend({}, apm.ajax.defaultOpts, userOptions);
        options.success = undefined;
        options.error = undefined;

        return $.Deferred(function ($dfd) {
            $.ajax(options)
                .done(function (data, textStatus, jqXHR) {
                    if (data.__apm) {
                        apm.ajax.handleResponse(data, userOptions, $dfd, jqXHR);
                    } else {
                        $dfd.resolve(data);
                        userOptions.success && userOptions.success(data);
                    }
                }).fail(function (jqXHR) {
                    if (jqXHR.responseJSON && jqXHR.responseJSON.__apm) {
                        apm.ajax.handleResponse(jqXHR.responseJSON, userOptions, $dfd, jqXHR);
                    } else {
                        apm.ajax.handleNonAbpErrorResponse(jqXHR, userOptions, $dfd);
                    }
                });
        });
    };

    $.extend(apm.ajax, {
        defaultOpts: {
            dataType: 'json',
            type: 'POST',
            contentType: 'application/json'
        },

        defaultError: {
            message: 'An error has occurred!',
            details: 'Error detail not sent by server.'
        },

        defaultError401: {
            message: 'You are not authenticated!',
            details: 'You should be authenticated (sign in) in order to perform this operation.'
        },

        defaultError403: {
            message: 'You are not authorized!',
            details: 'You are not allowed to perform this operation.'
        },

        logError: function (error) {
            apm.log.error(error);
        },

        showError: function (error) {
            if (error.details) {
                return apm.message.error(error.details, error.message);
            } else {
                return apm.message.error(error.message || apm.ajax.defaultError.message);
            }
        },

        handleTargetUrl: function (targetUrl) {
            if (!targetUrl) {
                location.href = apm.appPath;
            } else {
                location.href = targetUrl;
            }
        },

        handleNonAbpErrorResponse: function (jqXHR, userOptions, $dfd) {
            switch (jqXHR.status) {
                case 401:
                    apm.ajax.handleUnAuthorizedRequest(
                        apm.ajax.showError(apm.ajax.defaultError401),
                        apm.appPath
                    );
                    break;
                case 403:
                    apm.ajax.showError(apm.ajax.defaultError403);
                    break;
                default:
                    apm.ajax.showError(apm.ajax.defaultError);
                    break;
            }

            $dfd.reject.apply(this, arguments);
            userOptions.error && userOptions.error.apply(this, arguments);
        },

        handleUnAuthorizedRequest: function (messagePromise, targetUrl) {
            if (messagePromise) {
                messagePromise.done(function () {
                    apm.ajax.handleTargetUrl(targetUrl);
                });
            } else {
                apm.ajax.handleTargetUrl(targetUrl);
            }
        },

        handleResponse: function (data, userOptions, $dfd, jqXHR) {
            if (data) {
                if (data.success === true) {
                    $dfd && $dfd.resolve(data.result, data, jqXHR);
                    userOptions.success && userOptions.success(data.result, data, jqXHR);

                    if (data.targetUrl) {
                        apm.ajax.handleTargetUrl(data.targetUrl);
                    }
                } else if (data.success === false) {
                    var messagePromise = null;

                    if (data.error) {
                        messagePromise = apm.ajax.showError(data.error);
                    } else {
                        data.error = apm.ajax.defaultError;
                    }

                    apm.ajax.logError(data.error);

                    $dfd && $dfd.reject(data.error, jqXHR);
                    userOptions.error && userOptions.error(data.error, jqXHR);

                    if (jqXHR.status == 401) {
                        apm.ajax.handleUnAuthorizedRequest(messagePromise, data.targetUrl);
                    }
                } else { //not wrapped result
                    $dfd && $dfd.resolve(data, null, jqXHR);
                    userOptions.success && userOptions.success(data, null, jqXHR);
                }
            } else { //no data sent to back
                $dfd && $dfd.resolve(jqXHR);
                userOptions.success && userOptions.success(jqXHR);
            }
        },

        blockUI: function (options) {
            if (options.blockUI) {
                if (options.blockUI === true) { //block whole page
                    apm.ui.setBusy();
                } else { //block an element
                    apm.ui.setBusy(options.blockUI);
                }
            }
        },

        unblockUI: function (options) {
            if (options.blockUI) {
                if (options.blockUI === true) { //unblock whole page
                    apm.ui.clearBusy();
                } else { //unblock an element
                    apm.ui.clearBusy(options.blockUI);
                }
            }
        }
    });

    /* JQUERY PLUGIN ENHANCEMENTS ********************************************/

    /* jQuery Form Plugin 
     * http://www.malsup.com/jquery/form/
     */

    // apmAjaxForm -> uses ajaxForm ------------------------------------------

    if ($.fn.ajaxForm) {
        $.fn.apmAjaxForm = function (userOptions) {
            userOptions = userOptions || {};

            var options = $.extend({}, $.fn.apmAjaxForm.defaults, userOptions);

            options.beforeSubmit = function () {
                apm.ajax.blockUI(options);
                userOptions.beforeSubmit && userOptions.beforeSubmit.apply(this, arguments);
            };

            options.success = function (data) {
                apm.ajax.handleResponse(data, userOptions);
            };

            //TODO: Error?

            options.complete = function () {
                apm.ajax.unblockUI(options);
                userOptions.complete && userOptions.complete.apply(this, arguments);
            };

            return this.ajaxForm(options);
        };

        $.fn.apmAjaxForm.defaults = {
            method: 'POST'
        };
    }

    apm.event.on('apm.dynamicScriptsInitialized', function () {
        apm.ajax.defaultError.message = apm.localization.apmWeb('DefaultError');
        apm.ajax.defaultError.details = apm.localization.apmWeb('DefaultErrorDetail');
        apm.ajax.defaultError401.message = apm.localization.apmWeb('DefaultError401');
        apm.ajax.defaultError401.details = apm.localization.apmWeb('DefaultErrorDetail401');
        apm.ajax.defaultError403.message = apm.localization.apmWeb('DefaultError403');
        apm.ajax.defaultError403.details = apm.localization.apmWeb('DefaultErrorDetail403');
    });

})(jQuery);