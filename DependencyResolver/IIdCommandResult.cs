namespace PS1_MIC_090_Core.DependencyResolver
{
    public interface IIdCommandResult<out T> :ICommandResult
    {
         T Id { get;  }
    }
}

