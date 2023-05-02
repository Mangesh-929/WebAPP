using Product.Data.Models.Domain;

namespace Product.Data.Repository
{
    public interface IProductRepository
    {
        Task<bool> AddAsync(ProductM product);
        Task<bool> UpdateAsync(ProductM product);
        Task<bool> DeleteAsync(int id);
        Task<ProductM> GetByIdAsync(int id);
        Task<IEnumerable<ProductM>> GetAllAsync();

    }
}