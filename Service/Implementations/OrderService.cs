using Domain.DTO;
using Repo.Implementation;
using Repo.Interfaces;
using Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Implementations
{
	public class OrderService : IOrderService
	{
		private readonly IOrderDa _orderDa;
        public OrderService(IOrderDa orderDa)
        {
			_orderDa = orderDa;
        }
        public async Task<int> PayOrderInShoppingCart(string userId)
		{
			return await _orderDa.PayOrderInShoppingCart(userId);
		}
		public async Task<List<ListPaidOrdersDto>> GetAllPaidOrders(string userId)
		{
			return await _orderDa.GetAllPaidOrders(userId);
		}
        public async Task<string> GetUserEmailByUserId(string userId)
        {
            return await _orderDa.GetUserEmailByUserId(userId);
        }
    }
}
