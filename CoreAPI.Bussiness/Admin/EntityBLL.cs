using CoreAPI.IService;
using CoreAPI.Model;
using CoreAPI.Service;
using CoreAPI.Token;
using CoreAPI.Token.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace CoreAPI.Bussiness.Admin
{
    public class EntityBLL
    {
        private IEntity iService = new EntityService();

        /// <summary>
        /// 生成实体类
        /// </summary>
        /// <param name="entityName">表名</param>
        /// <param name="contentRootPath">存放路径</param>
        /// <returns></returns>
        public MessageModel<string> CreateEntity(string entityName, string contentRootPath)
        {
            string[] arr = contentRootPath.Split('\\');
            string baseFileProvider = "";
            for (int i = 0; i < arr.Length - 1; i++)
            {
                baseFileProvider += arr[i];
                baseFileProvider += "\\";
            }
            string filePath = baseFileProvider + "CoreAPI.Entity";
            if (iService.CreateEntity(entityName, filePath))
                return new MessageModel<string> { Success = true, Msg = "生成成功" };
            else
                return new MessageModel<string> { Success = false, Msg = "生成失败" };
        }

        public MessageModel<string> CreateToken(int id, string sub, int expiresSliding, int expiressAbsoulte)
        {
            TokenModel model = new TokenModel();
            model.Uid = id;
            model.Sub = sub;
            string key = CoreAPIToken.IssueJWT(model, TimeSpan.FromMinutes(expiresSliding), TimeSpan.FromDays(expiressAbsoulte));
            if (!string.IsNullOrEmpty(key))
            {
                List<string> list = new List<string>();
                list.Add(key);
                return new MessageModel<string> { Success = true, Msg = "生成成功", Data = list };
            }
            else
                return new MessageModel<string> { Success = false, Msg = "生成失败" };
        }
    }
}
