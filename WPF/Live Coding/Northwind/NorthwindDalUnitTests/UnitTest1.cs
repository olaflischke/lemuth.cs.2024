using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using NorthwindDal.Model;

namespace NorthwindDalUnitTests
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void KannCustomersLesen()
        {
            NorthwindContext context = new NorthwindContext();
            var customers = context.Customers.Select(cu => new {cu.CompanyName, cu.Country}).ToList();

            foreach (var item in customers)
            {
                Console.WriteLine($"{item.CompanyName}, {item.Country}");
            }

            Assert.NotNull(customers);
        }

        [Test]
        public void KannKundeAendern()
        {
            NorthwindContext context1 = GetContext();

            Customer? alfki = context1.Customers.Find("ALFKI");  // context1.Customers.Where(cu => cu.CustomerId=="ALFKI").Single();

            alfki.ContactName = "Maria Maier";

            context1.SaveChanges();

            Assert.Pass();
        }

        [Test]
        public void KannBestellungenListen()
        {
            NorthwindContext context1 = GetContext();

            Customer? alfki = context1.Customers.Find("ALFKI");  // context1.Customers.Where(cu => cu.CustomerId=="ALFKI").Single();

            var qOrderInfo = context1.Orders
                                        .Include(od => od.OrderDetails)
                                        .ThenInclude(od => od.Product)
                                            .Where(od => od.CustomerId == "ALFKI")
                                                .Select(od => new { od.Id, od.OrderDetails });

            foreach (var item in qOrderInfo)
            {
                Console.WriteLine($"{item.Id}");
                foreach (var detail in item.OrderDetails)
                {
                    Console.WriteLine($"{detail.ProductId}: {detail.Product.ProductName}");
                }
            }

            Assert.Pass();
        }

        private NorthwindContext GetContext()
        {
            DbContextOptionsBuilder<NorthwindContext> builder = new DbContextOptionsBuilder<NorthwindContext>()
                                                            .UseSqlServer("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=northwind;Integrated Security=True;Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False").LogTo(log => Console.WriteLine(log), Microsoft.Extensions.Logging.LogLevel.Information);

            return new NorthwindContext(builder.Options);

        }
    }
}