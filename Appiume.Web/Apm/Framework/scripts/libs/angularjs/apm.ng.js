(function (apm, angular) {

    if (!angular) {
        return;
    }

    apm.ng = apm.ng || {};

    apm.ng.http = {
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
                return apm.message.error(error.details, error.message || apm.ng.http.defaultError.message);
            } else {
                return apm.message.error(error.message || apm.ng.http.defaultError.message);
            }
        },

        handleTargetUrl: function (targetUrl) {
            if (!targetUrl) {
                location.href = apm.appPath;
            } else {
                location.href = targetUrl;
            }
        },

        handleNonAbpErrorResponse: function (response, defer) {
            switch (response.status) {
                case 401:
                    apm.ng.http.handleUnAuthorizedRequest(
                        apm.ng.http.showError(apm.ng.http.defaultError401),
                        apm.appPath
                    );
                    break;
                case 403:
                    apm.ng.http.showError(apm.ajax.defaultError403);
                    break;
                default:
                    apm.ng.http.showError(apm.ng.http.defaultError);
                    break;
            }

            defer.reject(response);
        },

        handleUnAuthorizedRequest: function (messagePromise, targetUrl) {
            if (messagePromise) {
                messagePromise.done(function () {
                    apm.ng.http.handleTargetUrl(targetUrl || apm.appPath);
                });
            } else {
                apm.ng.http.handleTargetUrl(targetUrl || apm.appPath);
            }
        },

        handleResponse: function (response, defer) {
            var originalData = response.data;

            if (originalData.success === true) {
                response.data = originalData.result;
                defer.resolve(response);

                if (originalData.targetUrl) {
                    apm.ng.http.handleTargetUrl(originalData.targetUrl);
                }
            } else if (originalData.success === false) {
                var messagePromise = null;

                if (originalData.error) {
                    messagePromise = apm.ng.http.showError(originalData.error);
                } else {
                    originalData.error = defaultError;
                }

                apm.ng.http.logError(originalData.error);

                response.data = originalData.error;
                defer.reject(response);

                if (response.status == 401) {
                    apm.ng.http.handleUnAuthorizedRequest(messagePromise, originalData.targetUrl);
                }
            } else { //not wrapped result
                defer.resolve(response);
            }
        }
    }

    var apmModule = angular.module('apm', []);

    apmModule.config([
        '$httpProvider', function ($httpProvider) {
            $httpProvider.interceptors.push(['$q', function ($q) {

                return {

                    'request': function (config) {
                        if (endsWith(config.url, '.cshtml')) {
                            config.url = apm.appPath + 'AbpAppView/Load?viewUrl=' + config.url + '&_t=' + apm.pageLoadTime.getTime();
                        }

                        return config;
                    },

                    'response': function (response) {
                        if (!response.data || !response.data.__apm) {
                            //Non ABP related return value
                            return response;
                        }

                        var defer = $q.defer();
                        apm.ng.http.handleResponse(response, defer);
                        return defer.promise;
                    },

                    'responseError': function (ngError) {
                        var defer = $q.defer();

                        if (!ngError.data || !ngError.data.__apm) {
                            apm.ng.http.handleNonAbpErrorResponse(ngError, defer);
                        } else {
                            apm.ng.http.handleResponse(ngError, defer);
                        }

                        return defer.promise;
                    }

                };
            }]);
        }
    ]);

    function endsWith(str, suffix) {
        if (suffix.length > str.length) {
            return false;
        }

        return str.indexOf(suffix, str.length - suffix.length) !== -1;
    }

    apm.event.on('apm.dynamicScriptsInitialized', function () {
        apm.ng.http.defaultError.message = apm.localization.apmWeb('DefaultError');
        apm.ng.http.defaultError.details = apm.localization.apmWeb('DefaultErrorDetail');
        apm.ng.http.defaultError401.message = apm.localization.apmWeb('DefaultError401');
        apm.ng.http.defaultError401.details = apm.localization.apmWeb('DefaultErrorDetail401');
        apm.ng.http.defaultError403.message = apm.localization.apmWeb('DefaultError403');
        apm.ng.http.defaultError403.details = apm.localization.apmWeb('DefaultErrorDetail403');
    });

})((apm || (apm = {})), (angular || undefined));