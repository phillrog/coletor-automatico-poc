using ColetaAutomatica.ConsultaJurisprudencia.API.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ColetaAutomatica.ConsultaJurisprudencia.API.Data.Mappings
{
    public class SolicitacaoProcessoMapping : IEntityTypeConfiguration<SolicitacaoProcesso>
    {
        public void Configure(EntityTypeBuilder<SolicitacaoProcesso> builder)
        {
            builder.HasKey(s => s.Id);
            
            builder.Property(s => s.NumeroProcesso).IsRequired();
            builder.Property(s => s.DataCadastro);
        }
    }
}
