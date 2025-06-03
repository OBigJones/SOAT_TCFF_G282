using Application.Repository;
using Domain.Entities;
using Domain.Enums;
using Microsoft.EntityFrameworkCore;

namespace Infra.Data
{
    internal class ProductRepository : RepositoryBase<UserEntity>, IProductRepository
    {
        private readonly AppDbContext _context;

        public ProductRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<bool> CreateProductAsync(ProductEntity product)
        {
            try
            {
                await _context.Products.AddAsync(product);
                await _context.SaveChangesAsync();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<List<ProductEntity>> GetAllProductsAsync()
        {
            return await _context.Products.ToListAsync();
        }

        public async Task<ProductEntity?> GetProductByIdAsync(long id)
        {
            return await _context.Products.FindAsync(id);
        }

        public async Task<bool> UpdateProductAsync(ProductEntity product)
        {
            try
            {
                _context.Products.Update(product);
                await _context.SaveChangesAsync();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<bool> DeleteProductAsync(long id)
        {
            var product = await GetProductByIdAsync(id);
            if (product == null) return false;
            _context.Products.Remove(product);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<List<ProductEntity>> GetProductsInStockAsync()
        {
            return await _context.Products
                                 .Where(p => p.Quantity > 0)
                                 .ToListAsync();
        }

        public async Task<List<ProductEntity>> GetProductsInStockByTypeAsync(ProductType productType)
        {
            return await _context.Products
                                 .Where(p => p.Quantity > 0 && p.Type == productType)
                                 .ToListAsync();
        }

        public async Task<bool> DecrementStockAsync(List<ProductEntity> productsToDecrement)
        {
            using (var transaction = await _context.Database.BeginTransactionAsync())
            {
                try
                {
                    foreach (var productInList in productsToDecrement)
                    {
                        var existingProduct = await _context.Products
                                                            .AsNoTracking()
                                                            .FirstOrDefaultAsync(p => p.Id == productInList.Id);

                        if (existingProduct == null)
                        {
                            await transaction.RollbackAsync();
                            Console.WriteLine($"Erro: Produto com Id {productInList.Id} não encontrado no estoque.");
                            return false;
                        }

                        if (existingProduct.Quantity < productInList.Quantity)
                        {
                            await transaction.RollbackAsync();
                            Console.WriteLine($"Erro: Estoque insuficiente para o produto '{existingProduct.Name}' (Id: {existingProduct.Id}). Necessário: {productInList.Quantity}, Disponível: {existingProduct.Quantity}");
                            return false;
                        }

                        existingProduct.Quantity -= productInList.Quantity;
                        _context.Products.Update(existingProduct);
                    }

                    await _context.SaveChangesAsync();

                    await transaction.CommitAsync();
                    return true;
                }
                catch (DbUpdateConcurrencyException ex)
                {
                    await transaction.RollbackAsync();
                    Console.WriteLine($"Erro de concorrência ao atualizar estoque: {ex.Message}");
                    return false;
                }
                catch (Exception ex) // Captura qualquer outra exceção genérica.
                {
                    await transaction.RollbackAsync();
                    Console.WriteLine($"Erro inesperado ao decrementar estoque: {ex.Message}");
                    return false;
                }
            }
        }
    }
}
