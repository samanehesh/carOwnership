﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace CarOwnership
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class CarOwnershipEntities1 : DbContext
    {
        public CarOwnershipEntities1()
            : base("name=CarOwnershipEntities1")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<Car> Cars { get; set; }
        public virtual DbSet<CarManufacturer> CarManufacturers { get; set; }
        public virtual DbSet<CarOwner> CarOwners { get; set; }
        public virtual DbSet<OwnershipHistory> OwnershipHistories { get; set; }
        public virtual DbSet<ServiceCenter> ServiceCenters { get; set; }
        public virtual DbSet<ServiceEmployee> ServiceEmployees { get; set; }
        public virtual DbSet<ServiceHistory> ServiceHistories { get; set; }
    }
}