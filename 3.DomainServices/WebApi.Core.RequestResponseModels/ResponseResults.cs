using System.Collections.Generic;
using WebApi.Core.ViewModels.Core;

namespace WebApi.Core.ServiceResponse
{
    public class ResponseResults<VM> : BaseResponseResult  where VM: BaseViewModel
    {
        public List<VM> ViewModels { get; set; } 
    }
}
