public interface IDiscountStrategy
{
    decimal CalculateDiscount(decimal roomPrice);
}
public class NoDiscountStrategy : IDiscountStrategy
{
    public decimal CalculateDiscount(decimal roomPrice)
    {
        return 0;
    }
}
public class PercentageDiscountStrategy : IDiscountStrategy
{
    private readonly decimal _discountPercentage;
    public PercentageDiscountStrategy(decimal discountPercentage)
    {
        _discountPercentage = discountPercentage;
    }
    public decimal CalculateDiscount(decimal roomPrice)
    {
        return roomPrice * _discountPercentage / 100;
    }
}
public class Hotel
{
    private List<decimal> _roomPrices;
    private IDiscountStrategy _discountStrategy;
    public Hotel()
    {
        _roomPrices = new List<decimal>();
        _discountStrategy = new NoDiscountStrategy();
    }
    public void AddRoom(decimal roomPrice)
    {
        _roomPrices.Add(roomPrice);
    }
    public decimal CalculateAveragePrice()
    {
        decimal total = _roomPrices.Sum();
        int count = _roomPrices.Count;
        return total / count;
    }
    public void SetDiscountStrategy(IDiscountStrategy discountStrategy)
    {
        _discountStrategy = discountStrategy;
    }
    public decimal CalculateDiscount(decimal roomPrice)
    {
        return _discountStrategy.CalculateDiscount(roomPrice);
    }
}
public class Program
{
    public static void Main(string[] args)
    {
        Hotel hotel = new Hotel();
        hotel.AddRoom(100);
        hotel.AddRoom(150);
        hotel.AddRoom(200);
        decimal averagePrice = hotel.CalculateAveragePrice();
        Console.WriteLine($"Average room price: {averagePrice}");
        IDiscountStrategy discountStrategy = new PercentageDiscountStrategy(10);
        hotel.SetDiscountStrategy(discountStrategy);
        decimal discount = hotel.CalculateDiscount(150);
        Console.WriteLine($"Discount for room price 150: {discount}");
    }
}