﻿using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Infrastructure.Entities;

namespace Infrastructure.Contexts
{
    public class UserContext : IdentityDbContext<UserEntity>
    {
        public UserContext(DbContextOptions options) : base(options)
        {
        }

        public virtual DbSet<AddressEntity> Addresses { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<UserEntity>()
                .HasOne(x => x.Address)
                .WithMany(a => a.Users)
                .HasForeignKey(a => a.AddressId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
