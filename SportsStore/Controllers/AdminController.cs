using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SportsStore.Models;
using SportsStore.Models.Repository;

namespace SportsStore.Controllers
{
    [Authorize]
    [Route("Admin")]
    public class AdminController : Controller
    {
        private readonly IStoreRepository storeRepository;
        private readonly IOrderRepository orderRepository;

        public AdminController(IStoreRepository storeRepository, IOrderRepository orderRepository)
        {
            this.storeRepository = storeRepository;
            this.orderRepository = orderRepository;
        }

        [Route("Orders")]
        public ViewResult Orders()
        {
            return this.View(this.orderRepository.Orders);
        }

        [Route("Products")]
        public ViewResult Products()
        {
            return this.View(this.storeRepository.Products);
        }

        [HttpPost]
        [Route("markShipped")]
        public IActionResult MarkShipped(int orderId)
        {
            Order? order = this.orderRepository.Orders.FirstOrDefault(o => o.OrderId == orderId);

            if (order != null)
            {
                order.Shipped = true;
                this.orderRepository.SaveOrder(order);
            }

            return this.RedirectToAction("Orders");
        }

        [HttpPost]
        [Route("Reset")]
        public IActionResult Reset(int orderId)
        {
            Order? order = this.orderRepository.Orders.FirstOrDefault(o => o.OrderId == orderId);

            if (order != null)
            {
                order.Shipped = false;
                this.orderRepository.SaveOrder(order);
            }

            return this.RedirectToAction("Orders");
        }

        [Route("Details/{productId:int}")]
        public ViewResult Details(int productId)
        {
            return this.View(this.storeRepository.Products.FirstOrDefault(p => p.ProductId == productId));
        }

        [Route("Products/Edit/{productId:long}")]
        public ViewResult Edit(int productId)
        {
            return this.View(this.storeRepository.Products.FirstOrDefault(p => p.ProductId == productId));
        }

        [HttpPost]
        [Route("Products/Edit/{productId:long}")]
        public IActionResult Edit(Product product)
        {
            if (this.ModelState.IsValid)
            {
                this.storeRepository.SaveProduct(product);
                return this.RedirectToAction("Products");
            }

            return this.View(product);
        }

        [Route("Products/Create")]
        public ViewResult Create()
        {
            return this.View(new Product());
        }

#pragma warning disable S4144

        [HttpPost]
        [Route("Products/Create")]
        public IActionResult Create(Product product)
        {
            if (this.ModelState.IsValid)
            {
                this.storeRepository.SaveProduct(product);
                return this.RedirectToAction("Products");
            }

            return this.View(product);
        }
#pragma warning restore S4144

        [Route("Products/Delete/{productId:long}")]
        public IActionResult Delete(int productId)
        {
            return this.View(this.storeRepository.Products.FirstOrDefault(p => p.ProductId == productId));
        }

        [HttpPost]
        [Route("Products/Delete/{productId:long}")]
        public IActionResult DeleteProduct(int productId)
        {
            Product product = this.storeRepository.Products.FirstOrDefault(p => p.ProductId == productId) !;
            this.storeRepository.DeleteProduct(product);
            return this.RedirectToAction("Products");
        }
    }
}
