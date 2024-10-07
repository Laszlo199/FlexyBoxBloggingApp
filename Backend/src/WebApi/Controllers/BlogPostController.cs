using Application.Dtos.BlogPostDtos;
using Application.IServices;
using Application.Models;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BlogPostController : ControllerBase
    {
        private readonly IBlogPostService _blogPostService;
        private readonly IMapper _mapper;

        public BlogPostController(IBlogPostService blogPostService, IMapper mapper)
        {
            _blogPostService = blogPostService;
            _mapper = mapper;
        }

        [Authorize]
        [HttpGet]
        public async Task<ActionResult<List<BlogPostDto>>> GetAll()
        {
            var blogPosts = await _blogPostService.GetAllBlogPosts();

            return Ok(blogPosts);
        }

        [Authorize]
        [HttpGet("{id}")]
        public async Task<ActionResult<BlogPostDto>> GetById(int id)
        {
            var blogPost = await _blogPostService.GetBlogPostById(id);

            return Ok(blogPost);
        }

        [Authorize]
        [HttpPost]
        public async Task<ActionResult<BlogPostDto>> Create([FromBody] CreateBlogPostDto dto)
        {
            var blogPostModel = _mapper.Map<BlogPostModel>(dto);
            var createdBlogPost = await _blogPostService.CreateBlogPost(blogPostModel);
            var createdDto = _mapper.Map<CreateBlogPostDto>(createdBlogPost);

            return CreatedAtAction(nameof(GetById), new { id = createdBlogPost.Id }, createdBlogPost);
        }

        // ToDO: Refactor Update method
        [Authorize]
        [HttpPut("{id}")]
        public async Task<ActionResult<BlogPostDto>> Update(int id, [FromBody] UpdateBlogPostDto dto)
        {
            var blogPost = _mapper.Map<BlogPostModel>(dto);
            blogPost.Id = id;
            var updatedBlogPost = await _blogPostService.UpdateBlogPost(blogPost);


            return Ok(_mapper.Map<BlogPostDto>(updatedBlogPost));
        }

        [Authorize]
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var deleted = await _blogPostService.DeleteBlogPost(id);
            if (!deleted)
            {
                return NotFound();
            }
            return NoContent();
        }
    }
}
