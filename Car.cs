namespace test1
{
    public class Car
    {
        public string make { get; set; }
        public string model { get; set; }
        public int year { get; set; }

        public Car(string make, string model, int year)
        {
            this.make = make;
            this.model = model;
            this.year = year;
        }

        public override string ToString()
        {
            return $"{this.year} {this.make} {this.model}";
        }
    }
}