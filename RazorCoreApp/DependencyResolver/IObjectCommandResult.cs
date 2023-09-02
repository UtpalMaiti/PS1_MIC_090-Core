namespace PS1_MIC_090_Core.DependencyResolver
{
    public interface IObjectCommandResult<out T> : ICommandResult
    {
        T Result { get; }
    }
}

