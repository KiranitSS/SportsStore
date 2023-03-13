﻿using Microsoft.AspNetCore.Mvc;
using SportsStore.Models;
using SportsStore.Models.Repository;

namespace SportsStore.Controllers
{
    public class OrderController : Controller
    {
        private readonly IOrderRepository orderRepository;

        private readonly Cart cart;

        public OrderController(IOrderRepository repository, Cart cart)
        {
            this.orderRepository = repository;
            this.cart = cart;
        }

        public ViewResult Checkout()
        {
            return this.View(new Order());
        }

        [HttpPost]
        public IActionResult Checkout(Order order)
        {
            if (!this.cart.Lines.Any())
            {
                this.ModelState.AddModelError(string.Empty, "Sorry, your cart is empty!");
            }

            if (this.ModelState.IsValid)
            {  
                order.Lines = this.cart.Lines.ToArray();
                this.orderRepository.SaveOrder(order);
                this.cart.Clear();
                return this.View("Completed", order.OrderId);
            }

            return this.View();
        }
    }
}
