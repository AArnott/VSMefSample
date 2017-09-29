using System;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.VisualStudio.Composition;

namespace VSMefSample
{
    class Program
    {
        static void Main(string[] args)
        {
            var exportProvider = CreateExportProviderAsync().GetAwaiter().GetResult();
            Tree tree = exportProvider.GetExportedValue<Tree>();
            var apple1 = (Apple)tree.CreateFruit("Apple");
            apple1.IsJuicy = true;
            var apple2 = (Apple)tree.CreateFruit("Apple");
            var pear = (Pear)tree.CreateFruit("Pear");
            pear.IsDelicious = true;

            Console.WriteLine(tree);
        }

        static async Task<ExportProvider> CreateExportProviderAsync(CancellationToken cancellationToken = default(CancellationToken))
        {
            var discovery = new AttributedPartDiscovery(Resolver.DefaultInstance);
            var parts = await discovery.CreatePartsAsync(Assembly.GetExecutingAssembly(), cancellationToken);
            var catalog = ComposableCatalog.Create(Resolver.DefaultInstance)
                .AddParts(parts);
            var composition = CompositionConfiguration.Create(catalog);
            composition.CreateDgml().Save("mefgraph.dgml");
            if (!composition.CompositionErrors.IsEmpty)
            {
                foreach (var error in composition.CompositionErrors.Peek())
                {
                    Console.Error.WriteLine(error.Message);
                    foreach (var part in error.Parts)
                    {
                        Console.Error.WriteLine("   " + part.Definition.Type.FullName);
                    }
                }
            }

            composition.ThrowOnErrors();
            var exportProviderFactory = composition.CreateExportProviderFactory();
            return exportProviderFactory.CreateExportProvider();
        }
    }
}
