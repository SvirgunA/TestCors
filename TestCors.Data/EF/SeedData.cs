using System;
using System.Collections.Generic;
using System.Linq;
using TestCors.Models;

namespace TestCors.Data.EF
{
    public static class SeedData
    {
        public static void SeedPhonesData(this TestCorsContext context)
        {
            var phones = new List<Phone>()
            {
                new Phone{Id = 1, Name = "Samsung" },
                new Phone{Id = 2, Name = "Nokia" },
                new Phone{Id = 3, Name = "Xiaomi" }
            };
            var ids = phones.Select(p => p.Id);
            var existing = context.Phones.Where(p => ids.Contains(p.Id)).Select(p => p.Id);
            //context.Phones.AddRange(phones.Where(p => !existing.Contains(p.Id)));
            //context.SaveChanges();
        }
    }
}
