using AutoMapper;
using ECommerceBackendSystem.API.Models.Orders.Request;
using ECommerceBackendSystem.Application.Abstractions.Dtos.Orders;
using ECommerceBackendSystem.Application.Abstractions.Dtos.ServiceResponse;
using ECommerceBackendSystem.Application.Abstractions.Services.OrderService;
using ECommerceBackendSystem.Application.Abstractions.Services.OrderService.Factory;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ECommerceBackendSystem.API.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    [Produces("application/json")]
    public class OrdersController(IOrderStrategyFactory factory, IOrderQueryService orderQueryService, IMapper mapper) : ControllerBase
    {
        private readonly IOrderStrategyFactory _factory = factory;
        private readonly IOrderQueryService _orderQueryService = orderQueryService;
        private readonly IMapper _mapper = mapper;


        /// <summary>
        /// Kullanýcýya ait sipariþleri getirir.
        /// </summary>
        /// <param name="request">Kullanýcý ID'si</param>
        [HttpGet("{userId}")]
        [ProducesResponseType(typeof(ServiceResponse<IEnumerable<OrderDto>>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ServiceResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ServiceResponse), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetOrders([FromRoute] GetOrdersRequest request)
        {
            var response = await this._orderQueryService.GetOrdersByUserIdAsync(Guid.Parse(request.UserId));

            if (!response.IsSuccessful)
                return BadRequest(response);

            return Ok(response);
        }

        /// <summary>
        /// Yeni sipariþ oluþturur.
        /// </summary>
        /// <param name="request"></param>
        [HttpPost("create")]
        [ProducesResponseType(typeof(ServiceResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ServiceResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ServiceResponse), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> CreateOrder([FromBody] CreateOrderRequest request)
        {
            var dto = this._mapper.Map<CreateOrderDto>(request);

            var strategy = _factory.GetStrategy(request.PaymentMethod);
            var response = await strategy.CreateOrderAsync(dto).ConfigureAwait(false);

            if (!response.IsSuccessful)
                return BadRequest(response);

            return Ok(response);
        }
    }
}
