using EnsaPlatform.Areas.Identity.Data;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Identity;

[assembly: HostingStartup(typeof(EnsaPlatform.Areas.Identity.IdentityHostingStartup))]
namespace EnsaPlatform.Areas.Identity
{
    public class IdentityHostingStartup : IHostingStartup
    {
        public void Configure(IWebHostBuilder builder)
        {
            builder.ConfigureServices((context, services) =>
            {
                services.AddDbContext<EnsaPlatformContext>(options =>
                    options.UseSqlServer(
                        context.Configuration.GetConnectionString("EnsaPlatformContextConnection")));

                services.AddIdentity<EnsaPlatformUser, IdentityRole>()
    .AddDefaultTokenProviders()
    .AddDefaultUI()
    .AddEntityFrameworkStores<EnsaPlatformContext>();


                //services.AddDefaultIdentity<EnsaPlatformUser>(options => options.SignIn.RequireConfirmedAccount = false)
                //    .AddEntityFrameworkStores<EnsaPlatformContext>();
            });
        }
    }
}