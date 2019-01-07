using CoreAPI.Entity;
using CoreAPI.IService;
using CoreAPI.Model;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace CoreAPI.Service
{
    public class StudentService : BaseDB, IStudent
    { 
        //private SqlSugarClient db = BaseDB.GetClient();
        public SimpleClient<Student> sdb = new SimpleClient<Student>(BaseDB.GetClient());

        #region base
        /// <summary>
        /// 获取带分页列表
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public TableModel<Student> GetPageList(int pageIndex, int pageSize)
        {
            PageModel p = new PageModel() { PageIndex = pageIndex, PageSize = pageSize };
            Expression<Func<Student, bool>> ex = (it => 1 == 1);
            List<Student> data = sdb.GetPageList(ex, p);
            TableModel<Student> t = new TableModel<Student>();
            t.Code = 0;
            t.Count = p.PageCount;
            t.Data = data;
            t.Msg = "成功";
            return t;
        }
        /// <summary>
        /// 获取一条信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Student Get(int id)
        {
            return sdb.GetById(id);
        }
        /// <summary>
        /// 添加一条信息
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public bool Add(Student entity)
        {
            return sdb.Insert(entity);
        }
        /// <summary>
        /// 修改一条信息
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public bool Update(Student entity)
        {
            return sdb.Update(entity);
        }
        /// <summary>
        /// 批量删除
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public bool Dels(dynamic[] ids)
        {
            return sdb.DeleteByIds(ids);
        }
        #endregion
    }
}
