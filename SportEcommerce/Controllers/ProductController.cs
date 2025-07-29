using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SportEcommerce.Data;
using SportEcommerce.Models.Product;
using SportEcommerce.Models.User;
using System.Security.Claims;

namespace SportEcommerce.Controllers
{
    [Authorize]
    public class ProductController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<ProductController> _logger;
        private readonly UserManager<ApplicationUser> _userManager;

        public ProductController(
            ApplicationDbContext context, 
            ILogger<ProductController> logger,
            UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _logger = logger;
            _userManager = userManager;
        }

        public IActionResult Index()
        {
            IQueryable products = GetSellerProducts();
            return View(products);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [Authorize(Roles = "Seller")]
        public async Task<IActionResult> Create(ProductType productType)
        {
            // If data is valid
            if (ModelState.IsValid)
            {
                // Insert product type and associate with user
                await _context.ProductTypes.AddAsync(productType);

                // Log registration
                _logger.LogInformation("Product type added");

                // Return to Product views index
                return RedirectToAction("Index");

            }
            return View(productType);
        }

        // Helper functions

        // Get seller user associated products
        private IQueryable GetSellerProducts()
        {
            // Retrieve products based on associated user id
            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier)!;
            var products = _context.ProductTypes.Where(p => p.SellerId.Equals(userId));
            return products;
        }
    }
}
