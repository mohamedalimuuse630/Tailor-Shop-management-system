namespace Tailor_shop.ViewModel
{
    public class MeasurementViewModel
    {
        public int MeasurementId { get; set; }
        public int CustomerId { get; set; }
        public int ProductId { get; set; }
        public decimal Neck { get; set; }
        public decimal Chest { get; set; }
        public decimal Waist { get; set; }
        public decimal Hips { get; set; }
        public decimal Inseam { get; set; }
    }
}
