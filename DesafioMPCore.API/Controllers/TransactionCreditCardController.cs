using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using DesafioMPCore.API.Models;
using AutoMapper;
using DesafioMPCore.Domain.Interface.Application;
using DesafioMPCore.Domain.CheckOut;

namespace DesafioMPCore.API.Controllers
{
    [Produces("application/json")]
    [Route("api")]
    public class TransactionCardController : Controller
    {
        readonly IFinancialTransactionApp _financialTransactionApp;

        public TransactionCardController(IFinancialTransactionApp financialTransactionApp)
        {
            _financialTransactionApp = financialTransactionApp;
        }

        // POST: api/CreditCardTransaction
        [HttpPost("CreditCardTransaction", Name = "CreditCardTransaction")]
        public async Task<IActionResult> Post([FromBody]Models.CreditCardTransaction transactionCardModel)
        {
            try
            {
                if (transactionCardModel == null || !transactionCardModel.IsValid())
                    return BadRequest();

                var creditCardSale = transactionCardModel.ToDomain();
                if (creditCardSale == null)
                    return BadRequest();

                var creditCardSaleResponse = await _financialTransactionApp.DoSaleWithCreditCardTransaction(creditCardSale);

                bool successSale = creditCardSaleResponse != null ? creditCardSaleResponse.CreditCardTransactionResultCollection.All(c => c.Success) : false;

                if (successSale)
                    return Ok(creditCardSaleResponse);
                else
                    return StatusCode(403, Error.CreateInternalError("Não foi possível realizar a venda.", (System.Net.HttpStatusCode)403));
            }
            catch
            {
                return StatusCode(500, Error.CreateInternalError());
            }
        }

    }
}
