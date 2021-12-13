using ColetaAutomatica.API.Models;
using ColetaAutomatica.Core.Data;
using ColetaAutomatica.Core.Extensions;
using ColetaAutomatica.Core.Mediator;
using ColetaAutomatica.Core.Messages;
using FluentValidation.Results;
using Microsoft.EntityFrameworkCore;

namespace ColetaAutomatica.API.Data
{
    public class ColetaAutomaticaContext : DbContext, IUnitOfWork
    {
        private readonly IMediatorHandler _mediatorHandler;
        public ColetaAutomaticaContext(DbContextOptions<ColetaAutomaticaContext> options,
            IMediatorHandler mediatorHandler) : base(options)
        {
            _mediatorHandler = mediatorHandler;
            ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
            ChangeTracker.AutoDetectChangesEnabled = false;
        }

        public DbSet<SolicitacaoProcesso> SolicitacaoProcesso { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Ignore<ValidationResult>();
            modelBuilder.Ignore<Event>();
            foreach (var relationship in modelBuilder.Model.GetEntityTypes()
                .SelectMany(e => e.GetForeignKeys())) relationship.DeleteBehavior = DeleteBehavior.ClientSetNull;
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(ColetaAutomaticaContext).Assembly);
        }

        public async Task<bool> Commit()
        {
            foreach (var entry in ChangeTracker
                .Entries()
                .Where(entry => entry.Entity.GetType().GetProperty("DataCadastro") != null))
            {
                if (entry.State == EntityState.Added)
                {
                    entry.Property("DataCadastro").CurrentValue = DateTime.Now;
                }

                if (entry.State == EntityState.Modified)
                {
                    entry.Property("DataCadastro").IsModified = false;
                }
            }

            var sucesso = await base.SaveChangesAsync() > 0;
            if (sucesso)
            {
                await _mediatorHandler.PublicarEventos(this);
            }

            return sucesso;
        }
    }
}
