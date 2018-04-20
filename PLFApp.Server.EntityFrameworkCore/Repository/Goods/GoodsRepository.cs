using PLFApp.Server.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace PLFApp.Server.EntityFrameworkCore
{
    public class GoodsRepository:BaseRepository<Goods>,IGoodsRepository
    {
        public GoodsRepository(PLFAppDbContext context) : base(context) { }
    }
}
