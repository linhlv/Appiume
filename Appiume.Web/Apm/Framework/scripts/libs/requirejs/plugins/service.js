define(function () {
    return {
        load: function (name, req, onload, config) {
            var url = apm.appPath + 'api/ApmServiceProxies/Get?name=' + name;
            req([url], function (value) {
                onload(value);
            });
        }
    };
});