/// <reference path="../lib/angular/angular.min.js" />
/// <reference path="../lib/angular/angular.js" />

(function () {

    "use strict";

    angular.module("tasks_app_rec").controller("recycleController", recycleController);

    function recycleController($http) {
        var vm = this;

        vm.todoItems = [];
        vm.urldeletedtasks = "/api/recycle";

        // Load deleted tasks
        $http.get(vm.urldeletedtasks)
            .then(function (response) {
                //success
                vm.todoItems = response.data;
            }, function () {
                //failed
            }).finally({});


        // Delete task from recycle
        vm.deleteTask = function (id, $index) {
            var urlTaskToDel = "/api/recycle/";
            $http.delete(urlTaskToDel + id)
                .then(function () {
                    //success
                    vm.todoItems.splice($index, 1);
                }, function () {
                    //error
                }).finally(function () {

                });
        };

        // Deleted all tasks
        vm.deleteAllDeleted = function () {
            var urlDelete = "/api/recycle/deleteAllDeleted";
            $http.delete(urlDelete)
                .then(function () {
                    //success
                    vm.refreshToDoItems();
                }, function () {
                    //error
                }).finally(function () {

                });
        };

        // Recover tasks to categories  
        vm.recoverTask = function (id, $index) {
            vm.updatedItem = vm.todoItems[$index];
            vm.updatedItem.isDeleted = false;

            vm.urlupdate = "/api/recycle/" + id;

            $http.put(vm.urlupdate, vm.updatedItem)
                .then(function (response) {
                    //success
                    console.log(vm.urlupdate + ": success");
                    vm.todoItems.splice($index, 1);
                }, function (response) {
                    //failed
                    console.log("error");
                })
                .finally(function () {
                    vm.isBusy = false;
                });
        };

    }
})();