namespace Prihatin.Models
{
    public class Family
    {
        public string Name { get; set; }
        public int Age { get; set; }
        public string MaritalStatus { get; set; }
        public string Occupation { get; set; }
        public double Salary { get; set; }
        public double FamilyIncome { get; set; }
        public string Pensioner { get; set; }

        public double AprilPayment { get; set; }
        public double MayPayment { get; set; }
        public double OccupationPayment { get; set; }
        public double PensionerPayment { get; set; }

        public double TotalPayment { get; set; }

    }
}