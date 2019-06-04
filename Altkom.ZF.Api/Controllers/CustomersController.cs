using Altkom.ZF.IServices;
using Microsoft.AspNetCore.Mvc;

namespace Altkom.ZF.Api.Controllers
{

    [Route("api/[controller]")]
    public class CustomersController : ControllerBase
    {
        private readonly ICustomersService customersService;

        public CustomersController(ICustomersService customersService)
        {
            this.customersService = customersService;
        }

        public IActionResult Get()
        {
            var customers = customersService.Get();

            return Ok(customers);
        }
    }

}