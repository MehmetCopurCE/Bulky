namespace Bulky.DataAccess.Repository.IRepository;

public interface IUnitOfWork
{
    ICategoryRepository _categoryRepository { get; }
    IProductRepository _productRepository { get; }
    void Save();
}