using System.Composition;

namespace VSMefSample
{
    [Export(typeof(Fruit))]
    [ExportMetadata("Type", "Pear")]
    public class Pear : Fruit
    {
        public bool IsDelicious { get; set; }

        public override string ToString() => $"Pear (IsDelicious: {this.IsDelicious})";
    }
}
