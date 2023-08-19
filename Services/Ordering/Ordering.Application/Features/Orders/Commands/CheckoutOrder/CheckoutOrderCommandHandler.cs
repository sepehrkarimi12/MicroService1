using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Ordering.Application.Contracts.Infrastructure;
using Ordering.Application.Contracts.Persistence;
using Ordering.Application.Models;
using Ordering.Domain.Entities;

namespace Ordering.Application.Features.Orders.Commands.CheckoutOrder
{
    internal class CheckoutOrderCommandHandler : IRequestHandler<CheckoutOrderCommand, int>
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IMapper _mapper;
        private readonly IEmailService _emailService;
        private readonly ILogger<CheckoutOrderCommandHandler> _logger;

        public CheckoutOrderCommandHandler(IOrderRepository orderRepository, IMapper mapper, IEmailService emailService, ILogger<CheckoutOrderCommandHandler> logger)
        {
            _orderRepository = orderRepository;
            _mapper = mapper;
            _emailService = emailService;
            _logger = logger;
        }

        public async Task<int> Handle(CheckoutOrderCommand request, CancellationToken cancellationToken)
        {
            var orderEntity = _mapper.Map<Order>(request);
            var newOrder = await _orderRepository.AddAsync(orderEntity);
            _logger.LogInformation($"order {newOrder.Id} is successfully created.");
            await this.SendMail(newOrder);
            return newOrder.Id;
        }

        private async Task SendMail(Order order)
        {
            try
            {
                //send email
                await _emailService.SendEmail(new Email
                {
                    To = "test@test.com",
                    Subject = "subject",
                    Body = "email body"
                });
            }
            catch (Exception e)
            {
                _logger.LogError($"There is an error on sending email {e.Message}");
            }
        }
    }
}
