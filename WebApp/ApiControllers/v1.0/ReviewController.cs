using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BLL.App.DTO;
using Contracts.BLL.App;
using Contracts.DAL.App;
using DAL.App.EF;
using Extensions;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
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
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<ReviewDTO>))]
        public async Task<ActionResult<IEnumerable<ReviewDTO>>> GetReviews([FromQuery] Guid pId)
        {
            var reviews = (await _bll.Reviews.PropertyReviews(pId)).Select(r=> _mapper.Map(r));
            return Ok(reviews);
        }

        
        /// <summary>
        /// Get a single Review
        /// </summary>
        /// <param name="id">Review Id</param>
        /// <returns>Array of reviews</returns>
        [HttpGet("{id}")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ReviewDTO))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(MessageDTO))]
        public async Task<ActionResult<ReviewDTO>> GetReview(Guid id)
        {
            var review = await _bll.Reviews.FirstOrDefaultAsync(id);

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
        [Produces("application/json")]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(MessageDTO))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(MessageDTO))]
        public async Task<IActionResult> PutReview(Guid id, ReviewDTO review)
        {
            if (id != review.Id)
            {
                return BadRequest(new MessageDTO("Ids does not match!"));
            }

            if (!await _bll.Reviews.ExistsAsync(id))
            {
                return BadRequest(new MessageDTO("Review does not exists"));

            }

            await _bll.Reviews.UpdateAsync(_mapper.Map(review));
            await _bll.SaveChangesAsync();
            return NoContent();
        }


        /// <summary>
        /// Post the new Review
        /// </summary>
        /// <param name="review"></param>
        /// <returns></returns>
        [HttpPost]
        [Produces("application/json")]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(ReviewDTO))] 
        public async Task<ActionResult<ReviewDTO>> PostReview(ReviewDTO review)
        {
            review.AppUserId = User.UserGuidId();
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
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ReviewDTO))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(MessageDTO))]
        public async Task<ActionResult<ReviewDTO>> DeleteReview(Guid id)
        {
            var review = await _bll.Reviews.FirstOrDefaultAsync(id);
            
            if (review == null)
            {
                return NotFound(new MessageDTO($"Review with id {id} was not found"));
            }
            await _bll.Reviews.RemoveAsync(review);
            
            await _bll.SaveChangesAsync();

            return Ok(review);
        }

       
    }
}
