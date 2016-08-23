namespace Task.Data
{
    public class AverageProfitability
    {
        public AverageProfitability(string city, decimal value)
        {
            City = city;
            Value = value;
        }

        public string City { get; }
        public decimal Value { get; }
    }
}
