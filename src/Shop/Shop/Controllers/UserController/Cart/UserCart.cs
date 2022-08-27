using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MediatR;
using Application.UserLogic.Cart.Commands.Add;
using Application.UserLogic.Cart.Commands.Remove;
using Domain.Entities.Product;
using Domain.Entities.User;
using Application.Common.Help;
using System.Text.Json;
using Application.UserLogic.Cart.Queries.MyCart;

using System.Reflection;



namespace Shop.Controllers.UserController.Cart
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserCart : ControllerBase
    {
        private readonly IMediator _mediator;
        public UserCart(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost,Authorize(Roles ="User")]
        [Produces("application/json")]
        public async Task<ActionResult> userCart()
        {
            var (response,status) = await _mediator.Send(new MyCartQuery(Request.Cookies["UserToken"]));
       

            return StatusCode(status,response);
        }

        [HttpPost,Authorize(Roles = "User")]
        [Route("Add")]
        [Produces("application/json")]
        public async Task<ActionResult> Add(AddCommand command)
        {
            var (response,status) = await _mediator.Send(command);

            return StatusCode(status,response);
        }

        [HttpPost, Authorize(Roles = "User")]
        [Route("Remove")]
        public async Task<ActionResult> Remove(RemoveCommand command)
        {
            var (response,status) = await _mediator.Send(command);
            return StatusCode(status, response);
        }
    }
}
