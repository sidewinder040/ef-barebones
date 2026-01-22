using Microsoft.EntityFrameworkCore;


internal class Program
{
    private static void Main(string[] args)
    {
        using (var context = new MyDbContext())
        {
            // Create
            var purchase = new Purchase { Product = "Laptop", Price = 999.99M };
            context.Purchases.Add(purchase);
            context.SaveChanges();

            // Read
            var savedPurchase = context.Purchases.FirstOrDefault(p => p.Id == purchase.Id);
            Console.WriteLine($"Saved Purchase: {savedPurchase?.Product}, Price: {savedPurchase?.Price}");

            // Update
            if (savedPurchase != null)
            {
                savedPurchase.Price = 899.99M;
                context.SaveChanges();
                Console.WriteLine($"Updated Purchase Price: {savedPurchase.Price}");
            }

            // Delete
            if (savedPurchase != null)
            {
                context.Purchases.Remove(savedPurchase);
                context.SaveChanges();
                Console.WriteLine("Purchase deleted.");
            }
        }
    }
}

public class MyDbContext : DbContext
{
    public DbSet<Purchase> Purchases { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseInMemoryDatabase("MyInMemoryDb");
    }
}

public class Purchase
{
    public int Id { get; set; }    
    public string? Product { get; set; }
    public decimal Price { get; set; }
}



