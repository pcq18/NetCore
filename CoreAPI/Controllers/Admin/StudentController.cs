using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CoreAPI.Entity;
using CoreAPI.Bussiness.Admin;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace CoreAPI.Controllers
{
    /// <summary>
    /// 学生信息接口
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Policy = "Admin")]//角色验证
    public class StudentController : Controller
    {
        private StudentBLL bll = new StudentBLL();
        /// <summary>
        /// 获取单个学生信息
        /// </summary>
        /// <param name="id">id</param>
        /// <returns></returns>
        [Route("GetStudentById")]
        [HttpPost]
        [ProducesResponseType(typeof(Student), 200)]
        public JsonResult GetStudentById(int id)
        {
            return Json(bll.GetById(id));
        }
        /// <summary>
        /// 获取所有学生信息
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        [Route("GetStudentPageList")]
        [HttpPost]
        [ProducesResponseType(typeof(Student), 200)]
        public JsonResult GetStudentPageList(int pageIndex = 1, int pageSize = 10)
        {
            return Json(bll.GetPageList(pageIndex, pageSize));
        }
        /// <summary>
        /// 添加学生信息
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        [Route("AddStudent")]
        [HttpPost]
        [ProducesResponseType(typeof(Student), 200)]
        public JsonResult AddStudent(Student entity)
        {
            if (entity == null)
                return Json("参数为空");
            return Json(bll.Add(entity));
        }

        /// <summary>
        /// 编辑学生信息
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        [Route("EditStudent")]
        [HttpPost]
        [ProducesResponseType(typeof(Student), 200)]
        public JsonResult EditStudent(Student entity)
        {
            if (entity == null)
                return Json("参数为空");
            return Json(bll.Update(entity));
        }
        /// <summary>
        /// 删除学生信息
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        [Route("Dels")]
        [HttpDelete]
        public JsonResult Dels(dynamic[] ids = null)
        {
            if (ids.Length == 0)
                return Json("参数为空");
            return Json(bll.Dels(ids));
        }
    }
}