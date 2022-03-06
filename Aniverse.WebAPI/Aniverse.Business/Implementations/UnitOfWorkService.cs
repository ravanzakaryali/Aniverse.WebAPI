using Aniverse.Business.Interface;
using Aniverse.Core;
using Aniverse.Core.Entites;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Hosting;

namespace Aniverse.Business.Implementations
{
    public class UnitOfWorkService : IUnitOfWorkService
    {
        private IUserService _userService;
        private IFriendService _friendService;
        private IPostService _postService;
        private IMessageService _messageService;
        private ICommentService _commentService;
        private IAnimalService _animalService;
        private IStoryService _storyService;
        private ILikeService _likeService;
        private UserManager<AppUser> _userManager;

        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IHostEnvironment _hostEnvironment;
        public UnitOfWorkService(IUnitOfWork unitOfWork, IMapper mapper, IHostEnvironment hostEnvironment, UserManager<AppUser> userManager)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _hostEnvironment = hostEnvironment;
            _userManager = userManager;
        }
        public IUserService UserService => _userService ??= new UserService(_unitOfWork, _mapper, _hostEnvironment);
        public IFriendService FriendService => _friendService ??= new FriendService(_unitOfWork, _mapper);
        public IPostService PostService => _postService ??= new PostService(_unitOfWork, _mapper, _hostEnvironment);
        public IMessageService MessageService => _messageService ??= new MessageService(_unitOfWork, _mapper);
        public ICommentService CommentService => _commentService ??= new CommentService(_unitOfWork, _mapper);
        public IAnimalService AnimalService => _animalService ??= new AnimalService(_unitOfWork, _mapper);
        public IStoryService StoryService => _storyService ??= new StoryService(_unitOfWork, _mapper, _hostEnvironment);
        public ILikeService LikeService => _likeService ??= new LikeService(_unitOfWork, _mapper);
    }
}
