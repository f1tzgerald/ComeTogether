(function () {

    "use strict";

    angular.module("tasks_app_user", ["ngRoute"])
            .config(function ($routeProvider) {

                $routeProvider.when("/", {
                    controller: "userController",
                    controllerAs: "vm",
                    templateUrl: "/views/usersView.html"
                });

                $routeProvider.otherwise({ redirectTo: "/" });

            });
})();