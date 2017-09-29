using System;
using System.Collections.Generic;
using System.Text;

namespace VSMefSample
{
    public class FruitMetadata
    {
        public FruitMetadata(IDictionary<string, object> metadata)
        {
            this.Type = (string)metadata["Type"];
        }

        public string Type { get; set; }
    }
}
