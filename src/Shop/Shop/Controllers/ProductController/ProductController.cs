using Application.Common.Help;
using Microsoft.AspNetCore.Mvc;
using Domain.Entities.Product;
using MediatR;
using Application.ProductLogic.Queries;
using Application.ProductLogic.Commands.CreateProduct;
using Application.ProductLogic.Commands.UpdateProduct;
using Application.ProductLogic.Commands.DeleteProduct;
using Application.ProductLogic.Commands.BuyProduct;


using Microsoft.AspNetCore.Authorization;
namespace Shop.Controllers.ProductsController
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IMediator _mediator;
        public ProductController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet,Authorize(Roles = "User")]
        public async Task<List<ProductModel>> Products()
        {
            
            return await _mediator.Send(new GetProductQuery());
        }

        [HttpPost, Authorize(Roles = "User")]
        [Route("Create")]
        public async Task<ProductModel> CreateProduct(CreateProductCommand command)
        {

            return await _mediator.Send(command);
        }
        [HttpPut, Authorize(Roles = "Admin")]
        [Route("Update")]
        public async Task<ProductModel> UpdateProduct(UpdateProductCommand command)
        {
            return await _mediator.Send(command);
            
        }
        [HttpDelete, Authorize(Roles = "Admin")]
        [Route("Delete")]
        public async Task<ProductModel> DeleteProduct([FromBody]int id)
        {
            return await _mediator.Send(new DeleteProductCommand(id));
        }

        [HttpPost,Authorize(Roles = "User")]
        [Route("buy")]
        public async Task<Response<string>> BUYProduct(int id)
        {
            
            return await _mediator.Send(new BuyProductCommand(id, Request.Cookies["UserToken"]));
        }


    }
}
