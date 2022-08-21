using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Application.UserLogic.Balance.Commands.BalanceUp;
using Application.UserLogic.Balance.Commands.BalanceDown;
using Application.UserLogic.Balance.Queries;
using Microsoft.AspNetCore.Authorization;
using MediatR;




namespace Shop.Controllers.UserController.Balance
{
    [Route("api/[controller]")]
    [ApiController]
    public class BalanceController : ControllerBase
    {
        private readonly IMediator _mediator;
        public BalanceController(IMediator mediator)
        {
            _mediator = mediator;
        }


        [HttpGet, Authorize(Roles = "User")]
        
        public async Task<string> Balance()
        {
            return await _mediator.Send(new BalanceQuery(Request.Cookies["UserToken"]));
        }

        [HttpPost,Authorize(Roles="User")]
        [Route("putMoney")]
        public async Task<string> BalanceUp([FromBody]int money)
        {
            var result = await _mediator.Send(new BalanceUpCommand(money, Request.Cookies["UserToken"]));
            return result;
            
           
        }

        [HttpPost, Authorize(Roles = "User")]
        [Route("getMoney")]
        public async Task<string> BalanceDown([FromBody]int money)
        {
            var result = await _mediator.Send(new BalanceDownCommand(money, Request.Cookies["UserToken"]));
            return result;


        }
        
    }
}
