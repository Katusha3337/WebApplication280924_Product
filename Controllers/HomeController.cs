using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using WebApplication280924_Product.Models;
using WebApplication280924_Product.ViewModels;

namespace WebApplication280924_Product.Controllers
{
    public class HomeController : Controller
    {
        private static List<Product> products = new List<Product>
    {
        new Product { Id = 1, Name = "Product 1", Description = "Description 1" },
        new Product { Id = 2, Name = "Product 2", Description = "Description 2" }
    };

        private static List<Review> reviews = new List<Review>();

        [HttpGet]
        public IActionResult Index()
        {
            return View(products);
        }

        [HttpGet]
        public IActionResult ProductDetails(int id)
        {
            var product = products.FirstOrDefault(p => p.Id == id);
            var productReviews = reviews.Where(r => r.ProductId == id).ToList();
            var averageRating = productReviews.Any() ? productReviews.Average(r => r.Rating) : 0;

            Console.WriteLine($"���������� �������: {productReviews.Count}, ������� ������: {averageRating}");

            var viewModel = new ProductDetailsViewModel
            {
                Product = product,
                AverageRating = averageRating,
                Reviews = productReviews
            };

            return View(viewModel);
        }

        [HttpPost]
        public IActionResult SubmitReview(Review review)
        {
            Console.WriteLine("����� SubmitReview ������");
            Console.WriteLine($"ProductId: {review.ProductId}, UserName: {review.UserName}, Rating: {review.Rating}, Comment: {review.Comment}");

            // ������� �������� �� ������������� �����
            if (string.IsNullOrWhiteSpace(review.UserName) || review.Rating < 1 || review.Rating > 5 || string.IsNullOrWhiteSpace(review.Comment))
            {
                Console.WriteLine("������ ���������������: ���� ��� ��������� ����� �� ��������� ��� ����� �������� ��������");
                return View(review);
            }

            review.Id = reviews.Count + 1;
            review.Date = DateTime.Now;
            reviews.Add(review);
            Console.WriteLine($"����� ��������: {review.Comment}, ������: {review.Rating}");
            return RedirectToAction("ProductDetails", new { id = review.ProductId });
        }
    }
}
