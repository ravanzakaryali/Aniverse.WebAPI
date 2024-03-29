﻿using Aniverse.Core.Interfaces;
using System.Threading.Tasks;

namespace Aniverse.Core
{
    public interface IUnitOfWork
    {
        IUserRepository UserRepository { get; }
        IFriendRepository FriendRepository { get; }
        IPostRepository PostRepository { get; }
        IMessageRepository MessageRepository { get; }
        ICommentRepository CommentRepository { get; }
        IAnimalRepository AnimalRepository { get; }
        IStoryRepository StoryRepository { get; }
        ILikeRepository LikeRepository { get; }
        IAnimalFollowRepository AnimalFollowRepository { get; }
        IPictureRepository PictureRepository { get; }
        IAnimalCategoryRepository AnimalCategoryRepository { get; }
        ISavePostRepository SavePostRepository { get; }
        IPageRepository PageRepository { get; }
        IPageFollowRepository PageFollowRepository { get; }
        IProductRepository ProductRepository { get; }
        IProductCategoryRepository ProductCategoryRepository { get; }
        ISaveProductRepository SaveProductRepository { get; }
        Task SaveAsync();
    }
}
