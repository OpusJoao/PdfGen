using Singulare.Services.QuoteService;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Singulare.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class QuoteController : ControllerBase
    {

        private readonly IQuoteService _quotesService;

        public QuoteController(IQuoteService quoteService)
        {
            _quotesService = quoteService;
        }

        [HttpGet]
        public async Task<ActionResult<List<Quote>>> GetQuotes()
        {
            var quotes = await _quotesService.GetQuotes();
            return Ok(quotes);
        }

        [HttpGet("{QuoteId}")]
        public async Task<ActionResult<Quote>> GetQuoteById(int QuoteId)
        {
            var quote = await _quotesService.GetQuoteByid(QuoteId);
            if (quote == null)
                return NotFound("Quote not found.");
            return Ok(quote);
        }

        [HttpPost]
        public async Task<ActionResult<List<Quote>>> createQuote(Quote quote)
        {
            var quotes = await _quotesService.createQuote(quote);
            return Ok(quotes);
        }
    }
}
