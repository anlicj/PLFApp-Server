using PLFApp.Server.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PLFApp.Server.EntityFrameworkCore
{
    public class DbInitializer
    {
        public static void Initialize(PLFAppDbContext context)
        {
            context.Database.EnsureCreated();
            if (!context.Member.Any())
            {
                var member = new Member() {
                    MobilePhone="13212345678",
                    Password="123456",
                    NickName="张三",
                    HeadImageUrl=""
                };
                context.Member.Add(member);
                context.SaveChanges();
            }
        }
    }
}
