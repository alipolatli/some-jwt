using AutoMapper;
using Shared.Dtos;
using Shared.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WithJwt.Core.Dtos.Posts;
using WithJwt.Core.Entites;
using WithJwt.Core.Repositories;
using WithJwt.Core.Services;
using WithJwt.Core.UnitOfWork;

namespace WithJwt.Service.Services
{
    public class PostService : IPostService
    {
        private readonly IPostRepository _postRepository;
        private readonly IMapper _mapper;
        private readonly IUnitofWork _unitofWork;

        public PostService(IMapper mapper, IPostRepository postRepository, IUnitofWork unitofWork)
        {
            _mapper = mapper;
            _postRepository = postRepository;
            _unitofWork = unitofWork;
        }

        public async Task<ResponseResult<PostDto>> AddAsync(PostAddDto postAddDto)
        {
            var addedPost = _mapper.Map<Post>(postAddDto);
            addedPost.CreatedDate = DateTime.Now;
            addedPost.UpdatedDate = DateTime.Now;
            await _postRepository.AddAsync(addedPost);
            await _unitofWork.CommitAsync();
            return ResponseResult<PostDto>.Success(PostConvertPostDtoMapper(addedPost), 201);
            //devam...
        }

        public async Task<ResponseResult<PostListDto>> GetAllAsync()
        {
            var posts = await _postRepository.GetAllAsync();
            var postsDto = _mapper.Map<IEnumerable<PostDto>>(posts);
            return ResponseResult<PostListDto>.Success(new PostListDto { PostDtos = postsDto }, 200);
            // ResponseResult<IEnumerable<PostDto>>.Success(postsDto,200); //DENE
        }

        public async Task<ResponseResult<PostDto>> GetByIdAsync(int postId)
        {
            var post = await _postRepository.GetByIdAsync(p => p.Id == postId);
            return post != null ? ResponseResult<PostDto>.Success(PostConvertPostDtoMapper(post), 200) : ResponseResult<PostDto>.Fail("Not found.", 404, true);
        }

        public async Task<ResponseResult<NoDataResult>> HardRemoveAsync(int postId)
        {
            var deletedPost = await _postRepository.GetByIdAsync(p => p.Id == postId);
            if (deletedPost != null)
            {
                _postRepository.Remove(deletedPost);
                await _unitofWork.CommitAsync();
                return ResponseResult<NoDataResult>.Success(204);
            }
            return ResponseResult<NoDataResult>.Fail("Not found", 400, true);
        }

        public async Task<ResponseResult<PostDto>> UpdateAsync(PostUpdateDto postUpdateDto)
        {
            var oldPost = await _postRepository.GetByIdAsync(p => p.Id == postUpdateDto.Id);
            var updatedPost = _postRepository.Update(_mapper.Map<PostUpdateDto, Post>(postUpdateDto, oldPost));
            await _unitofWork.CommitAsync();
            return ResponseResult<PostDto>.Success(PostConvertPostDtoMapper(updatedPost), 200);
        }


        public async Task<ResponseResult<PostListDto>> GetAllByPagination(int currentPage, int pageSize)
        {
            var postsFullCount = await _postRepository.CountAsync();

            var pagingPosts = _postRepository.GetAllAsyncV2().OrderBy(p => p.CreatedDate).Skip((currentPage - 1) * pageSize).Take(10).AsEnumerable();

            var pagingPostsDto = _mapper.Map<IEnumerable<PostDto>>(pagingPosts);

            return ResponseResult<PostListDto>.Success(new PostListDto
            {
                PostDtos = pagingPostsDto,
                CurrentPage = currentPage,
                PageSize = pageSize,
                TotalCount = postsFullCount,
            }, 200);

        }


        Task<ResponseResult<NoDataResult>> IPostService.RemoveAsync(int postId)
        {
            throw new NotImplementedException();
        }

        public PostDto PostConvertPostDtoMapper(Post post)
          => _mapper.Map<PostDto>(post);
    }
}
