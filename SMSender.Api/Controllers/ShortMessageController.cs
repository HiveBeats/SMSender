using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MassTransit;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SMSender.Api.Dto;
using SMSender.Shared.Dto;
using SMSender.Shared.Models;

namespace SMSender.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ShortMessageController: ControllerBase
    {
        private readonly IPublishEndpoint _publisher;
        private readonly AppDbContext _db;
        public ShortMessageController(IPublishEndpoint publisher, AppDbContext db)
        {
            _publisher = publisher;
            _db = db;
        }

        [HttpPost]
        public async Task<IActionResult> SendShortMessage([FromBody] SendMessageRequest request)
        {
            try
            {
                await _publisher.Publish(new ShortMessageCreated()
                {
                    From = request.From,
                    To = request.To,
                    Content = request.Content
                }, new CancellationToken());
            }
            catch (Exception ex)
            {
                return BadRequest();
            }

            return Ok();
        }
        
        [HttpGet("{id:Guid}")]
        public async Task<ActionResult<ShortMessageDto>> GetMessage(Guid id)
        {
            var item = await _db.ShortMessages.FindAsync(id.ToString());
            if (item == null)
                throw new Exception("SMS not found");

            return Ok(new ShortMessageDto()
            {
                From = item.From,
                To = item.To,
                Content = item.Content,
                Status = item.Status.ToString()
            });
        }
        
        [HttpGet("All")]
        public async Task<ActionResult<IEnumerable<ShortMessageDto>>> GetAllMessages()
        {
            var items = await _db.ShortMessages.ToListAsync();
            return Ok(items.Select(x => new ShortMessageDto()
            {
                From = x.From,
                To = x.To,
                Status = x.Status.ToString()
            }));
        }


    }
}