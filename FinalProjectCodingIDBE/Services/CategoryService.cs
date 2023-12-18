using FinalProjectCodingIDBE.DTOs.CategoryDTO;
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

        public Category GetByIdCategory(int Id)
        {
            Category category = _CategorysRepository.GetCategoryById(Id);
            return category;
        }
        public Category CategoryCreate(AddCategoryDTO categorysDTO)
        {
            Category category = _CategorysRepository.CreateCategory(categorysDTO);
            return category;
        }
        public Category CategoryUpdate(int Id, AddCategoryDTO categorysDTO)
        {
            Category category = _CategorysRepository.UpdateCategory(Id, categorysDTO);
            return category;
        }
        public bool CategoryDelete(int Id)
        {
            bool flagDelete = _CategorysRepository.DeleteCategory(Id);
            return flagDelete;
        }

        /*Landing Page*/
        public List<Category> GetLimitCategory()
        {
            List<Category> category = _CategorysRepository.GetCategoryLimit();
            return category;
        }
    }
}
