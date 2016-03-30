/// <reference path="../lib/angular/angular.min.js" />
/// <reference path="../lib/angular/angular.js" />

(function () {

    "use strict";

    angular.module("tasks_app")
        .controller("toDoItemsController", toDoItemsController);

    function toDoItemsController($routeParams, $http) {

        var vm = this;

        vm.categoryId = $routeParams.categoryId;

        vm.errorMessage = "";
        vm.isBusy = true;

        vm.todoItems = [];

        var urlTasks = "/api/category/" + vm.categoryId + "/tasks";

        $http.get(urlTasks)
            .then(function (response) {
                //success
                vm.todoItems = response.data;
            }, function () {
                //error
                vm.errorMessage = "Failed to load To Do Items from db";
            }).finally(function () {
                vm.isBusy = false;
            });

        console.log(vm.todoItems);


        // ADD POST - ADD NEW TASK
        vm.newTodoitem = {};
        vm.newItemMenuToggle = false; // Show/hide add new category
        vm.textToggleBtn = "Show add todo form"
        vm.showNewItem = function () {
            vm.newItemMenuToggle = !vm.newItemMenuToggle;
            
            if (vm.newItemMenuToggle)
                vm.textToggleBtn = "Hide add todo form";
            else
                vm.textToggleBtn = "Show add todo form";
        };

        //to UTC time
        var x = new Date();
        x.setHours(x.getHours() - x.getTimezoneOffset() / 60);
        console.log(x);
        vm.today = x.toJSON().slice(0, 10);

        vm.addNewToDoItem = function () {
            console.log(vm.newTodoitem);

            // to UTC time
            // ERRRRORORORORRORORRO
            vm.newTodoitem.dateFinish.setHours(vm.newTodoitem.dateFinish.getHours() - vm.newTodoitem.dateFinish.getTimezoneOffset() / 60);

            vm.urlNewtask = "/api/category/" + vm.categoryId + "/tasks";
            $http.post(vm.urlNewtask, vm.newTodoitem)
                .then(function () {
                    //success
                    vm.todoItems.push(vm.newTodoitem);
                }, function () {
                    //failed

                })
                .finally(function () {
                    
                });

            vm.newTodoitem = {};
        };


        // Change checkbox status         
        vm.updateStatus = function (id, $index) {
            vm.updatedItem = vm.todoItems[$index];
            vm.updatedItem.done = !vm.updatedItem.done;

            vm.urlupdate = "/api/category/" + vm.categoryId + "/tasks/" + id;

            $http.put(vm.urlupdate, vm.updatedItem)
                .then(function (response) {
                    //success
                    console.log(vm.urlupdate + ": success");
                }, function (response) {
                    //failed
                    console.log("error");
                })
                .finally(function () {
                    vm.isBusy = false;
                });

        };

        //DELETE ALL DONE TASKS
        vm.deleteAllDone = function () {
            vm.todoItemsToDelete = [];

            for (var i = 0; i < vm.todoItems.length; i++) {
                if (vm.todoItems[i].done)
                    vm.todoItemsToDelete.push(vm.todoItems[i]);
            }

            console.log(vm.todoItemsToDelete);
        };

        // SHOW COMMENTS BTN
        vm.showComments = function (id) {

        };
    }
})();