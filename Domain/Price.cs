using System;
using Contracts.DAL.Base;
using DAL.Base;

namespace Domain
{
    public class Price : IDomainEntityBaseMetadata
    {

        public decimal Total { get; set; }


        public Guid Id { get; set; }
    }
}