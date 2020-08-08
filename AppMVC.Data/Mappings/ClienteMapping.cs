using AppMVC.Business.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

//Mapeamentos do Banco de Dados, pode ser substituido peloas DataAnnotations nas Models ou ViewModels

namespace AppMVC.Data.Mappings
{
    public class ClienteMapping : IEntityTypeConfiguration<Cliente>
    {        
        public void Configure(EntityTypeBuilder<Cliente> builder)
        {
            //Define a chave como o Id
            builder.HasKey(c => c.Id);

            builder.Property(c => c.Nome)
                .IsRequired()
                .HasColumnType("varchar(200)");

            builder.Property(c => c.Documento)
                .IsRequired()
                .HasColumnType("varchar(11)");

            //Relacionamento 1:1
            builder.HasOne(c => c.Endereco)
            .WithOne(e => e.Cliente);

            builder.ToTable("Clientes");

        }
    }
}
