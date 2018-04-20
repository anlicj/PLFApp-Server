using PLFApp.Server.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace PLFApp.Server.EntityFrameworkCore
{
    public class MemberRepository:BaseRepository<Member>,IMemberRepository
    {
        public MemberRepository(PLFAppDbContext context) : base(context) { }
    }
}
