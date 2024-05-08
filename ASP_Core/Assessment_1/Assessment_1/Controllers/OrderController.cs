using Assessment_1.Models;
using Assessment_1.Repository;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

public class OrderController : Controller
{
    private readonly IEkartRepository _repository;

    public OrderController(IEkartRepository repository)
    {
        _repository = repository;
    }

    // GET
    public IActionResult Place()
    {
        // Displaying a form to place an order
        return View();
    }

    // POST
    [HttpPost]
    public IActionResult Place(Order order)
    {
        if (ModelState.IsValid)
        {
            var placedOrder = _repository.PlaceOrder(order);
            return RedirectToAction("Details", new { orderId = placedOrder.OrderId });
        }
        return View(order);
    }

    // GET
    public IActionResult Details(int orderId)
    {
        var order = _repository.GetOrderDetails(orderId);
        if (order == null)
        {
            return NotFound();
        }
        return View(order);
    }

    // GET
    public IActionResult Bill(int orderId)
    {
        var bill = _repository.Calculate_Bill(orderId);
        if (bill == 0)
        {
            return NotFound();
        }
        ViewBag.OrderId = orderId;
        ViewBag.BillAmount = bill;
        return View();
    }

    // GET
    public IActionResult CustomerDetailsByDate(DateTime orderDate)
    {
        var customers = _repository.GetCustomersByOrderDate(orderDate);
        if (customers.Count == 0)
        {
            return NotFound();
        }
        return View(customers);
    }

    // GET
    public IActionResult HighestOrderCustomer()
    {
        var customer = _repository.GetCustomerWithHighestOrder();
        if (customer == null)
        {
            return NotFound();
        }
        return View(customer);
    }
}
