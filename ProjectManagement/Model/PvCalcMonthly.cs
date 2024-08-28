namespace ProjectManagement.Model
{
    public class PvCalcMonthly
    {
        public int MonthNo { get; set; }

        public double ProductionkWh { get; set; }
        public double ClippedEnergy { get; set; }
        public double Consumption { get; set; }
        public double SelfConsumption { get; set; }

    }
}
