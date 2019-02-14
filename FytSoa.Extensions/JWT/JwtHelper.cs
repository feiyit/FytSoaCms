using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace FytSoa.Extensions
{
    public class JwtHelper
    {
        /// <summary>
        /// 颁发JWT字符串
        /// </summary>
        /// <param name="tokenModel"></param>
        /// <returns></returns>
        public static string IssueJWT(TokenModel tokenModel)
        {
            var dateTime = DateTime.UtcNow;
            var claims = new Claim[]
            {
                new Claim(JwtRegisteredClaimNames.Jti,tokenModel.Uid.ToString()),//用户Id
                new Claim("Role", tokenModel.Role),//身份
                new Claim("Project", tokenModel.Project),//身份
                new Claim(JwtRegisteredClaimNames.Iat,dateTime.ToString(),ClaimValueTypes.Integer64)
            };
            //秘钥
            var jwtConfig = new JwtAuthConfigModel();
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtConfig.JWTSecretKey));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            //过期时间
            double exp = 0;
            switch (tokenModel.TokenType)
            {
                case "Web":
                    exp = jwtConfig.WebExp;
                    break;
                case "App":
                    exp = jwtConfig.AppExp;
                    break;
                case "MiniProgram":
                    exp = jwtConfig.MiniProgramExp;
                    break;
                case "Other":
                    exp = jwtConfig.OtherExp;
                    break;
            }
            var jwt = new JwtSecurityToken(
                issuer: "FytSoa",
                claims: claims, //声明集合
                expires: dateTime.AddHours(exp),
                signingCredentials: creds);

            var jwtHandler = new JwtSecurityTokenHandler();
            var encodedJwt = jwtHandler.WriteToken(jwt);

            return encodedJwt;
        }

        /// <summary>
        /// 解析
        /// </summary>
        /// <param name="jwtStr"></param>
        /// <returns></returns>
        public static TokenModel SerializeJWT(string jwtStr)
        {
            var jwtHandler = new JwtSecurityTokenHandler();
            JwtSecurityToken jwtToken = jwtHandler.ReadJwtToken(jwtStr);
            object role = new object(); ;
            object project = new object();
            try
            {
                jwtToken.Payload.TryGetValue("Role", out role);
                jwtToken.Payload.TryGetValue("Project", out project);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
            var tm = new TokenModel
            {
                Uid = jwtToken.Id,
                Role = role.ToString(),
                Project = project.ToString()
            };
            return tm;
        }
    }
    /// <summary>
    /// 令牌
    /// </summary>
    public class TokenModel
    {
        /// <summary>
        /// 用户Id
        /// </summary>
        public string Uid { get; set; }
        /// <summary>
        /// 身份
        /// </summary>
        public string Role { get; set; }
        /// <summary>
        /// 项目名称
        /// </summary>
        public string Project { get; set; }
        /// <summary>
        /// 令牌类型
        /// </summary>
        public string TokenType { get; set; }
    }
}
