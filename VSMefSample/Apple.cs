using System.Composition;

namespace VSMefSample
{
    [Export(typeof(Fruit))]
    [ExportMetadata("Type", "Apple")]
    public class Apple : Fruit
    {
        public bool IsJuicy { get; set; }

        public override string ToString() => $"Apple (IsJuicy: {this.IsJuicy})";
    }
}
