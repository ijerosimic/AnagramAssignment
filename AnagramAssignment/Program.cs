using System;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;

namespace AnagramAssignment
{
    internal static class Program
    {
        private static async Task Main(string[] args)
        {
            var services = BuildServices(args);
            var app = services.GetRequiredService<App>();
            await app.RunAsync();
        }

        private static IServiceProvider BuildServices(string[] args) =>
            new ServiceCollection()
                .AddSingleton(new Arguments(args))
                .AddSingleton<App>()
                .AddSingleton<IAnagramGrouper, AnagramGrouper>()
                .AddSingleton<IOutputPrinter, OutputPrinter>()
                .BuildServiceProvider();
    }
}