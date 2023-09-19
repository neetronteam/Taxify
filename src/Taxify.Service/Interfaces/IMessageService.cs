using Taxify.Service.DTOs.Messages;

namespace Taxify.Service.Interfaces;

public interface IMessageService
{
    ValueTask<MessageResultDto> AddAsync(MessageCreationDto dto);
    ValueTask<MessageResultDto> ModifyAsync(MessageUpdateDto dto);
    ValueTask<bool> RemoveAsync(long id);
    ValueTask<bool> DestroyAsync(long id);
    ValueTask<MessageResultDto> RetrieveAsync(long id);
    ValueTask<IEnumerable<MessageResultDto>> RetrieveAllAsync();
}