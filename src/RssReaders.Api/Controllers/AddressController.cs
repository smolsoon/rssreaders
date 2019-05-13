using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using RssReaders.Infrastructure.Commands;
using RssReaders.Infrastructure.Services;

namespace RssReaders.Api.Controllers
{
    [Route("[Controller]")]
    public class AddressController : Controller
    {
        private readonly IAddressService _addressService;
        public AddressController(IAddressService addressService)
        {
            _addressService = addressService;
        }

        [HttpGet]
        public async Task<IActionResult> Get(ObjectId id)
            => Json (await _addressService.GetAddress(id));

        [HttpGet("string/{id}")]
        public string GetString(ObjectId id)
         => _addressService.GetLink(id);

        [HttpPost("create")]
        public async Task<IActionResult> Post([FromBody] CreateAddress command)
        {
            await _addressService.CreateAddress(ObjectId.GenerateNewId(), command.Link, command.Category);
            
            return Created("/link", null);
        }
    }
}