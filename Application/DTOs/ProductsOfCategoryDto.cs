using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs
{
    public class ProductsOfCategoryDto
    {
        public string CategoryName { get; set; }
        public IEnumerable<ProductDto> Products { get; set; }
    }
}
