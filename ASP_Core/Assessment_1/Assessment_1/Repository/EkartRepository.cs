using Assessment_1.Models;
using Assessment_1.Repository;
using Microsoft.EntityFrameworkCore;

namespace Assessment_1.Repository
{
    public class EkartRepository : IEkartRepository
    {
        private readonly NorthwindContext _dbContext;

        public EkartRepository(NorthwindContext dbContext)
        {
            _dbContext = dbContext;
        }

        public Order PlaceOrder(Order order)
        {
            _dbContext.Orders.Add(order);
            _dbContext.SaveChanges();
            return order;
        }

        public List<Order> GetOrderDetails()
        {
            //return _dbContext.Orders.FirstOrDefault(x => x.OrderId == orderId);
            return _dbContext.Orders.ToList();
        }

        //public decimal CalculateBill(int orderId)
        //{
        //    var orderDetails = _dbContext.OrderDetails.Where(od => od.OrderId == orderId).ToList();
        //    decimal totalBill = orderDetails.Sum(od => od.UnitPrice * od.Quantity);
        //    return totalBill;
        //}

        public List<Customer> GetCustomersByOrderDate(DateTime orderDate)
        {
            return _dbContext.Customers.Where(c => c.Orders.Any(o => o.OrderDate == orderDate)).ToList();
        }

        public Customer GetCustomerWithHighestOrder()
        {
            return _dbContext.Orders
                 .GroupBy(o => o.CustomerId)
                 .OrderByDescending(g => g.Count())
                 .Select(g => g.First().Customer)
                 .FirstOrDefault();
        }

        public decimal Calculate_Bill(int orderId)
        {
            var orderDetails = _dbContext.OrderDetails.Where(od => od.OrderId == orderId).ToList();
            decimal totalBill = orderDetails.Sum(od => od.UnitPrice * od.Quantity);
            return totalBill;
        }

        //public decimal Calculate_Bill(int orderId)
        //{
        //    throw new NotImplementedException();
        //}
    }
}
