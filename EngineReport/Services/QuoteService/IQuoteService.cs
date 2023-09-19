namespace EngineReport.Services.QuoteService
{
    public interface IQuoteService
    {
        Task<List<Quote>> GetQuotes(Nullable<DateTime> startDate = null, Nullable<DateTime> finalDate = null);
        Task<Quote?> GetQuoteByid(int QuoteId);
        Task<List<Quote>> createQuote(Quote quote);
    }
}
