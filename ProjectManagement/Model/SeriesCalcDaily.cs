namespace ProjectManagement.Model
{
    public class SeriesCalcDaily
    {
        public int Hour { get; set; }

        public double PVSystemPowerW { get; set; }
        public double Consumption { get; set; }
        public double SystemCapacity { get; set; }
        public double ClippedEnergy { get; set; }
    }
}
