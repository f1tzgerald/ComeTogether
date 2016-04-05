(function () {

    "use strict";

    angular.module("tasks_app_rec", ["ngRoute"])
            .config(function ($routeProvider) {

                $routeProvider.when("/", {
                    controller: "recycleController",
                    controllerAs: "vm",
                    templateUrl: "/views/recycleView.html"
                });

                $routeProvider.otherwise({ redirectTo: "/" });

            });
})();