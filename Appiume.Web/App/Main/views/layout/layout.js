(function () {
    var controllerId = 'app.views.layout';
    angular.module('app').controller(controllerId, [
        '$scope', '$state', function ($scope, $state) {
            var vm = this;

            //Welcome Message
            //growlService.growl('Welcome back Mallinda!', 'inverse')


            // Detact Mobile Browser
            if (/Android|webOS|iPhone|iPad|iPod|BlackBerry|IEMobile|Opera Mini/i.test(navigator.userAgent)) {
                angular.element('html').addClass('ismobile');
            }

            //Skin Switch
            vm.currentSkin = 'blue';

            vm.skinList = [
                'lightblue',
                'bluegray',
                'cyan',
                'teal',
                'green',
                'orange',
                'blue',
                'purple'
            ];

            // By default Sidbars are hidden in boxed layout and in wide layout only the right sidebar is hidden.
            vm.sidebarToggle = {
                left: false,
                right: false
            }

            // By default template has a boxed layout
            vm.layoutType = localStorage.getItem('ma-layout-status');

            // For Mainmenu Active Class
            vm.$state = $state;

            //Close sidebar on click
            vm.sidebarStat = function (event) {
                if (!angular.element(event.target).parent().hasClass('active')) {
                    vm.sidebarToggle.left = false;
                }
            }

            //Listview Search (Check listview pages)
            vm.listviewSearchStat = false;

            vm.lvSearch = function () {
                vm.listviewSearchStat = true;
            }

            vm.skinSwitch = function (color) {
                vm.currentSkin = color;
            }

            //Layout logic...
    }]);
})();