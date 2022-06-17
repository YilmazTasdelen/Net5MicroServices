using FreeCourse.Services.Order.Domain.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FreeCourse.Services.Order.Domain.OrderAggregate
{
    //EF Core Features (for ex: if we change the orm in the future like hibernate checkout these features ) 
    // Owned Types
    // Shadow Property
    // Backing Field

    public class Order:Entity,IAggregateRoot
    {
        public DateTime CreatedDate { get; private set; }

        public Address Address { get; private set; } //owned entity type it can be another table by ef core or can be group of column at order table depends on the ussage

        public string BuyerId { get; private set; }

        private readonly List<OrderItem> _orderItems;


        public IReadOnlyCollection<OrderItem> OrderItems => _orderItems;

        public Order( Address address, string buyerId)
        {
            CreatedDate = DateTime.Now;
            Address = address;
            BuyerId = buyerId;
            _orderItems = new List<OrderItem>();
        }

        public Order(string productId, string productName, decimal price, string pictureUrl)
        {
            var existProduct = _orderItems.Any(x=>x.ProductId==productId);
            if(!existProduct)
            {
                var newOrderItem = new OrderItem(productId,productName,pictureUrl,price);
                _orderItems.Add(newOrderItem);
            }


        }


        public decimal GetTotalPrice => _orderItems.Sum(x=>x.Price);
    }
}
