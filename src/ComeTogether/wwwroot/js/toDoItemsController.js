(function () {

    "use strict";

    angular.module("tasks_app")
        .controller("toDoItemsController", toDoItemsController);

    function toDoItemsController($routeParams, $http) {

        var vm = this;

        vm.categoryName = $routeParams.categoryName;

        vm.errorMessage = "";
        vm.isBusy = true;

        vm.todoItems = [];

        var urlTasks = "/api/category/" + vm.categoryName + "/tasks";

        $http.get(urlTasks)
            .then(function (response) {
                //success
                angular.copy(response.data, vm.todoItems);
            }, function () {
                //error
                vm.errorMessage = "Failed to load To Do Items from db";
            }).finally(function () {
                vm.isBusy = false;
            });


        // ADD POST - ADD NEW TASK
    }
})();