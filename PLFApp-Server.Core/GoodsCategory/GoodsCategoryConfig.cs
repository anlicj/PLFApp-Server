using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace PLFApp.Server.Core
{
    public class GoodsCategoryConfig : IEntityTypeConfiguration<GoodsCategory>
    {
        public void Configure(EntityTypeBuilder<GoodsCategory> builder)
        {
            builder.HasIndex("IsShowOnHomePage");
        }
    }
}
