using System.Composition;

namespace VSMefSample
{
    public class Fruit
    {
        [Import]
        public Tree GrewFrom { get; set; }

        public int SeedCount { get; set; }
    }
}
