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
    [ApiController]
    [ApiVersion( "1.0" )]
    [Produces("application/json")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class ReviewController : ControllerBase
    {
        private readonly IAppBLL _bll;
        private readonly DALMapper<Review, ReviewDTO> _mapper = new DALMapper<Review, ReviewDTO>();

        public ReviewController(IAppBLL bll)
        {
            _bll = bll;
        }

        [HttpGet][Route("property/{propertyId}")]
        public async Task<ActionResult<IEnumerable<ReviewDTO>>> GetReviewsForProperty(Guid propertyId)
        {
            var reviews = (await _bll.Reviews.PropertyReviews(propertyId)).Select(r=> _mapper.Map(r));             
            return Ok(reviews);
        }
        
        // GET: api/Review
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Review>>> GetReviews()
        {
            return Ok(await _bll.Reviews.AllAsync());
        }

        // GET: api/Review/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Review>> GetReview(Guid id)
        {
            var review = await _bll.Reviews.FindAsync(id);

            if (review == null)
            {
                return NotFound();
            }

            return review;
        }

        // PUT: api/Review/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutReview(Guid id, Review review)
        {
            if (id != review.Id)
            {
                return BadRequest();
            }

            // _bll.Entry(review).State = EntityState.Modified;

            try
            {
                await _bll.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (! await _bll.Reviews.ExistsAsync(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Review
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<Review>> PostReview(Review review)
        {
            review.AppUserId = User.UserGuidId();
            review.CreatedAt = DateTime.Now;
            _bll.Reviews.Add(review);
            await _bll.SaveChangesAsync();

            return CreatedAtAction("GetReview", new { id = review.Id }, review);
        }

        // DELETE: api/Review/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Review>> DeleteReview(Guid id)
        {
            var review = await _bll.Reviews.FindAsync(id);
            if (review == null)
            {
                return NotFound();
            }

            _bll.Reviews.Remove(review);
            await _bll.SaveChangesAsync();

            return review;
        }

       
    }
}
