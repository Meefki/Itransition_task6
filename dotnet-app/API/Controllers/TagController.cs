using Application.Repositories;
using Domain;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [EnableCors("Default")]
    [Route("api/[controller]")]
    [ApiController]
    public class TagController : ControllerBase
    {
        private readonly ILogger<TagController> logger;
        private readonly ITagRepository tagRepository;

        public TagController(
            ILogger<TagController> logger,
            ITagRepository tagRepository)
        {
            this.logger = logger;
            this.tagRepository = tagRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetTags(string startWith)
        {
            try
            {
                IEnumerable<Tag> mathedTags = await tagRepository.GetAsync(startWith);
                return Ok(mathedTags.Select(x => x.Name));
            }
            catch (Exception ex)
            {
                logger.LogError(ex, ex.Message);
            }

            return BadRequest();
        }
    }
}
