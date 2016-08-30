var apm = apm || {};
(function ($) {

    //Check if SignalR is defined
    if (!$ || !$.connection) {
        return;
    }

    //Create namespaces
    apm.signalr = apm.signalr || {};
    apm.signalr.hubs = apm.signalr.hubs || {};

    //Get the common hub
    apm.signalr.hubs.common = $.connection.getzCommonHub;

    var commonHub = apm.signalr.hubs.common;
    if (!commonHub) {
        return;
    }

    //Register to get notifications
    commonHub.client.getNotification = function (notification) {
        apm.event.trigger('apm.notifications.received', notification);
    };

    //Connect to the server
    apm.signalr.connect = function() {
        $.connection.hub.start().done(function () {
            apm.log.debug('Connected to SignalR server!'); //TODO: Remove log
            apm.event.trigger('apm.signalr.connected');
            commonHub.server.register().done(function () {
                apm.log.debug('Registered to the SignalR server!'); //TODO: Remove log
            });
        });
    };

    if (apm.signalr.autoConnect === undefined) {
        apm.signalr.autoConnect = true;
    }

    if (apm.signalr.autoConnect) {
        apm.signalr.connect();
    }

})(jQuery);