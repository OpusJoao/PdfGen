using Microsoft.EntityFrameworkCore;
using Singulare.DTOs;

namespace Singulare.Services.QuoteService
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

        public int createManyQuote(List<CreateQuoteDto> quotes)
        {
            _context.Database.EnsureCreated();
            var QuotesConverted = quotes.ConvertAll(x => new Quote {
                High = x.High,
                Low = x.Low,
                Close = x.Close,
                AdjClose = x.AdjClose,
                Date = x.Date,
                Open = x.Open,
                Volume = x.Volume
            });
            _context.Quotes.AddRange(QuotesConverted);
            var linesAffecteds = _context.SaveChanges();
            return linesAffecteds;
        }

        public async Task<Quote?> GetQuoteByid(int quoteId)
        {
            var quote = await _context.Quotes.FindAsync(quoteId);
            return quote;
        }

        public async Task<List<Quote>> GetQuotes()
        {
            var quotes = await _context.Quotes.ToListAsync();
            return quotes;
        }
    }
}
