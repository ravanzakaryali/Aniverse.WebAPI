using Aniverse.Core;
using Aniverse.Core.Interfaces;
using Aniverse.Data.DAL;
using Aniverse.Data.Implementations;
using System.Threading.Tasks;

namespace Aniverse.Data
{
    public class UnitOfWork : IUnitOfWork
    {
        private AppDbContext _context { get; }
        private IUserRepository _userRepository;
        private IFriendRepository _friendRepository;
        private IAnimalRepository _animalRepository;
        private IPostRepository _postRepository;
        private IMessageRepository _messageRepository;
        private ICommentRepository _commentRepository;
        private IStoryRepository _storyRepository;
        private ILikeRepository _likeRepository;
        private IAnimalFollowRepository _animalFollowRepository;
        private IPictureRepository _pictureRepository;
        private IAnimalCategoryRepository _animalCategoryRepository;
        private ISavePostRepository _savePostRepository;
        private IPageRepository _pageRepository;
        private IPageFollowRepository _pageFollowRepository;
        private IProductRepository _productRepository;
        private IProductCategoryRepository _productCategoryRepository;
        private ISaveProductRepository _saveProductRepository;
        public UnitOfWork(AppDbContext context)
        {
            _context = context;
        }
        public IUserRepository UserRepository => _userRepository ??= new UserRepository(_context);
        public IFriendRepository FriendRepository => _friendRepository ??= new FriendRepository(_context);
        public IAnimalRepository AnimalRepository => _animalRepository ??= new AnimalRepository(_context);
        public IPostRepository PostRepository => _postRepository ??= new PostRepository(_context);
        public IMessageRepository MessageRepository => _messageRepository ??= new MessageRepository(_context);
        public ICommentRepository CommentRepository => _commentRepository ??= new CommentRepository(_context);
        public IStoryRepository StoryRepository => _storyRepository ??= new StoryRepository(_context);
        public ILikeRepository LikeRepository => _likeRepository ??= new LikeRepository(_context);
        public IAnimalFollowRepository AnimalFollowRepository => _animalFollowRepository ??= new AnimalFollowRepository(_context);
        public IPictureRepository PictureRepository => _pictureRepository ??= new PictureRepository(_context);
        public IAnimalCategoryRepository AnimalCategoryRepository => _animalCategoryRepository ??= new AnimalCategoryRepository(_context);
        public ISavePostRepository SavePostRepository => _savePostRepository ??= new SavePostRepository(_context);
        public IPageRepository PageRepository => _pageRepository ??= new PageRepository(_context);
        public IPageFollowRepository PageFollowRepository => _pageFollowRepository ??= new PageFollowRepository(_context);
        public IProductRepository ProductRepository => _productRepository ??= new ProductRepository(_context); 
        public IProductCategoryRepository ProductCategoryRepository => _productCategoryRepository ??= new ProductCategoryRepository(_context);
        public ISaveProductRepository SaveProductRepository => _saveProductRepository ??= new SaveProductRepository(_context);
        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
