using BlogApp.Core.Utilities.Results.Interfaces;
using BlogApp.UI.Models.Topics;
using IResult = BlogApp.Core.Utilities.Results.Interfaces.IResult;

namespace BlogApp.UI.Services.Interfaces;

public interface ITopicService
{
    Task<IDataResult<List<TopicListVM>>?> GetAllAsync(CancellationToken cancellationToken = default);
    Task<IResult> AddAsync(TopicAddVM topicAddVM, CancellationToken cancellationToken = default);
    Task<IDataResult<TopicUpdateVM>?> GetByIdAsync(Guid id, CancellationToken cancellation = default);
    Task<IResult> UpdateAsync(TopicUpdateVM topicUpdateVM, CancellationToken cancellationToken = default);
}
