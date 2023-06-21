using Entities.Models;
using Shared.DataTransferObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Contracts
{
    public interface ICompanyService
    {
        IEnumerable<CompanyDto> GetAllCompanies(bool trackChanges);
        CompanyDto GetCompany(Guid companyId, bool trackChanges);
        CompanyDto CreateCompany(CompanyForCreationDto company);
        IEnumerable<CompanyDto> GetByIds(IEnumerable<Guid> ids, bool trackChanges);
        /// <summary>
        /// Creates a collection of companies based on the provided company data and returns a collection of CompanyDto objects.
        /// </summary>
        /// <param name="companyCollection">The collection of CompanyForCreationDto objects containing the data for creating companies.</param>
        /// <returns>A collection of CompanyDto objects representing the created companies.</returns>
        (IEnumerable<CompanyDto> companies, string ids) CreateCompanyCollection(IEnumerable<CompanyForCreationDto> companyCollection);
        void DeleteCompany(Guid companyId, bool trackChanges);
        void UpdateCompany(Guid companyid, CompanyForUpdateDto companyForUpdate, bool trackChanges);

    }
}
