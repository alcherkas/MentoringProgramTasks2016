namespace Task.Data
{
    public class AverageIntensity
    {
        public AverageIntensity(string city, double value)
        {
            City = city;
            Value = value;
        }

        public string City { get; }
        public double Value { get; set; }
    }
}
