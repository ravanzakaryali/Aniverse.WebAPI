using Aniverse.Core.Interfaces;
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
        IAnimalCategory AnimalCategory { get; }
        Task SaveAsync();
    }
}
