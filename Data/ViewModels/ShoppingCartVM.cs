using System;
using eTicketing.Data.Cart;

namespace eTicketing.Data.ViewModels
{
	public class ShoppingCartVM
	{
        public ShoppingCart shoppingCart { get; set; }

        public double shoppingCartTotal { get; set; }
    }
}

