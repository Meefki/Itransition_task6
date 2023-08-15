using API.Models;
using Application.Repositories;
using Domain;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [EnableCors("Default")]
    [ApiController]
    [Route("api/[controller]")]
    public class MessageController : ControllerBase
    {
        private readonly ILogger<MessageController> logger;
        private readonly IMessageRepository messageRepository;

        public MessageController(
            ILogger<MessageController> logger,
            IMessageRepository messageRepository)
        {
            this.logger = logger;
            this.messageRepository = messageRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetMessages([FromQuery] IEnumerable<string> tags)
        {
            try
            {
                IEnumerable<Message> messages = await messageRepository.GetAsync(tags);
                return Ok(messages.Select(
                    x => new MessageDTO()
                    {
                        Message = x.Value,
                        SentDate = x.SentDate,
                        Tags = x.Tags.Any() ? x.Tags.Select(t => t.Name) : new List<string>()
                    }));
            }
            catch (Exception ex)
            {
                logger.LogError(ex, ex.Message);
            }
            
            return BadRequest();
        }
    }
}