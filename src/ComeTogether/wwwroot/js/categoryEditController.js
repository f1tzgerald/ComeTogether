(function () {

    "use strict";

    angular.module("tasks_app")
        .controller("categoryEditController", categoryEditController);

    function categoryEditController($routeParams, $http) {

        var vm = this;
        vm.categoryName = $routeParams.categoryName;
        
        

        vm.url = "api/category/edit/" + vm.categoryName;


        vm.editCategoryClick = function () {

            vm.isBusy = true;
            vm.errorMessage = "";

            $http.post(vm.url, vm.editCategory)
                .then(function () {
                    //success

                }, function () {
                    //failed
                })
                .finally(function () {
                    vm.isBusy = false;
                }
                );
        };


    }
})();