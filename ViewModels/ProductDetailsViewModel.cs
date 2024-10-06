using WebApplication280924_Product.Models;

namespace WebApplication280924_Product.ViewModels
{
    public class ProductDetailsViewModel
    {
        public Product Product { get; set; }
        public double AverageRating { get; set; }
        public List<Review> Reviews { get; set; }
    }
}
