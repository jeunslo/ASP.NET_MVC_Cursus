﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace MVCBierenApplication.Models
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class MVCBierenEntities : DbContext
    {
        public MVCBierenEntities()
            : base("name=MVCBierenEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<Bier> Bieren { get; set; }
        public virtual DbSet<Brouwer> Brouwers { get; set; }
        public virtual DbSet<Soort> Soorten { get; set; }
    }
}
