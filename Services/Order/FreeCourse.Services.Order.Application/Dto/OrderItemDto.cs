using System;
using System.Collections.Generic;
using System.Text;

namespace FreeCourse.Services.Order.Application.Dto
{
    public class OrderItemDto
    {
        public string ProductId { get; set; }
        public string ProductName { get; set; }
        public string PictureUrl { get; set; }
        public Decimal Price { get; set; }
    }
}
