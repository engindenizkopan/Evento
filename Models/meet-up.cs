namespace MyProject.Models
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class meet_up : DbContext
    {
        public meet_up()
            : base("name=MyProjectDB")
        {
        }

        public virtual DbSet<_event> events { get; set; }
        public virtual DbSet<user> users { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<_event>()
                .Property(e => e.date)
                .IsFixedLength();

            modelBuilder.Entity<_event>()
                .Property(e => e.time)
                .IsFixedLength();

            modelBuilder.Entity<_event>()
                .Property(e => e.location)
                .IsFixedLength();

            modelBuilder.Entity<_event>()
                .Property(e => e.details)
                .IsFixedLength();

            modelBuilder.Entity<_event>()
                .Property(e => e.category)
                .IsFixedLength();

            modelBuilder.Entity<_event>()
                .Property(e => e.owner)
                .IsFixedLength();

            modelBuilder.Entity<user>()
                .Property(e => e.city)
                .IsFixedLength();
        }
    }
}
