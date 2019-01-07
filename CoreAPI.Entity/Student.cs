using System;
using System.Linq;
using System.Text;
using SqlSugar;

namespace CoreAPI.Entity
{
    ///<summary>
    ///
    ///</summary>
    [SugarTable("Student")]
    public partial class Student
    {
           public Student(){


           }
           /// <summary>
           /// Desc:主键自增列
           /// Default:
           /// Nullable:False
           /// </summary>           
           [SugarColumn(IsPrimaryKey=true,IsIdentity=true)]
           public int ID {get;set;}

           /// <summary>
           /// Desc:姓名
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string Name {get;set;}

           /// <summary>
           /// Desc:年龄
           /// Default:
           /// Nullable:True
           /// </summary>           
           public int? Age {get;set;}

           /// <summary>
           /// Desc:性别(0男  1女)
           /// Default:
           /// Nullable:True
           /// </summary>           
           public int? Sex {get;set;}

    }
}
