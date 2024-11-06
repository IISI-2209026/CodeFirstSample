// See https://aka.ms/new-console-template for more information

using CodeFirstSample;
using Microsoft.EntityFrameworkCore;

Console.WriteLine("Hello, World!");

var options = new DbContextOptionsBuilder()
    .UseNpgsql("Host=localhost;Database=CodeFirstSample;Username=postgres;Password=postgres")
    .LogTo(Console.WriteLine)
    .Options;
var dbContext = new MyDbContext(options);

dbContext.Persons
    .Add(new Person()
    {
        Name = "John",
        BirthDate = DateTime.Now
    });
    
await dbContext.SaveChangesAsync();

dbContext.Persons
    .ToList()
    .ForEach(p => Console.WriteLine($"Id: {p.Id}, Name: {p.Name}, BirthDate: {p.BirthDate}"));