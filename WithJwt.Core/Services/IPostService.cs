using Shared.Dtos;
using Shared.Results;
using WithJwt.Core.Dtos.Posts;

namespace WithJwt.Core.Services
{
    public interface IPostService
    {
        Task<ResponseResult<PostListDto>> GetAllAsync();

        Task<ResponseResult<PostDto>> GetByIdAsync(int postId);

        Task<ResponseResult<PostDto>> AddAsync(PostAddDto postAddDto);

        Task<ResponseResult<PostDto>> UpdateAsync(PostUpdateDto postUpdateDto);

        Task<ResponseResult<NoDataResult>> RemoveAsync(int postId);

        Task<ResponseResult<NoDataResult>> HardRemoveAsync(int postId);

        Task<ResponseResult<PostListDto>> GetAllByPagination(int currentPage, int pageSize);

    }
}
