using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WithJwt.Core.Dtos.Posts;
using WithJwt.Core.Services;

namespace WithJwt.API.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class PostsController : BaseController
    {
        readonly IPostService _postService;

        public PostsController(IPostService postService)
        {
            _postService = postService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _postService.GetAllAsync();
            return ActionResultInstance(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await _postService.GetByIdAsync(id);
            return ActionResultInstance(result);
        }

        [HttpPost]
        public async Task<IActionResult> Add(PostAddDto postAddDto)
        {
            var result = await _postService.AddAsync(postAddDto);
            return ActionResultInstance(result);
        }

        [HttpPut]
        public async Task<IActionResult> Edit(PostUpdateDto postUpdateDto)
        {
            var result = await _postService.UpdateAsync(postUpdateDto);
            return ActionResultInstance(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Edit(int id)
        {
            var result = await _postService.HardRemoveAsync(id);
            return ActionResultInstance(result);
        }
    }
}
