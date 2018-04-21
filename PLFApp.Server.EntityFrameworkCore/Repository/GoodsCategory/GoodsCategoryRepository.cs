using PLFApp.Server.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace PLFApp.Server.EntityFrameworkCore
{
    public class GoodsCategoryRepository : BaseRepository<GoodsCategory>, IGoodsCategoryRepository
    {
        public GoodsCategoryRepository(PLFAppDbContext _context) : base(_context)
        {
        }
    }
}
