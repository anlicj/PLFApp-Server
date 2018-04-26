using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace PLFApp.Server.Core
{
    public class GoodsConfig : IEntityTypeConfiguration<Goods>
    {
        public void Configure(EntityTypeBuilder<Goods> builder)
        {
            builder.HasIndex("GoodsName");
            builder.HasIndex("GoodsState");
            builder.HasOne(m => m.GoodsCategory).WithMany(m => m.Goodses).IsRequired().HasForeignKey(m=>m.GoodsCategoryId).OnDelete(DeleteBehavior.Cascade);
        }
    }
}
