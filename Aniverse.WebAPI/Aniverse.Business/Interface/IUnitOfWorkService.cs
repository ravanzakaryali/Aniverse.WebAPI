namespace Aniverse.Business.Interface
{
    public interface IUnitOfWorkService
    {
        IUserService UserService { get; }
        IFriendService FriendService { get; }
        IPostService PostService { get; }
        IMessageService MessageService { get; }
        ICommentService CommentService { get; }
        IAnimalService AnimalService { get; }
        IStoryService StoryService { get; }
        ILikeService LikeService { get; }
    }
}
