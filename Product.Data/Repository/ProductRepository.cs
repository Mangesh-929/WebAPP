using Product.Data.DataAccess;
using Product.Data.Models.Domain;

namespace Product.Data.Repository
{
    public class ProductRepository: IProductRepository

    {
        private readonly ISqlDataAccess _db;
        public ProductRepository(ISqlDataAccess db)
        {
            _db = db;
        }
        public async Task<bool> AddAsync(ProductM product)
        {
            try
            {
                await _db.SaveData("sp_add_product", new { product.Name, product.Price, product.Description });
                return true;
            }
            catch(Exception ex)
            {
                return false;
            }

        }

        public async Task<bool> UpdateAsync(ProductM product)
        {
            try
            {
                await _db.SaveData("sp_update_product", product);
                return true;
            }
            catch(Exception ex)
            {
                return false;
            }

        }

        public async Task<bool> DeleteAsync(int id)
        {
            try
            {
                await _db.SaveData("sp_delete_product", new {Id = id});
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }

        }
        public async Task<ProductM?> GetByIdAsync(int id)
        {
            IEnumerable<ProductM> result = await _db.GetData<ProductM, dynamic>
                ("sp_get_product", new { Id = id });
            return result.FirstOrDefault();
        }

        public async Task<IEnumerable<ProductM>> GetAllAsync()
        {
            string query = "sp_get_products";
            return await _db.GetData<ProductM, dynamic>(query, new { });
        }


    }
}
