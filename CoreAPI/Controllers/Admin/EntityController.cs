using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CoreAPI.Bussiness.Admin;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;

namespace CoreAPI.Controllers.Admin
{
    /// <summary>
    /// 生成实体
    /// </summary>
    [Route("api/System/[controller]")]
    [ApiController]
    public class EntityController : Controller
    {
        private EntityBLL bll = new EntityBLL();
        private readonly IHostingEnvironment _hostingEnvironment;
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="hostingEnvironment"></param>
        public EntityController(IHostingEnvironment hostingEnvironment)
        {
            _hostingEnvironment = hostingEnvironment;
        }
        /// <summary>
        /// 生成实体类
        /// </summary>
        /// <param name="entityName"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("CreateEntity")]
        public JsonResult CreateEntity(string entityName = null)
        {
            if (entityName == null)
                return Json("参数为空");
            return Json(bll.CreateEntity(entityName, _hostingEnvironment.ContentRootPath));
        }
        /// <summary>
        /// 获取Token
        /// </summary>
        /// <param name="id">用户Id</param>
        /// <param name="sub">用户名</param>
        /// <param name="expiresSliding">相对过期时间，单位为分</param>
        /// <param name="expiressAbsoulte">绝对过期时间，单位为天</param>
        /// <returns></returns>
        [HttpPost]
        [Route("Token")]
        public JsonResult Token(int id,string sub,int expiresSliding,int expiressAbsoulte)
        {
            if (string.IsNullOrEmpty(sub))
                return Json("用户名为空");
            return Json(bll.CreateToken(id, sub, expiresSliding, expiressAbsoulte));
        }
    }
    
}