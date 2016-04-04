/// <reference path="../lib/angular/angular.min.js" />
/// <reference path="../lib/angular/angular.js" />

(function () {

    "use strict";

    angular.module("tasks_app").controller("recycleController", recycleController);

    function recycleController($http) {
        var vm = this;

        vm.urldeletedtasks = "/api/recycle";

        // Load deleted tasks
        $http.get(vm.urldeletedtasks)
            .then(function () {
                //success
            }, function () {
                //failed
            }).finally({});

        // Delete task from recycle


        // Deleted all tasks


        // Recover tasks to categories
    }
})();