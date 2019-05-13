using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using RssReaders.Infrastructure.Commands;
using RssReaders.Infrastructure.Services;

namespace RssReaders.Api.Controllers
{
    [Route("[controller]")]
    public class ItemController : Controller
    {
        private readonly IItemService _itemService;
        public ItemController(IItemService itemService)
        {
            _itemService = itemService;            
        }

        [HttpGet]
        public async Task<IActionResult> Get()
            => Json (await _itemService.GetItems());

        [HttpPost("create")]
        public async Task<IActionResult> Post([FromBody] CreateItem command)
        {
            await _itemService.CreateItems(ObjectId.GenerateNewId(), command.Title, command.Link, command.Description, command.PubDate);
            
            return Created("/link", null);
        }
    }
}