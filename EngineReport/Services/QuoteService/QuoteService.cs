namespace EngineReport.Services.QuoteService
{
    public class QuoteService : IQuoteService
    {
        private readonly DataContext _context;
        public QuoteService(DataContext context)
        {
            _context = context;
        }
        
        public Task<List<Quote>> createQuote(Quote quote)
        {
            _context.Quotes.Add(quote);
            _context.SaveChanges();
            return _context.Quotes.ToListAsync();
        }

        public async Task<Quote?> GetQuoteByid(int quoteId)
        {
            var quote = await _context.Quotes.FindAsync(quoteId);
            return quote;
        }

        public async Task<List<Quote>> GetQuotes(Nullable<DateTime> startDate = null, Nullable<DateTime> finalDate = null)
        {
            List<Quote> quotesFound;

            if (startDate is not null && finalDate is not null)
            {
                quotesFound = await _context.Quotes.Where(quote => quote.Date >= startDate && quote.Date <= finalDate).ToListAsync();
            }
            else
            {
                quotesFound = await _context.Quotes.ToListAsync();
            }
            return quotesFound;
        }
    }
}
