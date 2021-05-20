using System.Collections.Generic;

namespace GameShop.Domain.Models
{
    class Article
    {
        public Article(string articleNumber)
        {
            ArticleNumber = articleNumber;
        }

        public Article(string articleNumber, string name, string description, int price)
        {
            ArticleNumber = articleNumber;
            Name = name;
            Description = description;
            Price = price;
        }

        public int Id { get; protected set; }
        public string ArticleNumber { get; protected set; }
        public string Name { get; protected set; }
        public string Description { get; protected set; }
        public int Price { get; protected set; }
        public List<ArticleOrder> Orders { get; protected set; } = new List<ArticleOrder>();
    }
}
