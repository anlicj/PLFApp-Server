using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.Extensions.DependencyInjection;
using PLFApp.Server.Core;
using PLFApp.Server.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace EntityFrameworkCoreTest
{
    public class RepositoryTest
    {
        ServiceProvider serviceProvider;
        public RepositoryTest()
        {
            var serviceCollection = new ServiceCollection();
            serviceCollection.AddDbContext<PLFAppDbContext>(options => options.UseInMemoryDatabase("PLFAppTestDb"));
            serviceCollection.AddScoped(typeof(IMemberRepository), typeof(MemberRepository));
            serviceProvider = serviceCollection.BuildServiceProvider();
        }
        [Fact]
        public async Task  FindListExcludeSoftDelete()
        {
            List<Member> notDeleteMember;
            //List<Member> allMember;
            var memberService = serviceProvider.GetRequiredService<IMemberRepository>();
            using (var db=serviceProvider.GetRequiredService<PLFAppDbContext>())
            {
                var memberList = new List<Member>();
                memberList.Add(new Member() {
                    MobilePhone="111111111111",
                    NickName="111",
                    Password="123456"
                });
                memberList.Add(new Member()
                {
                    MobilePhone = "222222222222",
                    NickName = "222",
                    Password = "123456",
                    IsDelete=true
                });
                memberService.AddRange(memberList);
                await memberService.SaveChangesAsync();

                //allMember = await memberService.FindList(m => true,false).ToListAsync();
                notDeleteMember = await memberService.FindList(m => true, true).ToListAsync();
            }
            Assert.Single(notDeleteMember);
        }
    }
}
