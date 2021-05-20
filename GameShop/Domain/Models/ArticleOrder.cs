
namespace GameShop.Domain.Models
{
    class ArticleOrder
    {
        public Order Order { get; protected set; }
        public int OrderId { get; protected set; }
        public Article Article { get; protected set; }
        public int ArticleId { get; protected set; }

        public ArticleOrder(int orderId, int articleId)
        {
            ArticleId = articleId;
            OrderId = orderId;
        }

        public ArticleOrder(Article article)
        {
            Article = article;
        }
    }

}
