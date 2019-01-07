using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using CoreAPI.AuthHelper;
using CoreAPI.SwaggerHelper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Swashbuckle.AspNetCore.Swagger;

namespace CoreAPI
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
            #region Swagger
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info
                {
                    Version = "v1.1.0",
                    Title = "Core WebAPI",
                    Description = "框架集合",
                    //TermsOfService = "None",
                    //Contact = new Swashbuckle.AspNetCore.Swagger.Contact { Name = "RayWang", Email = "2271272653@qq.com", Url = "http://www.cnblogs.com/RayWang" }
                });
                //添加读取注释服务
                var basePath = AppDomain.CurrentDomain.BaseDirectory;// @"E:\Personal\Project\NetCodeTest\RayPI\";
                var xmlPath = Path.Combine(basePath, "APIHelp.xml");
                c.IncludeXmlComments(xmlPath, true);
                var xmlEntityPath = Path.Combine(basePath, "EntityHelp.xml");
                c.IncludeXmlComments(xmlEntityPath);

                //添加对控制器的标签(描述)
                c.DocumentFilter<SwaggerDocTag>();

                //添加header验证信息
                //c.OperationFilter<SwaggerHeader>();
                var security = new Dictionary<string, IEnumerable<string>> { { "Bearer", new string[] { } }, };
                c.AddSecurityRequirement(security);//添加一个必须的全局安全信息，和AddSecurityDefinition方法指定的方案名称要一致，这里是Bearer。
                c.AddSecurityDefinition("Bearer", new ApiKeyScheme
                {
                    Description = "JWT授权(数据将在请求头中进行传输) 参数结构: \"Authorization: Bearer {token}\"",
                    Name = "Authorization",//jwt默认的参数名称
                    In = "header",//jwt默认存放Authorization信息的位置(请求头中)
                    Type = "apiKey"
                });
            });
            #endregion

            #region Token

            #region 缓存

            services.AddSingleton<IMemoryCache>(factory =>
            {
                var cache = new MemoryCache(new MemoryCacheOptions());
                return cache;
            });

            #endregion
            #region 认证

            services.AddAuthorization(options =>
            {
                options.AddPolicy("System", policy => policy.RequireClaim("SystemType").Build());
                options.AddPolicy("Client", policy => policy.RequireClaim("ClientType").Build());
                options.AddPolicy("Admin", policy => policy.RequireClaim("AdminType").Build());
            });

            #endregion

            #endregion

            #region CORS 跨域问题
            services.AddCors(c =>
            {
                c.AddPolicy("AllowAnyOrigin", policy =>
                {
                    policy.AllowAnyOrigin()//允许任何源
                    .AllowAnyMethod()//允许任何方式
                    .AllowAnyHeader()//允许任何头
                    .AllowCredentials();//允许cookie
                });
                //可配置在数据库或配置文件中
                string[] origins = { "http://localhost:8083" };
                c.AddPolicy("AllowSpecificOrigin", policy =>
                {
                    policy.WithOrigins(origins)
                    .WithMethods("GET", "POST", "PUT", "DELETE")
                    .WithHeaders("authorization");
                });
            });
            #endregion
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            #region Swagger
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "ApiHelp V1");
            });
            #endregion

            //将TokenAuth类注册为中间件
            app.UseMiddleware<TokenAuth>();

            app.UseHttpsRedirection();
            app.UseMvc();

            app.UseStaticFiles();//用于访问wwwroot下的文件 
        }
    }
}
