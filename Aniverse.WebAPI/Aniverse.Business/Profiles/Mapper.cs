using Aniverse.Business.DTO_s.Animal;
using Aniverse.Business.DTO_s.Comment;
using Aniverse.Business.DTO_s.Friend;
using Aniverse.Business.DTO_s.Page;
using Aniverse.Business.DTO_s.Picture;
using Aniverse.Business.DTO_s.Post;
using Aniverse.Business.DTO_s.Post.Like;
using Aniverse.Business.DTO_s.Story;
using Aniverse.Business.DTO_s.User;
using Aniverse.Core.Entites;
using Aniverse.Core.Entities;
using AutoMapper;

namespace Aniverse.Business.Profiles
{
    public class Mapper : Profile
    {
        public Mapper()
        {
            CreateMap<AppUser, UserAllDto>()
                .ForMember(c => c.ProfilPicture, c => c.Ignore());
            CreateMap<AppUser, UserGetDto>();
            CreateMap<Post, PostGetDto>();
            CreateMap<Like, LikeGetDto>();
            CreateMap<LikeCreateDto, Like>();
            CreateMap<Comment, CommentGetDto>();
            CreateMap<CommentCreateDto, Comment>();
            CreateMap<Animal, AnimalAllDto>();
            CreateMap<Animal, AnimalGetDto>();
            CreateMap<Story, StoryGetDto>();
            CreateMap<UserFriend, UserFriendDto>();
            CreateMap<StoryCreateDto, Story>();
            CreateMap<PostCreateDto, Post>();
            CreateMap<PostImageDto, Picture>();
            CreateMap<AnimalFollow, AnimalFollowDto>();
            CreateMap<Picture, GetPictureDto>();
            CreateMap<AnimalCategory, AnimalGetCategory>();
            CreateMap<AnimalCreateDto, Animal>();
            CreateMap<Animal, AnimalSelectGetDto>();
            CreateMap<AnimalUpdateDto, Animal>();
            CreateMap<Page, PageGetDto>();

        }
    }
}
