using System;
using eTicketing.Models;

namespace eTicketing.Data.Services
{
	public interface IOrdersService
	{
		Task StoreOrderASync(List<ShoppingCartItem> items, string userId, string userEmailAddress);

		Task<List<Order>> GetOrdersByUserIdASync(string userId);
	}
}

