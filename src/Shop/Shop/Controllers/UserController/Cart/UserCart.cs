using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MediatR;
using Application.UserLogic.Cart.Commands.Add;
using Application.UserLogic.Cart.Commands.Remove;
using Domain.Entities.Product;
using Domain.Entities.User;
using Application.Common.Json;
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
        public async Task<string> userCart()
        {            
            return await _mediator.Send(new MyCartQuery(Request.Cookies["UserToken"]));
        }

        [HttpPost,Authorize(Roles = "User")]
        [Route("Add")]
        public async Task<string> Add(AddCommand command)
        {
            return await _mediator.Send(command);       
        }

        [HttpPost, Authorize(Roles = "User")]
        [Route("Remove")]
        public async Task<string> Remove(RemoveCommand command)
        {
            return await _mediator.Send(command);           
        }
    }
}
