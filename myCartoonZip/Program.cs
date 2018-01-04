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
                //Application.Run(new Form1(cartoonService));
                Application.Run(new Form1(new CartoonService()));
            }
        }

        static void RegisterAutofacService(ContainerBuilder builder)
        {
            builder.RegisterType<CartoonService>().As<ICartoonService>();
            //builder.RegisterInstance(new CartoonService()).As<ICartoonService>();
        }
    }
}
