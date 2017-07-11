using System;
using System.Collections.Generic;

namespace INAX.Models
{
    public partial class ImageProduct
    {
        public int id { get; set; }
        public Nullable<int> idProduct { get; set; }
        public string Name { get; set; }
        public string Images { get; set; }
    }
}
