using FinalProjectCodingIDBE.Models;
using FinalProjectCodingIDBE.Repositories;

namespace FinalProjectCodingIDBE.Services
{
    public class CategoryService
    {
        private readonly CategoryRepository _CategorysRepository;
        public CategoryService(CategoryRepository CategoryRepository) {
            _CategorysRepository = CategoryRepository;
        }
        public List<Category> GetCategories()
        {
            return _CategorysRepository.GetCategories();
        }
    }
}
