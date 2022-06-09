using System;
using eTicketing.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

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

		public static ShoppingCart GetShoppingCart(IServiceProvider services)
        {
			ISession session = services.GetRequiredService<IHttpContextAccessor>()?.HttpContext.Session;
			var context = services.GetService<AppDbContext>();

			string cartId = session.GetString("CartId") ?? Guid.NewGuid().ToString();
			session.SetString("CartId", cartId);

			return new ShoppingCart(context) { ShoppingCartid = cartId};
        }

		public void AddItemToCart(Movie movie)
        {
			var shoppingCartItem = _context.ShoppingCartItems.FirstOrDefault(n => n.Movie.Id == movie.Id && n.ShoppingCartId == ShoppingCartid);

			if(shoppingCartItem==null)
            {
				shoppingCartItem = new ShoppingCartItem()
				{
					ShoppingCartId = ShoppingCartid,
					Movie = movie,
					Amount = 1,
				};
				_context.ShoppingCartItems.Add(shoppingCartItem);
            }
			else
            {
				shoppingCartItem.Amount++;
            }
			_context.SaveChanges();
        }

		public void RemoveItemfromCart(Movie movie)
        {
			var shoppingCartItem = _context.ShoppingCartItems.FirstOrDefault(n => n.Movie.Id == movie.Id && n.ShoppingCartId == ShoppingCartid);

			if (shoppingCartItem != null)
			{
				if (shoppingCartItem.Amount > 1)
				{
					shoppingCartItem.Amount--;
				}
				else
				{
					_context.ShoppingCartItems.Remove(shoppingCartItem);
				}
			}
			_context.SaveChanges();
		}


		public List<ShoppingCartItem> GetShoppingCartItems()
        {
			return ShoppingCartItems ?? (ShoppingCartItems = _context.ShoppingCartItems.Where(n => n.ShoppingCartId == ShoppingCartid).Include(n => n.Movie).ToList());
        }

		public double GetShoppingCartTotal() => _context.ShoppingCartItems.Where(n => n.ShoppingCartId == ShoppingCartid).Select(m => m.Movie.Price * m.Amount).Sum();
	}
}

