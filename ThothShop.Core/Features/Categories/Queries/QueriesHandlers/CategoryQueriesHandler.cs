using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ThothShop.Core.Bases;
using ThothShop.Core.Features.Categories.Queries.Models;
using ThothShop.Core.Features.Categories.Queries.Responses;
using ThothShop.Domain.Helpers;
using ThothShop.Service.Contract;

namespace ThothShop.Core.Features.Categories.Queries.QueriesHandlers
{
    public class CategoryQueriesHandler :ResponseHandler
                                        , IRequestHandler<GetCategoryByIdQueryModel, Response<CategoryResponse>>
                                        , IRequestHandler<GetAllCategoriesQueryModel,  Response<IEnumerable<CategoryResponse>>>
                                        , IRequestHandler<GetPopularCategoriesQueryModel, Response<IEnumerable<CategoryResponse>>>
                                        , IRequestHandler<GetCategoryBynameQueryModel,Response<CategoryResponse>>
                                        , IRequestHandler<GetCountOFBooksInCategoryQueryModel,Response<int>>
    {
        #region Field(s)
            private readonly ICategoryService _categoryService;
            private readonly IMapper _mapper;
        #endregion

        #region Constructor(s)
        public CategoryQueriesHandler(ICategoryService categoryService, IMapper mapper)
        {
            _categoryService = categoryService;
            _mapper = mapper;
        }


        #endregion

        #region HandleMethod(s)
        public async Task<Response<CategoryResponse>> Handle(GetCategoryByIdQueryModel request, CancellationToken cancellationToken)
        {
           var ExistingCategory = await _categoryService.GetCategoryByIdAsync(request.Id);
            if (ExistingCategory is null)
              NotFound<CategoryResponse>(string.Format(SystemMessages.NotFound,"Category"));
            var categoryResponse = _mapper.Map<CategoryResponse>(ExistingCategory);
            return Success<CategoryResponse>(categoryResponse);
        }

        public async Task<Response<IEnumerable<CategoryResponse>>> Handle(GetAllCategoriesQueryModel request, CancellationToken cancellationToken)
        {
            var ExistingCategories = await _categoryService.GetAllCategoriesAsync();
            if (ExistingCategories is null)
                NotFound<IEnumerable<CategoryResponse>>(string.Format(SystemMessages.NotFound, "Category"));
            var categoriesResponse = _mapper.Map<IEnumerable<CategoryResponse>>(ExistingCategories);
            return Success<IEnumerable<CategoryResponse>>(categoriesResponse);
        }

        public async Task<Response<IEnumerable<CategoryResponse>>> Handle(GetPopularCategoriesQueryModel request, CancellationToken cancellationToken)
        {
            var ExistingCategories = await _categoryService.GetPopularCategoriesAsync();
            if (ExistingCategories is null)
                NotFound<IEnumerable<CategoryResponse>>(string.Format(SystemMessages.NotFound, "Category"));
            var categoriesResponse = _mapper.Map<IEnumerable<CategoryResponse>>(ExistingCategories);
            return Success<IEnumerable<CategoryResponse>>(categoriesResponse);
        }

        public async Task<Response<CategoryResponse>> Handle(GetCategoryBynameQueryModel request, CancellationToken cancellationToken)
        {
            var ExistingCategory = await _categoryService.GetCategoryByNameAsync(request.Name);
            if (ExistingCategory is null)
                NotFound<CategoryResponse>(string.Format(SystemMessages.NotFound, "Category"));
            var categoryResponse = _mapper.Map<CategoryResponse>(ExistingCategory);
            return Success<CategoryResponse>(categoryResponse);
        }

        public async Task<Response<int>> Handle(GetCountOFBooksInCategoryQueryModel request, CancellationToken cancellationToken)
        {
             var count = await _categoryService.GetNumberOfBooksInCategory(request.Id);
             return Success<int>(count);
        }
        #endregion
    }
}
