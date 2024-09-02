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
        public CompanyDto CreateCompany(CompanyForCreationDto company)
        {
            var companyEntity = _mapper.Map<Company>(company);

            _repositoryManager.CompanyRepository.CreateCompany(companyEntity);
            _repositoryManager.Save();

            var companyReturn = _mapper.Map<CompanyDto>(companyEntity);
            return companyReturn;
        }
        public IEnumerable<CompanyDto> GetByIds(IEnumerable<Guid> ids, bool trackChanges)
        {
            if( ids == null)
            {
                throw new IdParametersBadRequestException();
            }
            var companyEntities = _repositoryManager.CompanyRepository.GetByIds(ids, trackChanges);
            if(ids.Count() != companyEntities.Count())
            {
                throw new CollectionByIdsBadRequestException();
            }
            var companiesToReturn = _mapper.Map<IEnumerable<CompanyDto>>(companyEntities);
            return companiesToReturn;
        }
        public (IEnumerable<CompanyDto> companies, string ids) CreateCompanyCollection(IEnumerable<CompanyForCreationDto> companyCollection)
        {
            if(companyCollection == null)
            {
                throw new CompanyCollectionBadRequest();
            }
            var companyEntities = _mapper.Map<IEnumerable<Company>>(companyCollection);
            foreach(var company in companyEntities)
            {
                _repositoryManager.CompanyRepository.CreateCompany(company);
            }
            _repositoryManager.Save();
            var companyCollectionToReturn = _mapper.Map<IEnumerable<CompanyDto>>(companyEntities);
            var ids = string.Join(",", companyCollectionToReturn.Select(s=>s.guid));
            return (companies: companyCollectionToReturn, ids: ids);
        }
    }
}
