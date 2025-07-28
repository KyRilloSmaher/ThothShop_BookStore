using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ThothShop.Core.Bases;
using ThothShop.Core.Features.Categories.Commands.Models;
using ThothShop.Domain.Helpers;
using ThothShop.Domain.Models;
using ThothShop.Service.Contract;

namespace ThothShop.Core.Features.Categories.Commands.CommandsHandlers
{
    public class CategoryCommandsHandler : ResponseHandler
                                            , IRequestHandler<CreateCategoryCommandModel, Response<string>>
                                            , IRequestHandler<UpdateCategoryCommandModel, Response<string>>
                                            , IRequestHandler<DeleteCategoryCommandModel, Response<string>>
                                            , IRequestHandler<AddAuthorToCategoriesCommandModel, Response<string>>
    {
        #region Field(s)
        private readonly ICategoryService _categoryService;
        private readonly IMapper _mapper;
        #endregion

        #region Constructor(s)
        public CategoryCommandsHandler(ICategoryService categoryService, IMapper mapper)
        {
            _categoryService = categoryService ?? throw new ArgumentNullException(nameof(categoryService));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }
        #endregion

        #region HandleMethod(s)
        public async Task<Response<string>> Handle(CreateCategoryCommandModel request, CancellationToken cancellationToken)
        {
            var category = _mapper.Map<Category>(request);
            var result = await _categoryService.CreateCategoryAsync(category,request.Icon);
            if (result == null)
            {
                return Failed<string>(string.Format(SystemMessages.FailedToAddEntity,"Category"));
            }
            return Created(SystemMessages.Success);
        }

        public async Task<Response<string>> Handle(UpdateCategoryCommandModel request, CancellationToken cancellationToken)
        {
            var ExistingCategory = await _categoryService.GetCategoryAsTrackedByIdAsync(request.Id);
            if (ExistingCategory == null)
            {
                return Failed<string>(string.Format(SystemMessages.EntityNotFound, "Category"));
            }
            _mapper.Map(request, ExistingCategory);
            await _categoryService.UpdateCategoryAsync(ExistingCategory ,request.Icon);
            return Success(string.Format(SystemMessages.UpdatedSuccessfully, "Category"));
        }

        public async Task<Response<string>> Handle(DeleteCategoryCommandModel request, CancellationToken cancellationToken)
        {
            var result = await _categoryService.DeleteCategoryAsync(request.Id); ;
            if (!result)
            {
                return Failed<string>(string.Format(SystemMessages.EntityNotFound, "Category"));
            }
            return Success(string.Format(SystemMessages.DeletedSuccessfully, "Category"));

        }

        public async Task<Response<string>> Handle(AddAuthorToCategoriesCommandModel request, CancellationToken cancellationToken)
        {
            var result = await _categoryService.AddAuthorToCategoriesAsync(request.AuthorId, request.CategoryIds);
            if (!result)
            {
                return Failed<string>(string.Format(SystemMessages.FailedToAddEntity, "Author"));
            }
            return Created(SystemMessages.Success);

        }
        #endregion
    }
}
