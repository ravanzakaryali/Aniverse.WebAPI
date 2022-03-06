using Aniverse.Business.DTO_s.Message;
using Aniverse.Business.Interface;
using Aniverse.Core;
using AutoMapper;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Aniverse.Business.Implementations
{
    public class MessageService : IMessageService
    {
        public readonly IUnitOfWork _unitOfWork;
        public readonly IMapper _mapper;
        public MessageService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public Task<List<MessageGetDto>> GetAllAsync(int id)
        {
            throw new System.NotImplementedException();
        }
    }
}
