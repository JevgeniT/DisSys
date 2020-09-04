using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BLL.App.DTO;
using Contracts.BLL.App;
using DAL.App.EF;
using Extensions;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Public.DTO;

namespace WebApp.ApiControllers
{
    /// <summary>
    ///  Reviews
    /// </summary>
    [ApiController]
    [ApiVersion( "1.0" )]
    [Route("api/v{version:apiVersion}/[controller]")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class ReviewController : ControllerBase
    {
        private readonly IAppBLL _bll;
        private readonly DALMapper<Review, ReviewDTO> _mapper = new DALMapper<Review, ReviewDTO>();

        /// <summary>
        /// Constructor
        /// </summary>
        public ReviewController(IAppBLL bll)
        {
            _bll = bll;
        }

   
        /// <summary>
        /// Get all the property Reviews
        /// </summary>
        /// <param name="pId">property Id</param>
        /// <returns>Array of reviews</returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ReviewDTO>>> GetReviews([FromQuery] Guid pId)
        {
            // var reviews = (await _bll.Reviews.AllAsync()).Select(r=> _mapper.Map(r));             
            var reviews = (await _bll.Reviews.PropertyReviews(pId)).Select(r=> _mapper.Map(r));
            return Ok(reviews);
        }

        
        /// <summary>
        /// Get a single Review
        /// </summary>
        /// <param name="id">Review Id</param>
        /// <returns>Array of reviews</returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<ReviewDTO>> GetReview(Guid id)
        {
            var review = await _bll.Reviews.FindAsync(id);

            if (review == null)
            {
                return NotFound(new MessageDTO($"Review with id {id} not found"));
            }

            return Ok(_mapper.Map(review));
        }

        
        /// <summary>
        /// Update the Review
        /// </summary>
        /// <param name="id">Review Id</param>
        /// <param name="review">Review to update</param>
        /// <returns></returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> PutReview(Guid id, ReviewDTO review)
        {
            if (id != review.Id)
            {
                return BadRequest(new MessageDTO("Ids does not match!"));
            }

            if (!await _bll.Reviews.ExistsAsync(id))
            {
                return BadRequest();

            }

            _bll.Reviews.Update(_mapper.Map(review));
            await _bll.SaveChangesAsync();
            return NoContent();
        }


        /// <summary>
        /// Post the new Review
        /// </summary>
        /// <param name="review"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult<ReviewDTO>> PostReview(ReviewDTO review)
        {
            review.AppUserId = User.UserGuidId();
            review.CreatedAt = DateTime.Now;
            
            var entity = _mapper.Map(review);

            _bll.Reviews.Add(entity);
            await _bll.SaveChangesAsync();

            review.Id = entity.Id;
            
            return CreatedAtAction("GetReview", new { id = review.Id }, review);
        }

        /// <summary>
        /// Delete the Review
        /// </summary>
        /// <param name="id">Review id to delete</param>
        /// <returns>Review just deleted</returns>
        [HttpDelete("{id}")]
        public async Task<ActionResult<ReviewDTO>> DeleteReview(Guid id)
        {
            var review = await _bll.Reviews.FirstOrDefaultAsync(id);
            
            if (review == null)
            {
                return NotFound();
            }

            _bll.Reviews.Remove(review);
            
            await _bll.SaveChangesAsync();

            return Ok(review);
        }

       
    }
}
