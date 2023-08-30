using Microsoft.AspNetCore.Mvc;

using System.Text.Json.Serialization;
using System.Windows.Input;

namespace PS1_MIC_090_Core.DependencyResolver
{
    public interface ICommand<out T> :IQuery<T> where T : ICommandResult
    {
    }
    public interface ICommand : IQuery<ICommandResult> 
    {
    }
    public interface ICommandSender 
    {
        Task<IActionResult> SendCommand(ICommand command, CancellationToken cancellationToken=default,bool throwOnError=false);
        Task<IActionResult> SendCommand<T>(ICommand<T> command, Func<object, IActionResult>? createActionResultAction = null,
            CancellationToken cancellationToken = default, bool throwOnError = false) where T : ICommandResult;
        Task<IActionResult> SendQuery<T>(IQuery<T> query, CancellationToken cancellationToken=default,bool throwOnError=false);
    }
}

