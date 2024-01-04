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
        public string CategoryCreate(AddCategoryDTO categorysDTO, string fileUrlPath)
        {
            string category = _CategorysRepository.CreateCategory(categorysDTO, fileUrlPath);
            return category;
        }
        public string CategoryUpdate(int Id, EditCategoryDTO categorysDTO, string fileUrlPath)
        {
            string category = _CategorysRepository.UpdateCategory(Id, categorysDTO, fileUrlPath);
            return category;
        }
        public string CategoryDelete(int Id)
        {
            string category = _CategorysRepository.DeleteCategory(Id);
            return category;
        }

        public List<Category> GetCategoryLimit()
        {
            return _CategorysRepository.GetCategoryLimit();
        }

        public string CategoryStatus(int Id, bool Status)
        {
            string category = _CategorysRepository.UpdateStatusCategory(Id, Status);
            return category;
        }

    }
}
