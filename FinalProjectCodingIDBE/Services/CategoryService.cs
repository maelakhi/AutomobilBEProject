using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using FinalProjectCodingIDBE.DTOs.CategoryDTO;
using FinalProjectCodingIDBE.Models;
using FinalProjectCodingIDBE.Repositories;

namespace FinalProjectCodingIDBE.Services
{
    public class CategoryService
    {
        private readonly CategoryRepository _CategorysRepository;
        public CategoryService(CategoryRepository CategoryRepository)
        {
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
        public string CategoryCreate(AddCategoryDTO categorysDTO)
        {
            string category = _CategorysRepository.CreateCategory(categorysDTO);
            return category;
        }
        public string CategoryUpdate(int Id, AddCategoryDTO categorysDTO)
        {
            string category = _CategorysRepository.UpdateCategory(Id, categorysDTO);
            return category;
        }
        public string CategoryDelete(int Id)
        {
            string category = _CategorysRepository.DeleteCategory(Id);
            return category;
        }
    }
}
