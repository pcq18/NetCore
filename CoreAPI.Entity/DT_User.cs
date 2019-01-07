using System;
using System.Linq;
using System.Text;
using SqlSugar;

namespace CoreAPI.Entity
{
    ///<summary>
    ///
    ///</summary>
    [SugarTable("DT_User")]
    public partial class DT_User
    {
           public DT_User(){


           }
           /// <summary>
           /// Desc:
           /// Default:
           /// Nullable:False
           /// </summary>           
           [SugarColumn(IsPrimaryKey=true,IsIdentity=true)]
           public int ID {get;set;}

           /// <summary>
           /// Desc:
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string UserName {get;set;}

           /// <summary>
           /// Desc:
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string Sex {get;set;}

    }
}
