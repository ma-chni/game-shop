using System;
using System.Collections.Generic;

namespace GameShop.Domain.Models
{
    class Order
    {
  

        public Order()
        {
            Date = DateTime.Now;
        }
        public int Id { get; protected set; }
        public Customer Customer  { get; protected set; }
        public DateTime Date { get; protected set; }
        public int CustomerId { get; protected set; }
        public List<ArticleOrder> Articles { get; protected set; } = new List<ArticleOrder>();
    }
}
