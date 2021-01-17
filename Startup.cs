using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace HelloApp
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc(); // добавляем сервисы MVC
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app) //, IWebHostEnvironment env)
        {
            //  if (env.IsDevelopment())
            //  {
            //      app.UseDeveloperExceptionPage();
            //  }

             app.UseRouting();  // используем систему маршрутизации

              app.UseEndpoints(endpoints =>
              {
                  endpoints.MapControllerRoute(
                       name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
                  //   endpoints.MapGet("/", async context =>
                  // {

                  //       await context.Response.WriteAsync("Hello World!");
              });
           
            int x = 5;
            int y = 8;
            int z = 0;
            app.Use(async (context, next) =>
            {
                z = x * y;
                await next.Invoke();
                z = z + 1;
                await context.Response.WriteAsync($"Result: {z}");
            });

            app.Run(async (context) =>
            {
                z = z + 1;
                await Task.FromResult(0);
                //await context.Response.WriteAsync($"<h3>{x} * {y} = {z}</h3>");
            });
        }
    }
}
