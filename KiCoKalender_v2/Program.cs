using DAL;
using Microsoft.Azure.Functions.Worker.Extensions.OpenApi;
using Microsoft.Azure.Functions.Worker.Extensions.OpenApi.Extensions;
using Microsoft.Azure.Functions.Worker.Extensions.OpenApi.Functions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Repository;
using Service;
using Service.Interfaces;
using Microsoft.AspNetCore.Builder;

namespace KiCoKalender_v2
{
    public class Program
    {

		public static void Main()
		{
			IHost host = new HostBuilder()
				.ConfigureFunctionsWorkerDefaults(worker => worker.UseNewtonsoftJson())
				.ConfigureServices(Configure)
				.Build();

			host.Run();
		}

		static void Configure(HostBuilderContext Builder, IServiceCollection Services)
		{
			Services.AddDbContext<KiCoKalenderContext>(options =>
			options.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=KiCoKalender;Trusted_Connection=True;MultipleActiveResultSets=true"));
			Services.AddSingleton<IOpenApiHttpTriggerContext, OpenApiHttpTriggerContext>();
			Services.AddSingleton<IOpenApiTriggerFunction, OpenApiTriggerFunction>();
			Services.AddSingleton<IUserService, UserService>();
			Services.AddSingleton<IAppointmentService, AppointmentService>();
			Services.AddSingleton<AppointmentRepository>();
			Services.AddSingleton<UserRepository>();
		}

	}
}