using Domain.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repo.Interfaces
{
	public interface IOrderDa
	{
		Task<int> PayOrderInShoppingCart(string userId);
		Task<List<ListPaidOrdersDto>> GetAllPaidOrders(string userId);
        Task<string> GetUserEmailByUserId(string userId);
    }
}
