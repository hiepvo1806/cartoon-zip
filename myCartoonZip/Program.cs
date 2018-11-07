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
            var cartoonService = sp.GetService<ICartoonService>();
            var logService = sp.GetService<ILogService<List<ListViewItem>>>();
            Application.Run(new MangaDownloadForm(cartoonService, logService,sp));
        }

        static private void ConfigureServices(IServiceCollection serviceCollection)
        {
            serviceCollection.AddSingleton(typeof(ILogService<>), typeof(LogService<>));
            serviceCollection.AddSingleton<ICartoonService, TruyenTranhTuanCartoonService>();
        }
    }
}
