using Prihatin.Models;
using System.Web.Http;


namespace Prihatin.Controllers
{
    public class FamilyController : ApiController
    {
        [HttpGet]
        public IHttpActionResult Get(string name, int age, string maritalStatus, string occupation, double salary, double familyIncome, string pensioner)
        {

            var model = new Family();
            var occupation_payment = OccupationPayment(occupation);
            var april_payment = AprilPayment(maritalStatus, familyIncome, salary, age, occupation);
            var may_payment = MayPayment(maritalStatus, familyIncome, salary, age, occupation);
            var Pensioner = PensionerPayment(pensioner);

            model.Name = name;
            model.Age = age;
            model.MaritalStatus = maritalStatus;
            model.Occupation = occupation;
            model.Salary = salary;
            model.FamilyIncome = familyIncome;
            model.OccupationPayment = occupation_payment;
            model.AprilPayment = april_payment;
            model.MayPayment = may_payment;
            model.Pensioner = pensioner;
            model.PensionerPayment = Pensioner;
            model.TotalPayment = occupation_payment + april_payment + may_payment + Pensioner;

            return Ok(model);
        }


        public double OccupationPayment(string occupation)
        {
            var Occupation = occupation;


            if (Occupation == "Student")
                return 200;
            else if (Occupation == "e-Hailing")
                return 500;
            else if (Occupation == "Taxi Driver")
                return 600;
            else if (Occupation == "Civil Servant (Grade 56 and below)")
                return 500;
            else
                return 0;

        }

        public double PensionerPayment(string pensioner)
        {
            if (pensioner == "Pensioner")
                return 500;
            else
                return 0;
        }

        public double AprilPayment(string maritalStatus, double famIncome, double salary, int age, string occupation)
        {
            if (maritalStatus == "Married - Leader")
            {
                if (famIncome > 0 && famIncome <= 4000) //married b40
                    return 1000;
                if (famIncome > 4000 && famIncome <= 8000) //married m40
                    return 500;
                else
                    return 0;   //t20
            }
            if (maritalStatus == "Single" && age >= 21 && occupation != "Student")
            {
                if (salary > 0 && salary <= 2000) //single b40
                    return 500;
                if (salary > 2000 && salary <= 4000) //single m40
                    return 250;
                else
                    return 0;   //t20
            }
            else
                return 0; //neither single / married
        }

        public double MayPayment(string maritalStatus, double famIncome, double salary, int age, string occupation)
        {
            if (maritalStatus == "Married - Leader")
            {
                if (famIncome > 0 && famIncome <= 4000) //b40
                    return 600;
                if (famIncome > 4000 && famIncome <= 8000) //m40
                    return 500;
                else
                    return 0;
            }
            if (maritalStatus == "Single" && age >= 21 && occupation != "Student")
            {
                if (salary > 0 && salary <= 2000) //b40
                    return 300;
                if (salary > 2000 && salary <= 4000) //m40
                    return 250;
                else
                    return 0;
            }
            else
                return 0;    //neither single / married
        }
    }
}
