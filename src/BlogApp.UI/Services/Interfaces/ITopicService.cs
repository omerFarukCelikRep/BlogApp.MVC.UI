using BlogApp.Core.Utilities.Results.Interfaces;
using BlogApp.UI.Models.Topics;
using IResult = BlogApp.Core.Utilities.Results.Interfaces.IResult;

namespace BlogApp.UI.Services.Interfaces;

public interface ITopicService
{
    Task<IDataResult<List<TopicListVM>>?> GetAll();
    Task<IResult> AddAsync(TopicAddVM topicAddVM);
}
