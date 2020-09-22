using AppMVC.Business.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace AppMVC.Data.Mappings
{
    class VendaItemMapping : IEntityTypeConfiguration<VendaItem>
    {
        public void Configure(EntityTypeBuilder<VendaItem> builder)
        {
            builder.HasKey(v => v.Id);

            builder.Property(p => p.Quantidade)
                .HasColumnType("int");
        }
    }
}
