using maealim.Models;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace maealim.Data
{
    public static class SeedData
    {
        public static async Task Initialize(ApplicationDbContext context )
        {
            context.Database.EnsureCreated();
            if (!context.Seasons.Any())
            {
                context.Seasons.Add(new Season() { Name = "الحج"});
                context.Seasons.Add(new Season() { Name = "العمرة"});
               await context.SaveChangesAsync();
            }

            if (!context.Jops.Any())
            {
                context.Jops.Add(new Jop() { Name = "مسؤول الحركة" });
                context.Jops.Add(new Jop() { Name = "منسق الوجهاء" });
                context.Jops.Add(new Jop() { Name = "مشرف قباء" });
                context.Jops.Add(new Jop() { Name = "مشرف أحد" });
                context.Jops.Add(new Jop() { Name = "متعاون" });
                await context.SaveChangesAsync();
            }

            if (!context.Continents.Any())
            {
                context.Continents.Add(new Continent() { Name = "آسيا" });
                context.Continents.Add(new Continent() { Name = "افريقيا" });
                context.Continents.Add(new Continent() { Name = "أوربا" });
                context.Continents.Add(new Continent() { Name = "أمريكا الشمالية" });
                context.Continents.Add(new Continent() { Name = "أمريكا الجنوبية" });
                context.Continents.Add(new Continent() { Name = " أستراليا" });
                context.Continents.Add(new Continent() { Name = " أنتاركتيكا" });
                await context.SaveChangesAsync();
            }




        }
    }
}