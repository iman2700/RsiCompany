using Entities.LinkModels;
using Entities.Models;
using Shared.DataTransferObjects;
using Shared.RequestFeatures;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Contracts
{
    public interface IEmployeeService
    {
        Task<(IEnumerable<ExpandoObject> employees, MetaData metaData)> GetEmployeesAsync(Guid companyId, EmployeeParameters employeeParameters, bool trackChanges);
        Task<(LinkResponse linkResponse, MetaData metaData)> GetEmployeesAsync(Guid companyId, LinkParameters linkParameters, bool trackChanges); EmployeeDto CreateEmployeeForCompany(Guid companyId, EmployeeForCreationDto employeeForCreation, bool trackChanges);
        void DeleteEmployeeForCompany(Guid companyId, Guid id, bool trackChanges);
        void UpdateEmployeeForCompany(Guid companyId, Guid id,EmployeeForUpdateDto employeeForUpdate, bool compTrackChanges,
            bool empTrackChanges);
        /// <returns>A tuple containing the EmployeeForUpdateDto representing the employee data to be patched and the Employee entity associated with the provided IDs.</returns>
        (EmployeeForUpdateDto employeeToPatch, Employee employeeEntity) GetEmployeeForPatch(Guid companyId, Guid id, bool compTrackChanges, bool empTrackChanges);

        void SaveChangesForPatch(EmployeeForUpdateDto employeeToPatch, Employee employeeEntity);


    }
}
