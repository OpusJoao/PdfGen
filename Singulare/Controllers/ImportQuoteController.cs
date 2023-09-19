using Singulare.Services.QuoteService;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using CsvHelper.Configuration;
using CsvHelper;
using System.Globalization;
using Singulare.DTOs;

namespace Singulare.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImportQuoteController : ControllerBase
    {

        private readonly IQuoteService _quotesService;

        public ImportQuoteController(IQuoteService quoteService)
        {
            _quotesService = quoteService;
        }

        //[HttpPost]
        //public async Task<ActionResult<string>> ImportByCsvString(IFormFile csvFile)
        //{
        //    List<CreateQuoteDto> quotes;
        //    string line;
        //    int linesAffecteds = 0;

        //    using (var reader = new StreamReader(csvFile.OpenReadStream()))
        //    using (var csv = new CsvReader(reader, new CsvConfiguration(CultureInfo.InvariantCulture)))
        //    {
        //       quotes = csv.GetRecords<CreateQuoteDto>().ToList();
        //    }

        //    linesAffecteds = await _quotesService.createManyQuote(quotes);
        //    return Ok($"{linesAffecteds} lines affecteds.");
        //}
    }
}
