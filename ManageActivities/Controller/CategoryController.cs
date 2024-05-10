using ManageActivities.Dto.CategoryDto;
using ManageActivities.Entity.Model;
using ManageActivities.Repository.Interface;
using ManageActivities.Utils;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;

namespace ManageActivities.Controller;

[ApiController]
[Authorize]
[Route("/category")]
public class CategoryController(
    IUserRepository userRepository,
    ICategoryRepository categoryRepository,
    IMemoryCache memoryCache)
    : AuthenticatedControllerBase(userRepository)
{
    private const string CategoriesCacheKey = "categoriesdtos";
    private const string CacheKeyForExpanded = "expandedCategoriesdtos";

    [HttpGet]
    public async Task<ActionResult<List<CategoryDto>>> GetCategories([FromQuery] bool expand = false)
    {
        Guid userId = await VerifyUser();
        if (userId == Guid.Empty)
        {
            return Unauthorized();
        }

        if (expand)
        {
            IEnumerable<ExpandedCategoryDto>? cachedExpandedCategoriesDtos =
                memoryCache.Get<IEnumerable<ExpandedCategoryDto>>(CacheKeyForExpanded + userId);
            if (cachedExpandedCategoriesDtos is not null)
            {
                return Ok(cachedExpandedCategoriesDtos);
            }

            List<Category> expandedCategories = await categoryRepository.GetExpandedCategoriesByUserId(userId);
            List<ExpandedCategoryDto> expandedCategoryDtos =
                expandedCategories.Select(category => category.ToExpandedCategoryDto()).ToList();
            memoryCache.Set(CacheKeyForExpanded + userId, expandedCategoryDtos);
            return Ok(expandedCategoryDtos);
        }

        IEnumerable<CategoryDto>? cachedCategoryDtos =
            memoryCache.Get<IEnumerable<CategoryDto>>(CategoriesCacheKey + userId);
        if (cachedCategoryDtos is not null)
        {
            return Ok(cachedCategoryDtos);
        }

        List<Category> categories = await categoryRepository.GetCategoriesByUserId(userId);
        List<CategoryDto> categoryDtos = categories.Select(category => category.ToDto()).ToList();
        memoryCache.Set(CategoriesCacheKey + userId, categoryDtos);
        return Ok(categoryDtos);
    }

    [HttpPost]
    public async Task<ActionResult> CreateCategory([FromBody] CreateCategoryDto createCategoryDto)
    {
        Guid userId = await VerifyUser();
        if (userId == Guid.Empty)
        {
            return Unauthorized();
        }

        Category category = new Category
        {
            Id = Guid.NewGuid(),
            Name = createCategoryDto.Name,
            Created = DateTime.Now,
            Updated = DateTime.Now,
            UserId = userId
        };
        await categoryRepository.CreateEntityAsync(category);
        memoryCache.Remove(CacheKeyForExpanded + userId);
        memoryCache.Remove(CategoriesCacheKey + userId);
        return Created();
    }
}