using CFS.Domain.Entities;
using CFS.Infrastructure.Models;

namespace CFS.Infrastructure.Maps;

public static class ProductMap
{
    extension (Product product) {
        public ProductBson? MapToBson()
        {
            if (product == null) return null;

            return new ProductBson
            {
                Id = product.Id,
                Name = product.Name,
                Description = product.Description,
                Price = product.Price,
                Parameters = product.Parameters
            };
        }
    }

    extension (ProductBson productBson)
    {
        public Product? MapToDomain()
        {
            if (productBson == null) return null;

            return new Product
            {
                Id = productBson.Id,
                Name = productBson.Name,
                Description = productBson.Description,
                Price = productBson.Price,
                Parameters = productBson.Parameters
            };
        }
    }
}
