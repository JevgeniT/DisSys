using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BLL.App.DTO;
using Contracts.BLL.App;
using Extensions;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Public.DTO;
using Public.DTO.Mappers;

namespace WebApp.ApiControllers.v1._0
{
    
    /// <summary>
    /// Facility controller
    /// </summary>
    [ApiController]
    [ApiVersion( "1.0" )]
    [Route("api/v{version:apiVersion}/[controller]")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class FacilityController : ControllerBase
    {
        private readonly IAppBLL _bll;
        private IMemoryCache _cache;
        private readonly DTOMapper<Facility, FacilityDTO> _mapper = new DTOMapper<Facility, FacilityDTO>();

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="bll"></param>
        public FacilityController(IAppBLL bll, IMemoryCache cache)
        {
            _bll = bll;
            _cache = cache;
        }
        
        
        /// <summary>
        /// Get all facilities
        /// </summary>
        /// <returns>Array of facilities</returns>
        [HttpGet]
        [Produces("application/json")]
        [AllowAnonymous]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<FacilityDTO>))]
        public async Task<ActionResult<IEnumerable<FacilityDTO>>> GetFacilities()
        {
            
            if (!_cache.TryGetValue(User.UserGuidId(),out var facilities))
            {
                facilities = (await _bll.Facilities.AllAsync()).Select(a => _mapper.Map(a));
                 _cache.Set(User.UserGuidId(),facilities);
                return Ok(facilities);
            }

            facilities = _cache.Get(User.UserGuidId());
    
            return Ok(facilities);
        }
    }
}