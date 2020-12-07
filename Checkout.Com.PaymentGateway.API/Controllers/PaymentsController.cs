using AutoMapper;
using Checkout.Com.PaymentGateway.Services.PaymentProcessor.Contracts;
using Checkout.Com.PaymentGateway.Services.PaymentProcessor.CustomExceptions;
using Checkout.Com.PaymentGateway.Services.Persistance.Contracts;
using Checkout.Com.PaymentGateway.Services.Persistance.PersistanceExceptions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DM = Checkout.Com.PaymentGateway.Models;
using SM = Checkout.Com.PaymentGateway.API.ServiceModels;

namespace Checkout.Com.PaymentGateway.API.Controllers
{
    [ApiController]
    public class PaymentsController : ControllerBase
    {
        //TODO: Add yml for deployment
        //TODO: add client dll code

        readonly ILogger<PaymentsController> _logger;
        readonly IEnumerable<IProcessor> _processors;
        readonly IMapper _mapper;
        readonly ICommandService _commandService;
        readonly IQueryService _queryService;

        public PaymentsController(ILogger<PaymentsController> logger,
            IEnumerable<IProcessor> processors,
            IMapper mapper,
            IQueryService queryService,
            ICommandService commandService)
        {
            _logger = logger;
            _processors = processors;
            _mapper = mapper;
            _commandService = commandService;
            _queryService = queryService;
        }

        /// <summary>
        /// To capture a new payment
        /// </summary>
        /// <param name="payment"></param>
        /// <returns>a unique id that can be used to get the payment details</returns>
        [HttpPost]
        [Route("api/payments")]
        [ProducesResponseType(201)]
        public async Task<ActionResult> CollectPayment(SM.Payment payment)
        {
            try
            {
                var paymentRequest = _mapper.Map<SM.Payment, DM.Payment>(payment);
                var response = await _processors.
                                          First(p => p.IsAppropriateProcessor(payment.Card.Number)).
                                          ProcessAsync(paymentRequest);
                await _commandService.SaveAsync(response);
                if (response.Status == DM.Status.Success)
                    return Created($"api/payments/{response.PublicId}", _mapper.Map<SM.PaymentAcknowledgement>(response));
                else
                {
                    return new StatusCodeResult(500);
                }
            }
            catch (PaymentNotProcessedException ex)
            {
                return new StatusCodeResult(500);
            }
        }

        /// <summary>
        /// The get the payment details using its unique id
        /// </summary>
        /// <param name="publicId"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("api/payments/{publicId}")]
        public async Task<ActionResult> GetPayment(Guid publicId)
        {
            try
            {
                var payment = await _queryService.GetAsync(publicId);
                return Ok(_mapper.Map<SM.Payment>(payment));
            }
            catch (PaymentNotFoundException ex)
            {
                return NotFound();
            }
            catch (Exception ex)
            {
                return new StatusCodeResult(500);
            }
        }
    }
}
