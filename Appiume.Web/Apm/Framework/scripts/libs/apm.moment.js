var apm = apm || {};
(function () {
    if (!moment || !moment.tz) {
        return;
    }

    /* DEFAULTS *************************************************/

    apm.timing = apm.timing || {};

    /* FUNCTIONS **************************************************/

    apm.timing.convertToUserTimezone = function (date) {
        var momentDate = moment(date);
        var targetDate = momentDate.clone().tz(apm.timing.timeZoneInfo.iana.timeZoneId);
        return targetDate;
    };

})();