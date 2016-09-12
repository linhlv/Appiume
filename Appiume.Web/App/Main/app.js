(function () {
    'use strict';

    var app = angular.module('app', [
        'ngAnimate',
        'ngResource',
        'ngSanitize',        

        'ui.router',
        'ui.bootstrap',
        'ui.jq',

        'apm'
    ]);

    //Configuration for Angular UI routing.
    app.config([
        '$stateProvider', '$urlRouterProvider',
        function($stateProvider, $urlRouterProvider) {
            $urlRouterProvider.otherwise('/events');
            $stateProvider
                .state('events', {
                    url: '/events',
                    templateUrl: '/App/Main/views/events/index.cshtml',
                    menu: 'Events' //Matches to name of 'Events' menu in EventCloudNavigationProvider
                })
                .state('eventDetail', {
                    url: '/events/:id',
                    templateUrl: '/App/Main/views/events/detail.cshtml',
                    menu: 'Events' //Matches to name of 'Events' menu in EventCloudNavigationProvider
                })
                .state('tasklist', {
                       url: '/tasks',
                       templateUrl: '/App/Main/views/task/list.cshtml',
                       menu: 'TaskList' //Matches to name of 'TaskList' menu in SimpleTaskSystemNavigationProvider
                   })
                .state('newtask', {
                    url: '/tasks/new',
                    templateUrl: '/App/Main/views/task/new.cshtml',
                    menu: 'NewTask' //Matches to name of 'NewTask' menu in SimpleTaskSystemNavigationProvider
                })
                .state('about', {
                    url: '/about',
                    templateUrl: '/App/Main/views/about/about.cshtml',
                    menu: 'About' //Matches to name of 'About' menu in EventCloudNavigationProvider
                })
                 .state('users', {
                     url: '/users',
                     templateUrl: '/App/Main/views/users/index.cshtml',
                     menu: 'Users' //Matches to name of 'Users' menu in ModuleZeroSampleProjectNavigationProvider
                 });
        }
    ]);
})();