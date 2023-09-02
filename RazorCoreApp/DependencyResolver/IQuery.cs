using MediatR;

namespace PS1_MIC_090_Core.DependencyResolver
{
    public interface IQuery<out TRespose> :IRequest<TRespose>
    {
    }
}

