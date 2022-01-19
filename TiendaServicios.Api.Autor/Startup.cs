using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using TiendaServicios.Api.Autor.Persistencia;
using Microsoft.EntityFrameworkCore;
using MediatR;
using TiendaServicios.Api.Autor.Aplicacion;
using FluentValidation.AspNetCore;
using AutoMapper;
using TiendaServicios.RabbitMQ.Bus.EventoQueue;
using TiendaServicios.RabbitMQ.Bus.BusRabbit;
using TiendaServicios.Api.Autor.ManejadorRabbit;
using TiendaServicios.RabbitMQ.Bus.Implement;

namespace TiendaServicios.Api.Autor
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
            services.AddSingleton<IRabbitEventBus, RabbitEventBus>(sp =>
            {
                var scopeFactory = sp.GetRequiredService<IServiceScopeFactory>();

                return new RabbitEventBus(sp.GetService<IMediator>(), scopeFactory);
            });
            services.AddTransient<EmailEventoManejador>();
            services.AddControllers().AddFluentValidation(cfb => cfb.RegisterValidatorsFromAssemblyContaining<Nuevo>());

            services.AddDbContext<ContextoAutor>(options =>
            {
                object p = options.UseNpgsql(Configuration.GetConnectionString("postgreeConexion"));
            });

             //add after migration from 3.1 to 6
         //   services.AddTransient(typeof(IPipelineBehavior<,>), typeof(RequestPreProcessorBehavior<,>));
            services.AddMediatR(typeof(Nuevo.Manejador).Assembly);
            services.AddAutoMapper(typeof(Consulta.Manejador));
            
            services.AddTransient<IEventoManejador<EmailEventoQueue>, EmailEventoManejador>();
            


        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                
            }

           // app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            var eventBus = app.ApplicationServices.GetRequiredService<IRabbitEventBus>();
            eventBus.Suscribe<EmailEventoQueue, EmailEventoManejador>();
        }
    }
}
