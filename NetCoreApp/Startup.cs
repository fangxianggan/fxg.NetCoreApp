using System;
using System.IO;
using System.Reflection;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NetCore.Core.Extensions;
using NetCore.Domain;
using NetCore.DTO.AutoMapper;
using NetCore.EntityFrameworkCore.Context;
using NetCore.IRepository;
using NetCore.IRepository.Common;
using NetCore.Repository;
using NetCore.Repository.Common;
using NetCoreApp.Extensions;
using Swashbuckle.AspNetCore.Swagger;

namespace NetCoreApp
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;

            
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        /// <summary>
        /// 
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            // Add framework services.
            //自动创建映射
            //automap
            services.AddAutoMapperSetup();
          
            //读取aoosettings.json里配置的数据库连接语句需要的代码
            var connection = Configuration.GetConnectionString("MySqlConnection");
            services.AddDbContext<DBContext>(options => options.UseMySql(connection));

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            var basePath = Microsoft.DotNet.PlatformAbstractions.ApplicationEnvironment.ApplicationBasePath;
            //Swagger所需要的配置项
            services.AddSwaggerGen(c =>
            {
                //添加Swagger.
                c.SwaggerDoc("CoreTest", new Info
                {
                    Version = "CoreTest",
                    Title = "第一个netcore项目接口文档",
                    Description = "This CoreTest Api",
                    //服务条款
                    TermsOfService = "None",
                    //作者信息
                    Contact = new Contact
                    {
                        Name = "方向感",
                        Email = string.Empty,
                        Url = "http://fxg.fxg91.com/"
                    },
                    //许可证
                    License = new License
                    {
                        Name = "许可证名字",
                        Url = "http://fxg.fxg91.com/"
                    }
                });

                c.CustomSchemaIds(type => type.FullName); // 解决相同类名会报错的问题
                // 下面三个方法为 Swagger JSON and UI设置xml文档注释路径

                var xmlPath = Path.Combine(basePath, "NetCoreApp.xml");//这个就是刚刚配置的xml文件名
                c.IncludeXmlComments(xmlPath, true);//默认的第二个参数是false，这个是controller的注释，记得修改

                var xmlModelPath = Path.Combine(basePath, "NetCore.EntityFrameworkCore.xml");
                c.IncludeXmlComments(xmlModelPath);

                //c.OperationFilter<AssignOperationVendorExtensions>();
                //添加对控制器的标签(描述)
                //  c.DocumentFilter<ApplyTagDescriptions>();
            });


            #region AutoFac

            //实例化 AutoFac  容器   
            var builder = new ContainerBuilder();

            //注册要通过反射创建的组件(注释：需要手动引用实体层)
            //builder.RegisterType<SysSampleServices>().As<ISysSampleServices>();
            //builder.RegisterType<SysSampleRepository>().As<ISysSampleRepository>();

            //通过反射将Services和Repository两个程序集的全部方法注入，要记得!!!这个注入的是实现类层，不是接口层 IServices
            try
            {
                builder.RegisterGeneric(typeof(BaseDomain<>)).As(typeof(IBaseDomain<>)).InstancePerDependency();
                builder.RegisterGeneric(typeof(Repository<>)).As(typeof(IRepository<>)).InstancePerDependency();
                builder.RegisterGeneric(typeof(DapperRepository<>)).As(typeof(IDapperRepository<>)).InstancePerDependency();


                var servicesDllFile = Path.Combine(basePath, "NetCore.Services.dll");
                var assemblysServices = Assembly.LoadFrom(servicesDllFile);
                builder.RegisterAssemblyTypes(assemblysServices).AsImplementedInterfaces();

                var repositoryDllFile = Path.Combine(basePath, "NetCore.Repository.dll");
                var assemblysRepository = Assembly.LoadFrom(repositoryDllFile);
                builder.RegisterAssemblyTypes(assemblysRepository).AsImplementedInterfaces();

                

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            //将services填充到Autofac容器生成器中
            builder.Populate(services);

            //使用已进行的组件登记创建新容器
            var ApplicationContainer = builder.Build();

            #endregion

            return new AutofacServiceProvider(ApplicationContainer);//第三方IOC接管 core内置DI容器


           

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        /// <summary>
        /// 
        /// </summary>
        /// <param name="app"></param>
        /// <param name="env"></param>
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            // 配置Swagger  必须加在app.UseMvc前面 API文档中间件
            app.UseSwagger(c =>
            {
                c.PreSerializeFilters.Add((swagger, httpReq) => swagger.Host = httpReq.Host.Value);

            });
            //Swagger Core需要配置的  必须加在app.UseMvc前面
            app.UseSwaggerUI(s =>
            {
                s.SwaggerEndpoint("/swagger/CoreTest/swagger.json", "My API 1.0.1");//注意,中间那段的名字 (refuge) 要和 上面 SwaggerDoc 方法定义的 名字 (refuge)一样
                s.RoutePrefix = string.Empty; //默认值是 "swagger" ,需要这样请求:https://localhost:44300/

            });
            //automapper
            app.UseStateAutoMapper();
            app.UseHttpsRedirection();
            app.UseMvc();
        }
    }
}
