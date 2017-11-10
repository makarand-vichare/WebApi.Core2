using System;
using System.Linq;
using WebApi.Core.DomainServices.Core;
using WebApi.Core.EntityModels.Location;
using WebApi.Core.IDomainServices.Services;
using WebApi.Core.InfraStructure.Logging;
using WebApi.Core.ServiceResponse;
using WebApi.Core.Utility;
using WebApi.Core.ViewModels;

namespace WebApi.Core.DomainServices
{
    public class CityService : BaseService<City, CityViewModel>, ICityService
    {
        public ResponseResults<LookUpViewModel> GetLookup(long countryId)
        {
            var response = new ResponseResults<LookUpViewModel> { IsSucceed = true, Message = AppMessages.Retrieved_Details_Successfully };
            try
            {
                var entities =  UnitOfWork.CityRepository.GetMany(o=>o.CountryId == countryId).ToList();
                if (entities != null && entities.Count > 0)
                {
                    response.ViewModels = entities.Select(o=> new LookUpViewModel { Id = o.Id , Value = o.CityName }).ToList();
                }

                if (entities != null && entities.Count <= 0)
                {
                    response.Message = AppMessages.No_Record_Found;
                }
            }
            catch (Exception ex)
            {
                NLogLogger.Instance.Log(ex.Message);
            }
            return response;
        }
    }
}
