(function () {

    "use strict";

    angular.module("tasks_app")
        .controller("categoryEditController", categoryEditController);

    function categoryEditController($routeParams, $http) {

        var vm = this;
        vm.categoryId = $routeParams.categoryId;
        vm.categoryName = "";
        

        vm.url = "api/category/edit/" + vm.categoryId;


        vm.editCategoryClick = function () {

            vm.isBusy = true;
            vm.errorMessage = "";
            vm.category = {};
            console.log("Click edit category click");

            $http({
                method: 'PUT',
                url: vm.url,
                data: { Id: vm.categoryId, Name: vm.categoryName}
            })
                .then(function (response) {
                    //success
                    console.log(response.data);
                    console.log(vm.url);

                    return response.data;
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