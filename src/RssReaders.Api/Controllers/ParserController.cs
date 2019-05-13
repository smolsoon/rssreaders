using System.Threading.Channels;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using RssReaders.Core.Repositories;
using RssReaders.Infrastructure.Services;
using RssReaders.Infrastructure.Commands;

namespace RssReaders.Api.Controllers
{
    [Route("[Controller]")]
    public class ParserController : Controller
    {
        private readonly IParserService _parserService;
        private readonly IAddressService _addressService;
        public ParserController(IParserService parserService, IAddressService addressService)
        {
            _parserService = parserService;
            _addressService = addressService;
        }

        [HttpPost("create")]
        public async Task<IActionResult> Post([FromBody] CreateChannel command)
        {

            var link = _addressService.GetLink(command.Id);// pobieranie linka z bazy danych
            var temp = _parserService.ParseRss(link); // nastepuje parsowanie 
             await _parserService.CreateChannel(command.Id, command.Title, command.Description, command.Link, command.Language, command.Copyright,
                command.LastBuildDate, command.PubDate, command.Items);

            return Created("/link", null);
        }
    }
}