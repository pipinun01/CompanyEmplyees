using Contracts;
using Entities.Models;
using Service.Contracts;

namespace Service
{
    internal sealed class CompanyService: ICompanyService
    {
        private readonly IRepositoryManager _repositoryManager;
        private readonly ILoggerManager _loggerManager;
        public CompanyService(IRepositoryManager repositoryManager, ILoggerManager loggerManager)
        {
            _repositoryManager = repositoryManager;
            _loggerManager = loggerManager;
        }
        public IEnumerable<Company> GetAllCompanies(bool trackChanges)
        {
            try
            {
                var companies = _repositoryManager.CompanyRepository.GetAllCompanies(trackChanges);
                return companies;
            }
            catch (Exception ex) 
            {
                _loggerManager.LogError($"GetAllCompanies_CompanyService _Messages: {ex.Message} _Stacktrace: {ex.StackTrace}");
                throw;
            }
        }
    }
}
