using System;
using System.Collections;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using IdentityModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using PLFApp.Service;

namespace PLFApp.Server.WebApi.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    public class MemberController : Controller
    {
        IConfiguration configuration;
        IMemberService memberService;

        static Dictionary<string, RefreshStoreData> refreshTokenTable = new Dictionary<string, RefreshStoreData>();

        public MemberController(IConfiguration _configuration, IMemberService _memberService)
        {
            configuration = _configuration;
            memberService = _memberService;
        }

        [HttpPost("[action]")]
        public IActionResult Login([FromBody]MemberLoginModel data)
        {
            var member = memberService.GetEntity(m => m.MobilePhone == data.MobilePhone && m.Password == data.Password);
            if (member == null)
            {
                return Unauthorized();
            }
            var refreshData = new RefreshStoreData();
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(configuration.GetSection("JWTBearer").GetValue<string>("ClientSeret"));
            var authTime = DateTime.UtcNow;
            var expiresAt = authTime.AddDays(7);
            var refreshToken = Guid.NewGuid().ToString().Replace("-", "");
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(JwtClaimTypes.Audience,"api"),
                    new Claim(JwtClaimTypes.Issuer,"http://localhost:56325"),
                    new Claim(JwtClaimTypes.Id, member.Id.ToString()),
                    new Claim(JwtClaimTypes.PhoneNumber, member.MobilePhone),
                    new Claim("refresh_token", refreshToken)
                }),
                Expires = expiresAt,
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            var tokenString = tokenHandler.WriteToken(token);
            refreshData.AuthorizeAt = new DateTimeOffset(authTime).ToUnixTimeSeconds();
            refreshData.MemberId = member.Id;
            refreshData.MobilePhone = member.MobilePhone;
            refreshData.ExpiresAt = new DateTimeOffset(expiresAt).ToUnixTimeSeconds();
            refreshTokenTable.Add(refreshToken, refreshData);
            return Ok(new
            {
                access_token = tokenString,
                token_type = "Bearer",
                profile = new
                {
                    sid = refreshData.MemberId,
                    refresh_token = refreshToken,
                    auth_time = refreshData.AuthorizeAt,
                    expires_at = refreshData.ExpiresAt
                }
            });
        }

        [HttpPost("[action]")]
        public IActionResult RefreshToken([FromBody] RefreshTokenInputModel data)
        {
            RefreshStoreData refreshData;
            if (string.IsNullOrWhiteSpace(data.RefreshToken) ||!refreshTokenTable.TryGetValue(data.RefreshToken, out refreshData))
            {
                return Unauthorized();
            }
            refreshTokenTable.Remove(data.RefreshToken);
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(configuration.GetSection("JWTBearer").GetValue<string>("ClientSeret"));
            var authTime = DateTime.UtcNow;
            var expiresAt = authTime.AddDays(7);
            var refreshToken = Guid.NewGuid().ToString().Replace("-", "");
            refreshData.AuthorizeAt = new DateTimeOffset(authTime).ToUnixTimeSeconds();
            refreshData.ExpiresAt = new DateTimeOffset(expiresAt).ToUnixTimeSeconds();
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(JwtClaimTypes.Audience,"api"),
                    new Claim(JwtClaimTypes.Issuer,"http://localhost:56325"),
                    new Claim(JwtClaimTypes.Id, refreshData.MemberId.ToString()),
                    new Claim(JwtClaimTypes.PhoneNumber, refreshData.MobilePhone),
                    new Claim("refresh_token", refreshToken)
                }),
                Expires = expiresAt,
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            var tokenString = tokenHandler.WriteToken(token);
            refreshTokenTable.Add(refreshToken, refreshData);
            return Ok(new
            {
                access_token = tokenString,
                token_type = "Bearer",
                profile = new
                {
                    sid = refreshData.MemberId,
                    refresh_token = refreshToken,
                    auth_time = refreshData.AuthorizeAt,
                    expires_at = refreshData.ExpiresAt
                }
            });
        }

        public class MemberLoginModel
        {
            public string MobilePhone { get; set; }
            public string Password { get; set; }
        }
        public class RefreshTokenInputModel
        {
            public string RefreshToken { get; set; }
        }

        class RefreshStoreData
        {
            public int MemberId { get; set; }
            public string MobilePhone { get; set; }
            public long AuthorizeAt { get; set; }
            public long ExpiresAt { get; set; }
        }
    }
}