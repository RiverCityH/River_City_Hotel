using asp_hoteles.Pages.Emergentes;
using lib_aplicaciones.Implementaciones;
using lib_entidades_dominio;
using lib_repositorios;
using lib_repositorios.Implementaciones;

namespace asp_hoteles
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public static IConfiguration? Configuration { set; get; }

        public void ConfigureServices(IServiceCollection services)
        {
            // Repositorios
            services.AddScoped<Conexion, Conexion>();
            services.AddScoped<PaisesRepositorio, PaisesRepositorio>();
            services.AddScoped<DepartamentosRepositorio, DepartamentosRepositorio>();
            services.AddScoped<CiudadesRepositorio, CiudadesRepositorio>();
            services.AddScoped<TiposRepositorio, TiposRepositorio>();
            services.AddScoped<PersonasRepositorio, PersonasRepositorio>();
            services.AddScoped<EmpleadosRepositorio, EmpleadosRepositorio>();
            // Aplicaciones
            services.AddScoped<PaisesAplicacion, PaisesAplicacion>();
            services.AddScoped<DepartamentosAplicacion, DepartamentosAplicacion>();
            services.AddScoped<CiudadesAplicacion, CiudadesAplicacion>();
            services.AddScoped<TiposAplicacion, TiposAplicacion>();
            services.AddScoped<PersonasAplicacion, PersonasAplicacion>();
            services.AddScoped<EmpleadosAplicacion, EmpleadosAplicacion>();
            // Emergentes
            services.AddScoped<PaisesPPModel, PaisesPPModel>();
            services.AddScoped<DepartamentosPPModel, DepartamentosPPModel>();

            services.AddRazorPages();
            services.AddMvc();
            services.AddSession(options =>
            {
                options.IdleTimeout = TimeSpan.FromMinutes(30);
            });
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
                app.UseDeveloperExceptionPage();
            else
                app.UseExceptionHandler("/Error");
            app.UseStaticFiles();
            app.UseRouting();
            app.UseAuthorization();
            app.UseSession();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapRazorPages();
            });
        }
    }
}