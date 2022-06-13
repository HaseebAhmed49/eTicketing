using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using eTicketing.Data.Cart;
using eTicketing.Data.Services;
using eTicketing.Data.ViewModels;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace eTicketing.Controllers
{
    public class OrdersController : Controller
    {
        private readonly IMovieService _movieService;
        private readonly ShoppingCart _shoppingCart;
        private readonly IOrdersService _ordersService;

        public OrdersController(IMovieService movieService,ShoppingCart shoppingCart, IOrdersService ordersService)
        {
            _movieService = movieService;
            _shoppingCart = shoppingCart;
            _ordersService = ordersService;
        }

        public async Task<IActionResult> Index()
        {
            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            string userRole = User.FindFirstValue(ClaimTypes.Role);
            var orders = await _ordersService.GetOrdersByUserIdAndRoleASync(userId,userRole);

            return View(orders);
        }

        // GET: /<controller>/
        public IActionResult ShoppingCart()
        {
            var items = _shoppingCart.GetShoppingCartItems();
            _shoppingCart.ShoppingCartItems = items;
            var response = new ShoppingCartVM()
            {
                shoppingCart = _shoppingCart,
                shoppingCartTotal = _shoppingCart.GetShoppingCartTotal()
            };
            return View(response);

        }

        public async Task<IActionResult> AddItemToShoppingCart(int id)
        {
            var movie = await _movieService.GetMovieByIdASync(id);
            if(movie!=null)
            {
                _shoppingCart.AddItemToCart(movie);
            }
            return RedirectToAction(nameof(ShoppingCart));
        }

        public async Task<IActionResult> RemoveItemFromShoppingCart(int id)
        {
            var movie = await _movieService.GetMovieByIdASync(id);
            if (movie != null)
            {
                _shoppingCart.RemoveItemfromCart(movie);
            }
            return RedirectToAction(nameof(ShoppingCart));
        }

        public async Task<IActionResult> CompleteOrder()
        {
            var items = _shoppingCart.GetShoppingCartItems();
            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            string userEmailAddress = User.FindFirstValue(ClaimTypes.Email);

            await _ordersService.StoreOrderASync(items,userId,userEmailAddress);

            await _shoppingCart.ClearShoppingCartASync();

            return View("OrderCompleted");
        }
    }
}

