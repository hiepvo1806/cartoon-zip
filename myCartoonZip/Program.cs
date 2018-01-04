using Autofac;
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
            var builder = new ContainerBuilder();
            RegisterAutofacService(builder);
            using (var scope = builder.Build().BeginLifetimeScope())
            {
                var cartoonService = scope.Resolve<ICartoonService>();
                var logService = scope.Resolve<ILogService<List<ListViewItem>>>();
                //Application.Run(new Form1(cartoonService));
                Application.Run(new Form1(cartoonService, logService));
            }
        }

        static void RegisterAutofacService(ContainerBuilder builder)
        {
            builder.RegisterType<CartoonService>().As<ICartoonService>();
            //builder.RegisterType<LogService>().As<ILogService>();
            builder
                .RegisterGeneric(typeof(LogService<>))
                .As(typeof(ILogService<>));
            //builder.RegisterInstance(new CartoonService()).As<ICartoonService>();
        }
    }
}
