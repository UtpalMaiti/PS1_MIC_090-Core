using System.Net;

namespace PS1_MIC_090_Core.DependencyResolver
{
    public interface ICommandResult 
    {
       //[JsonIgnore] 
         HttpStatusCode  StatusCode { get;}
    }
}

