using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Appiume.Apm.Application.Services.Dto;
using Appiume.Apm.Domain.Repositories;
using Appiume.Apm.Domain.Uow;
using Appiume.Web.IoT.Core.Users;
using Appiume.Web.IoT.Core.MultiTenancy;

namespace Appiume.Web.IoT.Application.Statistics
{
    public class StatisticsAppService : IoTAppServiceBase, IStatisticsAppService
    {
        private readonly IRepository<Tenant> _tenantRepository;
        private readonly IRepository<User, long> _userRepository;

        public StatisticsAppService(
            IRepository<Tenant> tenantRepository,
            IRepository<User, long> userRepository)
        {
            _tenantRepository = tenantRepository;
            _userRepository = userRepository;
        }

        public async Task<ListResultOutput<NameValueDto>> GetStatistics()
        {
            //Disabled filters to access to all tenant's data, not for only current tenant.
            using (CurrentUnitOfWork.DisableFilter(ApmDataFilters.MayHaveTenant, ApmDataFilters.MustHaveTenant))
            {
                var statisticItems = new List<NameValueDto>
                {
                    new NameValueDto(
                        L("Tenants"),
                        (await _tenantRepository.CountAsync()).ToString()
                        ),
                    new NameValueDto(
                        L("Users"),
                        (await _userRepository.CountAsync()).ToString()
                        )
                };

                return new ListResultOutput<NameValueDto>(statisticItems);
            }
        }
    }
}
