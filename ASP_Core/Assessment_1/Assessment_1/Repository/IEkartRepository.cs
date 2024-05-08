using Assessment_1.Models;

namespace Assessment_1.Repository
{
    public interface IEkartRepository
    {
        Order PlaceOrder(Order order);
        Order GetOrderDetails(int orderId);
        decimal Calculate_Bill(int orderId);
        List<Customer> GetCustomersByOrderDate(DateTime orderDate);
        Customer GetCustomerWithHighestOrder();
    }
}
