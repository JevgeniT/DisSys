﻿using System;
using Contracts.BLL.App.Services;
using Contracts.BLL.Base;
using Contracts.DAL.App.Repositories;

namespace Contracts.BLL.App
{
    public interface IAppBLL : IBaseBLL
    {
        public IPropertyService Properties { get; }
        
        public IInvoiceService Invoices { get; }
        public IRoomService Rooms { get; }
        public IReservationService Reservations { get; }
        public IExtraService Extras { get; }
        public IFacilityService Facilities { get; }
        public IPolicyService Policies { get; }
        public IRoomFacilitiesService RoomFacilities { get; }
        public IPropertyRoomsService PropertyRoomsService { get; }
        public IReviewService  Reviews{ get; }

        public IAvailabilityService Availabilities { get; }






    }
}