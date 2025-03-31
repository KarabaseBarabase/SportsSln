using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace SportsStore.Models
{
    public class EFOrderRepository : IOrderRepository
    {
        private StoreDbContext context;
        public EFOrderRepository(StoreDbContext ctx)
        {
            context = ctx;
        }
        public IQueryable<Order> Orders => context.Orders //EFC требует инструкции относительно загрузки связанных данных, если они охватывают несколько таблиц:
            .Include(o => o.Lines) // при загрузке объекта Order нужно также загрузить связанные с ним объекты Line
            .ThenInclude(l => l.Product); // также загрузить связанные объекты Product для каждой Line
        public void SaveOrder(Order order)
        {
            //Уведомляем EFC о том, что объекты существуют и не должны сохраняться в бд до тех пор, пока не будут модифицированы
            context.AttachRange(order.Lines.Select(l => l.Product)); 
            if (order.OrderID == 0)
            {
                context.Orders.Add(order);
            }
            context.SaveChanges();
        }
    }
}
