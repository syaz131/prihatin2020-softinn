(function () {
    var app = angular.module("family", []);

    var MainCtrl = function ($scope, $http) {

        $scope.forms = {
            familyMember: [{ name: '', age: null, maritalStatus: '', occupation: '', salary: null, familyIncome: null, pensioner: '' }],
            payments: []
        };

        $scope.grandTotal = 0;
        $scope.countPayment = 0;
        $scope.countForm = 1;

        $scope.addMember = function (form) {
            $scope.countForm += 1;
            form.familyMember.push({ name: '', age: null, maritalStatus: '', occupation: '', salary: null, familyIncome: null, pensioner: '' });
        };

        $scope.removeMember = function (form, index) {
            var totalPayment = form.payments[index].TotalPayment;
            $scope.grandTotal -= totalPayment;
            $scope.countPayment -= 1;
            form.payments.splice(index, 1);
        };

        $scope.checkAge = function (value) {
            if (value < 0 || Number.isInteger(value) == false) {
                document.getElementById("error_age").innerHTML = "Please enter a valid Age number";
            }
            else {
                document.getElementById("error_age").innerHTML = "";
            }
        }

        $scope.checkSalary = function (value) {
            if (value < 0 || Number.isInteger(value) == false) {
                document.getElementById("error_salary").innerHTML = "Please enter a valid Whole number";
            }
            else {
                document.getElementById("error_salary").innerHTML = "";
            }
        }

        $scope.checkFamIncome = function (value, salary, maritalStatus) {
            if (maritalStatus == "")
                document.getElementById("error_famIncome").innerHTML = "Fill in Marital Status first";
            else if ((value < 0 || Number.isInteger(value) == false || salary > value) && (maritalStatus == "Married - Leader" || maritalStatus == "Single")) {
                document.getElementById("error_famIncome").innerHTML = "Please enter a valid Income number. Family income must be same or higher than salary";
            }
            else {
                document.getElementById("error_famIncome").innerHTML = "";
            }
        }

        $scope.checkSubmitBtnDisable = function (member) {

            if (!member.name)
                return true;
            else if (!member.age)
                return true;
            else if (!member.maritalStatus)
                return true;
            else if (!member.occupation)
                return true;
            else if (!member.salary)
                return true;
            else if (!member.familyIncome)
                return true;
            else if (!member.pensioner)
                return true;
            else
                return (false);
        }

        $scope.calculate = function (member, form, index) {

            var name = member.name;
            var age = member.age;
            var maritalStatus = member.maritalStatus;
            var occupation = member.occupation;
            var salary = member.salary;
            var familyIncome = member.familyIncome;
            var pensioner = member.pensioner;


            var url = "https://localhost:44346/api/family?name=" + name + "&age=" + age + "&maritalStatus=" + maritalStatus +
                "&occupation=" + occupation + "&salary=" + salary + "&familyIncome=" + familyIncome + "&pensioner=" + pensioner;

            var onComplete = function (response) {
                $scope.result = response.data;
                $scope.countPayment += 1;
                $scope.countForm -= 1;

                var totalPayment = response.data.TotalPayment;
                $scope.grandTotal += totalPayment;

                form.payments.push(response.data);
                form.familyMember.splice(index, 1);

                console.log($scope.result);
            }

            var onError = function () {
                console.log("Could not get data");
            }

            $http.get(url).then(onComplete, onError);
        }
    };
    app.controller("MainCtrl", MainCtrl);
}())