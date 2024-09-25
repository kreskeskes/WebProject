using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebProjectUniversity.Core.Domain.Entities.ProductFolder
{
    public class ProductProductCategory
    {
        public Guid ProductId { get; set; }
        public Product Product { get; set; }

        public Guid CategoryId { get; set; }
        public ProductCategory ProductCategory { get; set; }
    }
}
