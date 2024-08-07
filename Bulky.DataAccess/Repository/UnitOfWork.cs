using Bulky.DataAccess.Data;
using Bulky.DataAccess.Repository.IRepository;

namespace Bulky.DataAccess.Repository;

public class UnitOfWork : IUnitOfWork
{
    public ICategoryRepository _categoryRepository { get; private set; }
    public IProductRepository _productRepository { get; private set; }
    
    private readonly ApplicationDbContext _db;
    public UnitOfWork(ApplicationDbContext db)
    {
        _db = db;
        _categoryRepository = new CategoryRepository(_db);
        _productRepository = new ProductRepository(_db);
    }
    
    public void Save()
    {
        _db.SaveChanges();
    }
}