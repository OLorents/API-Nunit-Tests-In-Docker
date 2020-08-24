using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using WebApp.Data;

namespace WebApp
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        //Data Source = {DB}; uid={uid}; pwd={pwd}; => set available db and credentials
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();
            services.AddDbContext<ProductsDbContext>(option => option.UseSqlServer(@"Data Source = {DB}; uid={uid}; pwd={pwd}; Initial Catalog = ProductsDb;"));
            services.AddDbContext<CompanyDbContext>(option => option.UseSqlServer(@"Data Source = {DB}; uid={uid}; pwd={pwd}; Initial Catalog = CompaniesDb;"));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ProductsDbContext productsDbContext, CompanyDbContext companyDbContext)
        {
            if (env.IsDevelopment())
            {
                app.UseBrowserLink();
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
            }
            //app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseMvc();
            productsDbContext.Database.EnsureCreated();
            companyDbContext.Database.EnsureCreated();
        }
    }
}
