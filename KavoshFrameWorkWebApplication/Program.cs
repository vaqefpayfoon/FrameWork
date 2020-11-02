
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using PersianCulture;
using Serilog;
using System;
using System.Globalization;
using System.Threading;

namespace KavoshFrameWorkWebApplication
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
            CultureInfo info = new CultureInfo("fa-Ir");
            PersianCultureHelper.SetPersianOptions(info);
            Thread.CurrentThread.CurrentCulture = info;
            string str = DateTime.Now.ToString("yyyy/MM/dd");
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
              Host.CreateDefaultBuilder(args)
                  .ConfigureWebHostDefaults(webBuilder =>
                  {
                      webBuilder.UseStartup<Startup>();
                  }).UseSerilog((hostingContext, loggerConfiguration) =>
                  {
                      loggerConfiguration.ReadFrom.Configuration(hostingContext.Configuration);
                  });
    }
}
