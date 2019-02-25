namespace LinqAdvTester
{
    public class Car
    {
        public string Plate { get;  set; }
        public int Price { get;  set; }
        public string Description { get;  set; }

        public Car(string plate, int price, string description)
        {
            Plate = plate;
            Price = price;
            Description = description;
        }
    }
}