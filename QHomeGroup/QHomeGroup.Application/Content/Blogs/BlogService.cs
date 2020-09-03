using System;
using Microsoft.EntityFrameworkCore;
using QHomeGroup.Application.Content.Blogs.Dtos;
using QHomeGroup.Data.Entities.Content;
using QHomeGroup.Data.Enum;
using QHomeGroup.Infrastructure.Interfaces;
using QHomeGroup.Utilities.Dtos;
using QHomeGroup.Utilities.Extensions;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using QHomeGroup.Application.Content.Blogs.Request;
using QHomeGroup.Utilities.Constants;
using QHomeGroup.Utilities.Helpers;

namespace QHomeGroup.Application.Content.Blogs
{
    public class BlogService : IBlogService
    {
        private readonly IRepository<Blog, int> _blogRepository;
        private readonly IRepository<Tag, string> _tagRepository;
        private readonly IRepository<BlogTag, int> _blogTagRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IUnitOfWork _unitOfWork;

        public BlogService(IRepository<Blog, int> blogRepository,
            IRepository<Tag, string> tagRepository,
            IUnitOfWork unitOfWork, IRepository<BlogTag, int> blogTagRepository, IHttpContextAccessor httpContextAccessor)
        {
            _blogRepository = blogRepository;
            _tagRepository = tagRepository;
            _unitOfWork = unitOfWork;
            _blogTagRepository = blogTagRepository;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task Add(UpdateBlogRequest updateBlogRequest)
        {
            var userId = Guid.Parse(_httpContextAccessor.HttpContext.User?.FindFirst(SystemConstants.UserClaim.Id)?.Value);
            var blog = new Blog(updateBlogRequest.Name, updateBlogRequest.Thumbnail, updateBlogRequest.Description, updateBlogRequest.Block, updateBlogRequest.HotFlag, userId, updateBlogRequest.Status, updateBlogRequest.Tags, updateBlogRequest.SlideOption, updateBlogRequest.SlideVideos, updateBlogRequest.SlideImages);
            if (!string.IsNullOrEmpty(blog.Tags))
            {
                var tags = blog.Tags.Split(',');
                foreach (var t in tags)
                {
                    var tagId = TextHelper.ToUnsignString(t);
                    if (!await _tagRepository.ExistsAsync(x => x.Id == tagId))
                    {
                        Tag tag = new Tag
                        {
                            Id = tagId,
                            Name = t,
                            Type = CommonConstants.BlogTag
                        };
                        _tagRepository.Add(tag);
                    }

                    var blogTag = new BlogTag { TagId = tagId };
                    blog.BlogTags.Add(blogTag);
                }
            }
            _blogRepository.Add(blog);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task<bool> Delete(int id)
        {
            var blog = await _blogRepository.FindByIdAsync(id);
            if (blog != null)
            {
                _blogRepository.Remove(blog);
                return true;
            }
            return false;
        }

        public async Task<BlogDto> Get(int id)
        {
            var blog = await _blogRepository.FindByIdAsync(id);
            var blogResult = blog.To<BlogDto>();
            return blogResult;
        }

        public async Task<BlogDto> GetDetails(int id)
        {
            var blog = await _blogRepository.FindByIdAsync(id);
            var blogs = await _blogRepository.GetAllAsync();
            if (blog != null)
            {
                blogs.Remove(blog);
            }

            var data = new List<BlogDto>();
            if (blogs != null && blogs.Count > 0)
            {
                foreach (var item in blogs)
                {
                    data.Add(new BlogDto()
                    {
                        Name = item.Name,
                        LinkDetail = "tin-tuc." + item.SeoAlias + "." + item.Id + ".html"
                    });
                }
            }
            var blogResult = blog.To<BlogDto>();
            blogResult.BlogInterest = data;
            return blogResult;
        }

        public async Task<IList<BlogDto>> GetAll()
        {
            return (await _blogRepository.GetAllAsync())
                .To<IList<BlogDto>>();
        }

        public async Task<QueryResult<BlogDto>> GetAllPaging(string keyword, int skip, int take)
        {
            var queryResult = await _blogRepository.QueryAsync(t => string.IsNullOrEmpty(keyword) || EF.Functions.Like(t.Name, $"%{keyword}%"), skip, take);
            return queryResult.To<QueryResult<BlogDto>>();
        }

        public async Task<PagedResult<BlogDto>> GetAllPagingWebApp(string keyword, int page, int pageSize)
        {
            var listBlog = await _blogRepository.GetManyAsync(x => x.Status == Status.Active);
            if (!string.IsNullOrEmpty(keyword))
            {
                listBlog = listBlog.Where(x => x.Name.Contains(keyword)).ToList();
            }

            int totalRow = listBlog.Count;
            listBlog = listBlog.OrderBy(x => x.DateCreated).Skip((page - 1) * pageSize).Take(pageSize).ToList();
            var data = new List<BlogDto>();
            foreach (var item in listBlog)
            {
                data.Add(new BlogDto()
                {
                    Name = item.Name,
                    Description = item.Description,
                    DateCreated = item.DateCreated,
                    Status = item.Status,
                    Thumbnail = item.Thumbnail,
                    Block = item.Block,
                    CreatedBy = item.CreatedBy,
                    DateModified = item.DateModified,
                    HomeFlag = item.HomeFlag,
                    HotFlag = item.HotFlag,
                    Id = item.Id,
                    SlideImages = item.SlideImages,
                    SlideOption = item.SlideOption,
                    SlideVideos = item.SlideVideos,
                    SeoAlias = item.SeoAlias,
                    LinkDetail = "tin-tuc." + item.SeoAlias + "." + item.Id + ".html"
                });
            }
            var paginationSet = new PagedResult<BlogDto>()
            {
                Results = data,
                CurrentPage = page,
                RowCount = totalRow,
                PageSize = pageSize
            };
            return paginationSet;
        }


        public async Task Update(int id, UpdateBlogRequest updateBlogRequest)
        {
            var blog = await _blogRepository.FindByIdAsync(id);
            if (blog != null)
            {
                blog.Update(updateBlogRequest.Name, updateBlogRequest.Thumbnail, updateBlogRequest.Description, updateBlogRequest.Block, updateBlogRequest.HotFlag, updateBlogRequest.Status, updateBlogRequest.Tags, updateBlogRequest.SlideOption, updateBlogRequest.SlideVideos, updateBlogRequest.SlideImages);
                _blogRepository.Update(blog);
            }
            if (blog != null && !string.IsNullOrEmpty(blog.Tags))
            {
                var tags = blog.Tags.Split(',');
                foreach (var t in tags)
                {
                    var tagId = TextHelper.ToUnsignString(t);
                    if (!await _tagRepository.ExistsAsync(x => x.Id == tagId))
                    {
                        Tag tag = new Tag
                        {
                            Id = tagId,
                            Name = t,
                            Type = CommonConstants.ProductTag
                        };
                        _tagRepository.Add(tag);
                    }

                    _blogTagRepository.RemoveMultiple(await _blogTagRepository.GetManyAsync(x => x.Id == blog.Id));
                    BlogTag blogTag = new BlogTag
                    {
                        BlogId = blog.Id,
                        TagId = tagId
                    };
                    _blogTagRepository.Add(blogTag);
                }
            }
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task<IList<BlogDto>> GetLastest(int top)
        {
            return (await _blogRepository.GetManyAsync(x => x.Status == Status.Active)).OrderByDescending(x => x.DateCreated)
                .Take(top).To<IList<BlogDto>>();
        }

        public async Task<IList<BlogDto>> GetHotProduct(int top)
        {
            return (await _blogRepository.GetManyAsync(x => x.Status == Status.Active && x.HotFlag == true))
                .OrderByDescending(x => x.DateCreated)
                .Take(top)
                .To<IList<BlogDto>>();
        }

        public async Task SaveChangesAsync()
        {
            await _unitOfWork.SaveChangesAsync();
        }
    }
}