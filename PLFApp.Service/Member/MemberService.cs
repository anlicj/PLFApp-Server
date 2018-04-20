using PLFApp.Server.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace PLFApp.Service
{
    public class MemberService : BaseService<Member>, IMemberService
    {
        public MemberService(IMemberRepository _repository)
        {
            this.repository = _repository;
        }
    }
}
