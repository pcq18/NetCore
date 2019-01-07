using CoreAPI.Token;
using CoreAPI.Token.Model;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace CoreAPI.AuthHelper
{
    /// <summary>
    /// Token验证授权中间件
    /// </summary>
    public class TokenAuth
    {
        /// <summary>
        /// http委托
        /// </summary>
        private readonly RequestDelegate _next;
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="next"></param>
        public TokenAuth(RequestDelegate next)
        {
            _next = next;
        }
        /// <summary>
        /// 验证授权
        /// </summary>
        /// <param name="httpContext"></param>
        /// <returns></returns>
        public Task Invoke(HttpContext httpContext)
        {
            var headers = httpContext.Request.Headers;
            //检测是否包含'Authorization'请求头，如果不包含返回context进行下一个中间件，用于访问不需要认证的API
            if (!headers.ContainsKey("Authorization"))
            {
                return _next(httpContext);
            }
            var tokenStr = headers["Authorization"];
            try
            {
                string jwtStr = tokenStr.ToString().Substring("Bearer ".Length).Trim();
                //验证缓存中是否存在该jwt字符串
                if (!CoreAPIMemoryCache.Exists(jwtStr))
                {
                    return httpContext.Response.WriteAsync("非法请求");
                }
                TokenModel tm = ((TokenModel)CoreAPIMemoryCache.Get(jwtStr));
                //提取tokenModel中的Sub属性进行authorize认证
                List<Claim> lc = new List<Claim>();
                Claim c = new Claim(tm.Sub + "Type", tm.Sub);
                lc.Add(c);
                ClaimsIdentity identity = new ClaimsIdentity(lc);
                ClaimsPrincipal principal = new ClaimsPrincipal(identity);
                httpContext.User = principal;
                return _next(httpContext);
            }
            catch (Exception)
            {
                return httpContext.Response.WriteAsync("token验证异常");
            }
        }
    }
}
