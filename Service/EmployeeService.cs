using AutoMapper;
using Contracts;
using Entities.Exceptions;
using Entities.Models;
using Service.Contracts;
using Shared.DataTransferObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    internal sealed class EmployeeService : IEmployeeService
    {
        private readonly IRepositoryManager _repositoryManager;
        private readonly ILoggerManager _loggerManager;
        private readonly IMapper _mapper;
        public EmployeeService(IRepositoryManager repositoryManager, ILoggerManager loggerManager, IMapper mapper)
        {
            _repositoryManager = repositoryManager;
            _loggerManager = loggerManager;
            _mapper = mapper;
        }

        public IEnumerable<EmployeeDto> GetEmployees(Guid companyId, bool trackChanges)
        {
            var company = _repositoryManager.CompanyRepository.GetCompany(companyId, trackChanges);
            if (company == null) 
            {
                throw new CompanyNotFoundException(companyId);  
            }
            var employees = _repositoryManager.EmployeeRepository.GetEmployees(companyId, trackChanges);
            var employeeDto = _mapper.Map<IEnumerable<EmployeeDto>>(employees);
            return employeeDto;
        }

        public EmployeeDto GetEmployee(Guid companyId, Guid employeeId, bool trackChanges) 
        {
            var company = _repositoryManager.CompanyRepository.GetCompany(companyId, trackChanges);
            if (company is null)
                throw new CompanyNotFoundException(companyId);

            var employeeDb = _repositoryManager.EmployeeRepository.GetEmployee(companyId, employeeId, trackChanges);
            if(employeeDb is null)
                throw new EmployeeNotFoundException(employeeId);

            var employee = _mapper.Map<EmployeeDto>(employeeDb);
            return employee;
        }
        public EmployeeDto CreateEmployeeForCompany (Guid companyId, EmployeeForCreationDto employeeForCreationDto, bool trackChanges)
        {
            var company = _repositoryManager.CompanyRepository.GetCompany(companyId, trackChanges);
            if(company is null)
            {
                throw new CompanyNotFoundException(companyId);
            }
            var employeeEntity = _mapper.Map<Employee>(employeeForCreationDto);
            _repositoryManager.EmployeeRepository.CreateEmployeeCompany(companyId, employeeEntity);
            _repositoryManager.Save();

            var employeeToReturn = _mapper.Map<EmployeeDto>(employeeEntity);
            return employeeToReturn;
        }
    }
}
