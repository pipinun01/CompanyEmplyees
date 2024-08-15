using AutoMapper;
using Contracts;
using Entities.Exceptions;
using Entities.Models;
using Service.Contracts;
using Shared.DataTransferObjects;

namespace Service
{
    internal sealed class CompanyService: ICompanyService
    {
        private readonly IRepositoryManager _repositoryManager;
        private readonly ILoggerManager _loggerManager;
        private readonly IMapper _mapper;
        public CompanyService(IRepositoryManager repositoryManager, ILoggerManager loggerManager, IMapper mapper)
        {
            _repositoryManager = repositoryManager;
            _loggerManager = loggerManager;
            _mapper = mapper;
        }
        public IEnumerable<CompanyDto> GetAllCompanies(bool trackChanges)
        {

            var companies = _repositoryManager.CompanyRepository.GetAllCompanies(trackChanges);
            //var companiesDto = companies.Select(se=> new CompanyDto ( se.Id, se.Name ?? "", string.Join(' ', se.Address, se.Country))).ToList();
            var companiesDto = _mapper.Map<List<CompanyDto>>(companies);
            return companiesDto;
        }
        public CompanyDto GetCompany(Guid companyId, bool trackChanges)
        {
            var company = _repositoryManager.CompanyRepository.GetCompany(companyId, trackChanges);
            if( company is null )
            {
                throw new CompanyNotFoundException(companyId);
            }

            var companyDto = _mapper.Map<CompanyDto>(company);
            return companyDto;
        }
    }
}
