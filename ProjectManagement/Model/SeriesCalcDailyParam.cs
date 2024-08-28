namespace ProjectManagement.Model
{
    public class SeriesCalcDailyParam
    {
        public double? Lat { get; set; }
        public double? Lon { get; set; }
        public int? StartYear { get; set; }
        public int? EndYear { get; set; }
        public int? PvCalculation { get; set; }
        public int? PeakPower { get; set; }
        public int? Loss { get; set; }
        public string? OutputFormat { get; set; }
        public int? UseHorizon { get; set; }
        public int MountNumber { get; set; }
    }
}
