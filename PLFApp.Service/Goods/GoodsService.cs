using PLFApp.Server.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace PLFApp.Service
{
    public class GoodsService : BaseService<Goods>, IGoodsService
    {
        public GoodsService(IGoodsRepository _repository)
        {
            this.repository = _repository;
        }
    }
}
