using Cafe.Domain.Entities;
using Cafe.Domain.ValueObjects;
using Cafe.Persistance.EFCustomizations;
using Microsoft.EntityFrameworkCore;

namespace Cafe.Persistance
{
    public static class DataSeeder
    {
        public static void SeedData(this CafeDbContext dbContext)
        {
            if (dbContext.Cafes.Any() || dbContext.Employees.Any())
            {
                return;
            }
            var starbucksKinex = Domain.Entities.Cafe.CreateCafe(
                "Starbucks KINEX",
                "11 Tanjong Katong Rd, #02-K1, Singapore 437157",
                "KINEX");

            var coffeeBean = Domain.Entities.Cafe.CreateCafe(
                "Coffee Bean NEX",
                "23 Serangoon Central, #02-K10 NEX, Singapore 556083",
                "Marina Bay Sands");
            var starbucksReserve = Domain.Entities.Cafe.CreateCafe(
                "Starbucks Reserve",
                "2 Bayfront Avenue B2-56 The Shoppes at Marina Bay Sands, Singapore 018972",
                "KINEX");


            var david = Employee.CreateEmployee(
                "UIADT1233",
                "David",
                email: Email.Create("david@yahoo.com").Value,
                PhoneNumber.Create("91235536").Value,
                Gender.Create("Male").Value);
            var kelly = Employee.CreateEmployee(
                "UIADT1232",
                "Kelly",
                email: Email.Create("Kelly@yahoo.com").Value,
                PhoneNumber.Create("91235556").Value,
                Gender.Create("Female").Value);
            var joseph = Employee.CreateEmployee(
                "UIADT1262",
                "Joseph",
                email: Email.Create("Joseph@yahoo.com").Value,
                PhoneNumber.Create("91235576").Value,
                Gender.Create("Male").Value,
                starbucksKinex);
            var ruth = Employee.CreateEmployee(
                "UIADTA262",
                "Ruth",
                email: Email.Create("Ruth@yahoo.com").Value,
                PhoneNumber.Create("91235276").Value,
                Gender.Create("Female").Value,
                coffeeBean);
            var emily = Employee.CreateEmployee(
                "UIAFT1262",
                "Emily",
                email: Email.Create("emily@yahoo.com").Value,
                PhoneNumber.Create("94235276").Value,
                Gender.Create("Female").Value,
                starbucksKinex);

            dbContext.Cafes.AddRange(starbucksKinex, coffeeBean, starbucksReserve);
            dbContext.Employees.AddRange(david, kelly, joseph, ruth, emily);
            dbContext.SaveChanges();
        }
    }
}
