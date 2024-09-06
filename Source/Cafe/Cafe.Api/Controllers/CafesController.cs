using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Mvc;

namespace Cafe.Api.Controllers
{
    public class CafesController :BaseController
    {
        [HttpGet]
        public async Task<ActionResult<object>> GetCafes(string? location)
        {
            //var result = await mediator.Send(new GetProductCommand
            //{
            //    ProductName = productName
            //});
            return Ok();
        }

        [HttpPost]
        public async Task<ActionResult<object>> CreateCafe()
        {
            //var result = await mediator.Send(new GetProductCommand
            //{
            //    ProductName = productName
            //});
            return Ok();
        }
        [HttpPut]
        public async Task<ActionResult<object>> UpdateCafe()
        {
            //var result = await mediator.Send(new GetProductCommand
            //{
            //    ProductName = productName
            //});
            return Ok();
        }

        [HttpDelete]
        public async Task<ActionResult<object>> RemoveCafe()
        {
            //var result = await mediator.Send(new GetProductCommand
            //{
            //    ProductName = productName
            //});
            return Ok();
        }
    }
}
