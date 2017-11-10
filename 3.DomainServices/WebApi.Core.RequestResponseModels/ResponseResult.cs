using WebApi.Core.ViewModels.Core;

namespace WebApi.Core.ServiceResponse
{
    public class ResponseResult<VM> : BaseResponseResult
        where VM: BaseViewModel
    {
        public VM ViewModel { get; set; }
    }
}
