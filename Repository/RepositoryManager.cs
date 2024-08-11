using Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public sealed class RepositoryManager : IRepositoryManager
    {
        private readonly RepositoryContext _context;
        private readonly Lazy<ICompanyRepository> _companyRepository;
        private readonly Lazy<IEmployeeRepository> _employeeRepository;
        
        public RepositoryManager(RepositoryContext repositoryContext)
        {
            _context = repositoryContext;
            _companyRepository = new Lazy<ICompanyRepository>(() => new CompanyRepository(repositoryContext));
            _employeeRepository = new Lazy<IEmployeeRepository>(() => new EmployeeRepository(repositoryContext));
        }
        //public ICompanyRepository CompanyRepository { get { return _companyRepository.Value; } }    ВЕРНО НО ЕСТЬ УПРАЩЕННЫЙ КОД
        public ICompanyRepository CompanyRepository => _companyRepository.Value;
        public IEmployeeRepository EmployeeRepository => _employeeRepository.Value;
        
        public void Save()=> _context.SaveChanges();
    }
}
