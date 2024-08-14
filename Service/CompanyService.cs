﻿using AutoMapper;
using Contracts;
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
            try
            {
                var companies = _repositoryManager.CompanyRepository.GetAllCompanies(trackChanges);
                //var companiesDto = companies.Select(se=> new CompanyDto ( se.Id, se.Name ?? "", string.Join(' ', se.Address, se.Country))).ToList();
                var companiesDto = _mapper.Map<IEnumerable<CompanyDto>>(companies);
                return companiesDto;
            }
            catch (Exception ex) 
            {
                _loggerManager.LogError($"GetAllCompanies_CompanyService _Messages: {ex.Message} _Stacktrace: {ex.StackTrace}");
                throw;
            }
        }
    }
}
