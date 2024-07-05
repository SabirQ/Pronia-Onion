using Microsoft.EntityFrameworkCore;
using ProniaOnion104.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProniaOnion104.Persistence.Common
{
    internal static class GlobalQueryFilter
    {
        public static void ApplyQuery<T>(ModelBuilder builder) where T : BaseEntity, new()
        {
            builder.Entity<T>().HasQueryFilter(x => x.IsDeleted == false);
        }

        public static void ApplyQueryFilters(this ModelBuilder builder)
        {
            ApplyQuery<Category>(builder);
            ApplyQuery<Product>(builder);
            ApplyQuery<Color>(builder);
            ApplyQuery<ProductColor>(builder);
        }
    }
}
