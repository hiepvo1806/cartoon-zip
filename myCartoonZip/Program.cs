using Autofac;
using Microsoft.Extensions.DependencyInjection;
using newCartoonImplementation;
using NewCartoonInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace myCartoonZip
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            IServiceCollection serviceCollection = new ServiceCollection();
            ConfigureServices(serviceCollection);
            var sp = serviceCollection.BuildServiceProvider();

            var builder = new ContainerBuilder();

            RegisterAutofacService(builder);
            using (var scope = builder.Build().BeginLifetimeScope())
            {
                var cartoonService = sp.GetService<ICartoonService>();


                //var logService = scope.Resolve<ILogService<List<ListViewItem>>>();
                var logService = sp.GetService<ILogService<List<ListViewItem>>>();
                //Application.Run(new Form1(cartoonService));
                Application.Run(new Form1(cartoonService, logService));
            }
        }

        static void RegisterAutofacService(ContainerBuilder builder)
        {
            
            ////builder.RegisterType<LogService>().As<ILogService>();
            //builder
            //    .RegisterGeneric(typeof(LogService<>))
            //    .As(typeof(ILogService<>))
            //    .InstancePerLifetimeScope();
            ////builder.RegisterInstance(new CartoonService()).As<ICartoonService>();


            //builder.RegisterType<CartoonService>().As<ICartoonService>();
            
        }


        static private void ConfigureServices(IServiceCollection serviceCollection)
        {
            serviceCollection.AddSingleton(typeof(ILogService<>), typeof(LogService<>));
            serviceCollection.AddSingleton<ICartoonService, CartoonService>();
        }
    }
}
