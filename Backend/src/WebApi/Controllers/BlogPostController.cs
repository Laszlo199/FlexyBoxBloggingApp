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
    [AllowAnonymous]
    public class BlogPostController : ControllerBase
    {
        private readonly IBlogPostService _blogPostService;
        private readonly IMapper _mapper;

        public BlogPostController(IBlogPostService blogPostService, IMapper mapper)
        {
            _blogPostService = blogPostService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<List<BlogPostDto>>> GetAllBlogPost()
        {
            var blogPosts = await _blogPostService.GetAllBlogPosts();

            return Ok(blogPosts);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<BlogPostDto>> GetBlogPostById(int id)
        {
            var blogPost = await _blogPostService.GetBlogPostById(id);

            return Ok(blogPost);
        }

        [HttpPost]
        public async Task<ActionResult<BlogPostDto>> CreateNewBlogPost([FromBody] CreateBlogPostDto dto)
        {
            var blogPostModel = _mapper.Map<BlogPostModel>(dto);
            var createdBlogPost = await _blogPostService.CreateBlogPost(blogPostModel);
            var createdDto = _mapper.Map<CreateBlogPostDto>(createdBlogPost);

            return CreatedAtAction(nameof(GetBlogPostById), new { id = createdBlogPost.Id }, createdBlogPost);
        }

        // ToDO: Refactor Update method
        [HttpPut("{id}")]
        public async Task<ActionResult<BlogPostDto>> UpdateBlogPost(int id, [FromBody] UpdateBlogPostDto dto)
        {
            var blogPost = _mapper.Map<BlogPostModel>(dto);
            blogPost.Id = id;
            var updatedBlogPost = await _blogPostService.UpdateBlogPost(blogPost);


            return Ok(_mapper.Map<BlogPostDto>(updatedBlogPost));
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteBlogPost(int id)
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