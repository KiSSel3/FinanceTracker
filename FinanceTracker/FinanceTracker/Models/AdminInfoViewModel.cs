namespace FinanceTracker.Models
{
    public class AdminInfoViewModel
    {
        public int AverageAge { get; set; }
        public int MinAge { get; set; }
        public int MaxAge { get; set; }

        public decimal AverageExpense { get; set; }
        public decimal MinExpense { get; set; }
        public decimal MaxExpense { get; set; }

        public decimal AverageIncome{ get; set; }
        public decimal MinIncome { get; set; }
        public decimal MaxIncome { get; set; }
    }
}
