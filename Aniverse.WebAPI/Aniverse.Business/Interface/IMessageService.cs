using Aniverse.Business.DTO_s.Message;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Aniverse.Business.Interface
{
    public interface IMessageService
    {
        Task<List<MessageGetDto>> GetAllAsync(int id);
    }
}
