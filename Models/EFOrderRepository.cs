using Microsoft.EntityFrameworkCore;

namespace SportStore.Models
{
    public class EFOrderRepository : IOrderRepository
    {
        private StoreDbContext _context;
        public EFOrderRepository(StoreDbContext dbContext)
        {
            _context = dbContext;
        }
        public IQueryable<Order> Orders => _context.Orders
                                        .Include(order => order.Lines)
                                        .ThenInclude(lines => lines.Product);
        public void SaveOrder (Order order)
        {
            _context.AttachRange(order.Lines.Select(lines => lines.Product)); // AttachRange - is used to prevent from double writing data ( products ) 
            if ( order.OrderID == 0 )                                         // it only updates objects if they were modified ;
            {
                _context.Orders.Add(order);
            }
            _context.SaveChanges();
        }
    }
}
