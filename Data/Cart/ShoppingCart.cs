using System;
using eTicketing.Models;
using Microsoft.EntityFrameworkCore;

namespace eTicketing.Data.Cart
{
	public class ShoppingCart
	{
        public AppDbContext _context { get; set; }

        public string ShoppingCartid { get; set; }

        public List<ShoppingCartItem> ShoppingCartItems { get; set; }

        public ShoppingCart(AppDbContext context)
		{
			_context = context;
		}

		public List<ShoppingCartItem> GetShoppingCartItems()
        {
			return ShoppingCartItems ?? (ShoppingCartItems = _context.ShoppingCartItems.Where(n => n.ShoppingCartId == ShoppingCartid).Include(n => n.Movie).ToList());
        }

		public double ShoppingCartTotal() => _context.ShoppingCartItems.Where(n => n.ShoppingCartId == ShoppingCartid).Select(m => m.Movie.Price * m.Amount).Sum();
	}
}

