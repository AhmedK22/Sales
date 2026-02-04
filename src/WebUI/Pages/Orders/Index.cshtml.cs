using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;
using System.Net.Http.Json;
using WebUI.Models;

namespace WebUI.Pages.Orders
{
    public class IndexModel : PageModel
    {
        public List<OrderDto> Orders { get; set; } = new();
        public async Task OnGetAsync()
        {
            var client = new HttpClient();
            var list = await client.GetFromJsonAsync<List<OrderDto>>("http://localhost:5045/api/orders");
            if (list != null) Orders = list;
        }
    }
}
