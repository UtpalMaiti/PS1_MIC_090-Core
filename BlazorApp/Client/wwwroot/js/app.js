"use strict";

(function () {

    const app = angular.module('app', []);

    app.value('appSettingsValue', {
        title: 'Customers Application',
        version: '1.0'
    });

    app.constant('appSettingsConstant', {
        title: 'Customers Application',
        version: '1.0'
    });
    const appFactory = function ($http) {

        const factory = {};

        factory.getCustomers = function () {
            return $http.get('/customers');
        };

        factory.getCustomer = function (customerId) {
            return $http.get('/customers/' + customerId);
        };

        factory.getOrders = function () {
            return $http.get('/orders');
        }

        factory.deleteCustomer = function (customerId) {
            return $http.delete('/customers/' + customerId);
        }

        return factory;
    };

    appFactory.$inject = ['$http'];
    angular.module('app').factory('appFactory', appFactory);

    const appService = function ($http, $q) {
       
        this.createapplications = function (body) {

            var promiseObject = $q.defer();

            $http({
                method: 'POST',
                url: '/api/Application',
                data: JSON.stringify(body),
                headers: {
                    'Accept': 'application/json',
                    'Content-Type': 'application/json; charset=utf8',
                    "X-Login-Ajax-call": 'true'
                }
            }).then(function (list) {

                promiseObject.resolve(list.data);

            });

            return promiseObject.promise;
        };
    };


    appService.$inject = ['$http', '$q', '$log', '$window', 'appFactory', 'appSettingsValue', 'appSettingsConstant'];
    angular.module('app').service('appService', appService);


    const appController = function ($scope, $log, $window, appFactory, appService, appSettingsValue, appSettingsConstant) {
        const vm = $scope;

        vm.products = [];


        console.log(document);

        vm.getProducts = function () {
            fetch('https://dummyjson.com/products')
                .then(res => res.json())
                .then(json => {
                    console.log(json);
                    vm.products = json ?? []; 

                    const ul = $("#exampleAnimateContainerId");
                    if (ul) {

                        //$("head").text("Hello world!");
                    }

                });  
        }
         
        function init() {
            vm.getProducts();
        }
        init();
    };

    appController.$inject = ['$scope', '$log', '$window', 'appFactory', 'appService', 'appSettingsValue', 'appSettingsConstant'];

    angular.module('app')
        .controller('appController', appController);













   

    $(document).ready(function () {

        $("button").click(function (x) {
            console.log(x);
            $("p").text("Hello world!");
        });
    });
    //$(document).ready(function () {
    //    bootbox.alert('This is the default alert!');
    //});

  

}());