using AppMVC.Business.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace AppMVC.Data.Mappings
{
    
    public class VendaMapping : IEntityTypeConfiguration<Venda>
    {       

        public void Configure(EntityTypeBuilder<Venda> builder)
        {
            builder.HasKey(v => v.Id);

            builder.Property(p => p.Observacoes)                
                .HasColumnType("varchar(500)");
        }
    }
}
