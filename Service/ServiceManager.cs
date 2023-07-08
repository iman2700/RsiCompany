using AutoMapper;
using Contracts;
using Service.Contracts;
using Shared.DataTransferObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    public sealed class ServiceManager : IServiceManager
    {
        private readonly Lazy<ICompanyService> _companyService;
        private readonly Lazy<IEmployeeService> _employeeService;
       
        public ServiceManager(IRepositoryManager repositoryManager, ILoggerManager logger, IMapper mapper, IDataShaper<EmployeeDto> dataShaper)
        {
            _companyService = new Lazy<ICompanyService>(() =>
            {
                return new CompanyService(repositoryManager, logger, mapper);
            });
            _employeeService = new Lazy<IEmployeeService>(() =>
            {
                return new EmployeeService(repositoryManager, logger, mapper, dataShaper);
            });
        }

        // Method to create the EmployeeService object
        //public ICompanyService CompanyService { 
        //    get { return _companyService.Value; }
        //}


        public ICompanyService CompanyService => _companyService.Value;
        public IEmployeeService EmployeeService => _employeeService.Value;
    }
}
