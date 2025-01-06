using MediatR;
using Words.API.Models;
using Words.Domain.Entities;
using Words.Infrastructure.Repositories;

namespace Words.API.Features.Queries.GetCategoriesDict;

public class GetCategoriesQuery : IRequest<IReadOnlyList<CategoryDto>>
{
    public bool IsActive { get; }

    public GetCategoriesQuery(bool isActive)
    {
        IsActive = isActive;
    }
}

public class GetCategoriesHandler(ICategoryRepository categoryRepository) : IRequestHandler<GetCategoriesQuery, IReadOnlyList<CategoryDto>>
{
    public async Task<IReadOnlyList<CategoryDto>> Handle(GetCategoriesQuery request, CancellationToken cancellationToken)
    {
        var result = request.IsActive 
            ? await categoryRepository.GetOnlyActiveCategories() 
            : await categoryRepository.GetCategories();

        return result
            ?.Select(x => Map(x))
            .ToList();
    }

    private static CategoryDto Map(Category category) =>
        new CategoryDto()
        {
            CategoryId = category.Id.ToString(),
            CategoryDescription = category.Description,
            CategoryName = category.Name,
        };
}
