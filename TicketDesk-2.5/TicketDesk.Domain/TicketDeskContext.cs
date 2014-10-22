// TicketDesk - Attribution notice
// Contributor(s):
//
//      Stephen Redd (stephen@reddnet.net, http://www.reddnet.net)
//
// This file is distributed under the terms of the Microsoft Public 
// License (Ms-PL). See http://ticketdesk.codeplex.com/license
// for the complete terms of use. 
//
// For any distribution that contains code from this file, this notice of 
// attribution must remain intact, and a copy of the license must be 
// provided to the recipient.

using System.Data.Entity.Core.Objects;
using System.Data.Entity.Infrastructure;
using TicketDesk.Domain.Conventions;
using TicketDesk.Domain.Model;
using System.Data.Entity;
using TicketDesk.Domain.Models;

namespace TicketDesk.Domain
{


    public class TicketDeskContext : DbContext
    {
        public TicketDeskContext()
            : base("name=TicketDesk")
        {

        }

        public ObjectQuery<T> GetObjectQueryFor<T>(IDbSet<T> entity) where T : class
        {
            var oc = ((IObjectContextAdapter)this).ObjectContext;
            return oc.CreateObjectSet<T>();
        }

        public virtual DbSet<Setting> Settings { get; set; }
        public virtual DbSet<TicketAttachment> TicketAttachments { get; set; }
        public virtual DbSet<TicketComment> TicketComments { get; set; }
        public virtual DbSet<TicketEventNotification> TicketEventNotifications { get; set; }
        public virtual DbSet<Ticket> Tickets { get; set; }
        public virtual DbSet<TicketTag> TicketTags { get; set; }
        public virtual DbSet<UserSetting> UserSettings { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            //TODO: Remove along with supoprting class if unneeded
            //modelBuilder.Conventions.Add(new NonPublicColumnAttributeConvention());

            modelBuilder.Entity<TicketComment>()
                .Property(e => e.Version)
                .IsFixedLength();

            modelBuilder.Entity<TicketComment>()
                .HasMany(e => e.TicketEventNotifications)
                .WithRequired(e => e.TicketComment)
                .HasForeignKey(e => new { e.TicketId, e.CommentId })
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Ticket>()
                .Property(e => e.Version)
                .IsFixedLength();

            modelBuilder.ComplexType<UserTicketListSettingsCollection>()
                .Property(p => p.Serialized)
                .HasColumnName("ListSettingsJson");

        }
    }
}