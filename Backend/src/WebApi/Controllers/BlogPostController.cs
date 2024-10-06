using Application.Dtos.BlogPostDtos;
using Application.IServices;
using Application.Models;
using AutoMapper;
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

        [HttpGet]
        public async Task<ActionResult<List<BlogPostDto>>> GetAll()
        {
            var blogPosts = await _blogPostService.GetAllBlogPosts();
            var blogPostDtos = _mapper.Map<List<BlogPostDto>>(blogPosts);

            return Ok(blogPostDtos);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<BlogPostDto>> GetById(int id)
        {
            var blogPost = await _blogPostService.GetBlogPostById(id);
            if (blogPost == null)
            {
                return NotFound();
            }
            var blogPostDto = _mapper.Map<BlogPostDto>(blogPost);
            return Ok(blogPostDto);
        }

        [HttpPost]
        public async Task<ActionResult<BlogPostDto>> Create([FromBody] BlogPostDto blogPostDto)
        {
            var blogPostModel = _mapper.Map<BlogPostModel>(blogPostDto);
            var createdBlogPost = await _blogPostService.CreateBlogPost(blogPostModel);
            var createdBlogPostDto = _mapper.Map<BlogPostDto>(createdBlogPost);

            return CreatedAtAction(nameof(GetById), new { id = createdBlogPost.Id }, createdBlogPost);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<BlogPostDto>> Update([FromBody] int id, BlogPostDto blogPostDto)
        {
            if (id != blogPostDto.Id)
            {
                return BadRequest();
            }
            var blogPost = _mapper.Map<BlogPostModel>(blogPostDto);
            var updatedBlogPost = await _blogPostService.UpdateBlogPost(blogPost);

            if (updatedBlogPost == null)
            {
                return NotFound();
            }
            var updatedBlogPostDto = _mapper.Map<BlogPostDto>(updatedBlogPost);

            return Ok(updatedBlogPostDto);
        }

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
