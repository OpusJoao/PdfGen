using Singulare.DTOs;

namespace Singulare.Services.QuoteService
{
    public interface IQuoteService
    {
        Task<List<Quote>> GetQuotes();
        Task<Quote?> GetQuoteByid(int QuoteId);
        Task<List<Quote>> createQuote(Quote quote);

        int createManyQuote(List<CreateQuoteDto> quotes);
    }
}
