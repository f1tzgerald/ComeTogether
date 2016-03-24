(function () {

    "use strict";

    angular.module("tasks_app", ["ngRoute"])
            .config(function ($routeProvider) {

                $routeProvider.when("/", {
                    controller: "categoryController",
                    controllerAs: "vm",
                    templateUrl: "/views/categoryView.html"
                });

                $routeProvider.when("/:categoryName/tasks", {
                    controller: "toDoItemsController",
                    controllerAs: "vm",
                    templateUrl: "/views/toDoItemsView.html"
                });

                $routeProvider.when("/edit/:categoryName", {
                    controller: "categoryEditController",
                    controllerAs: "vm",
                    templateUrl: "/views/editCategory.html"
                })

                $routeProvider.otherwise({ redirectTo: "/"});

            });
})();