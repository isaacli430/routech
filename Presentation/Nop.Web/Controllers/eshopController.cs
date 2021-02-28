using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Nop.Services.Catalog;
using Nop.Core.Domain.Catalog;
using Nop.Core.Domain.Seo;
using Nop.Services.Seo;

namespace Nop.Web.Controllers
{
    public class eshopController : Controller
    {
        private readonly ICategoryService _categoryService;
        private readonly IUrlRecordService _urlRecordService;
        public eshopController(
            ICategoryService categoryService,
            IUrlRecordService urlRecordService)
        {
            _categoryService = categoryService;
            _urlRecordService = urlRecordService;
        }
        public IActionResult Index(string? group)
        {
            if (string.IsNullOrEmpty(group))
            {
                List<Category> model = new List<Category>();
                foreach (Category category in _categoryService.GetAllCategories())
                {
                    if (_categoryService.GetAllCategoriesByParentCategoryId(category.Id).Count > 0)
                    {
                        model.Add(category);
                    }
                }
                return View(model);
            }
            UrlRecord record = _urlRecordService.GetBySlug(group);
            if (record == null)
            {
                return View(null);
            }
            return View(new List<Category>(_categoryService.GetAllCategoriesByParentCategoryId(record.EntityId)));
        }
    }
}
