using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BLL.App.DTO;
using BLL.Base.Mappers;
using BLL.Base.Services;
using Contracts.BLL.App.Services;
using Contracts.DAL.App;
using Contracts.DAL.App.Repositories;

namespace BLL.App.Services
{
    public class ReviewService: 
        BaseEntityService<IReviewRepository, IAppUnitOfWork, DAL.App.DTO.Review, Review>, IReviewService
    {
        public ReviewService(IAppUnitOfWork unitOfWork) 
            : base(unitOfWork, new BaseBLLMapper<DAL.App.DTO.Review, Review>(), unitOfWork.Reviews)
        {
        }
        public async Task<IEnumerable<Review>> PropertyReviews(Guid? propertyId)
        {
            return (await ServiceRepository.PropertyReviews(propertyId)).Select( dalEntity => Mapper.Map(dalEntity) );
        }

        public override async Task<bool> ExistsAsync(Guid reservationId, object? userId = null)
        {
            return  await ServiceRepository.ExistsAsync(reservationId, userId);
        }

    }
    
    
}