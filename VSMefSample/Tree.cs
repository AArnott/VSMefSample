using System;
using System.Collections.Generic;
using System.Composition;
using System.Linq;
using System.Text;

namespace VSMefSample
{
    [Export]
    public class Tree : IDisposable
    {
        private readonly List<Export<Fruit>> createdFruit = new List<Export<Fruit>>();

        [ImportMany]
        public List<ExportFactory<Fruit, FruitMetadata>> FruitFactories { get; set; }

        public Fruit CreateFruit(string type)
        {
            ExportFactory<Fruit> fruitFactory = this.FruitFactories.Single(ff => ff.Metadata.Type == type);
            Export<Fruit> export = fruitFactory.CreateExport();
            this.createdFruit.Add(export);
            return export.Value;
        }

        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.AppendLine("Tree");
            foreach (var fruit in this.createdFruit)
            {
                sb.AppendLine($"   {fruit.Value}");
            }

            return sb.ToString();
        }

        public void Dispose() => this.Dispose(true);

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                foreach (Export<Fruit> export in this.createdFruit)
                {
                    export.Dispose();
                }

                this.createdFruit.Clear();
            }
        }
    }
}
