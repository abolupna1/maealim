using maealim.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace maealim.Data.Repositories
{
    public interface IMaealimRepository
    {
        void Add<T>(T entity) where T : class;
        void Delete<T>(T entity) where T : class;
        void Update<T>(T entity) where T : class;
        Task<bool> SavaAll();

        Task<IEnumerable<Season>> GetSeasons();
        Task<Season> GetSeason(int id);
        Task<IEnumerable<Jop>> GetJops();
        Task<Jop> GetJop(int id);

        Task<IEnumerable<Employee>> GetEmployees();
        Task<Employee> GetEmployee(int id);
        Task<IEnumerable<AppUser>> GetUsers();

        
        Task<IEnumerable<WorkSeason>> GetWorkSeasons();
        Task<WorkSeason> GetWorkSeason(int id);
        Task<IEnumerable<Country>> GetCountries();
        Task<IEnumerable<Continent>> GetContinents();
        Task<Country> GetCountry(int id);
        Task<IEnumerable<Bank>> GetBanks();
        Task<Bank> GetBank(int id);
        Task<IEnumerable<University>> GetUniversities();
        Task<University> GetUniversity(int id);
        Task<IEnumerable<College>> GetColleges();
        Task<College> GetCollege(int id);
        Task<IEnumerable<Stage>> GetStages();
        Task<Stage> GetStage(int id);
        Task<IEnumerable<Level>> GetLevels();
        Task<Level> GetLevel(int id);
        Task<IEnumerable<Language>> GetLanguages();
        Task<Language> GetLanguage(int id);
        Task<IEnumerable<Specialization>> GetSpecializations();
        Task<Specialization> GetSpecialization(int id);
        Task<IEnumerable<EmployeeContract>> GetEmployeeContracts();
        Task<EmployeeContract> GetEmployeeContract(int id);

        Task<bool> IsEemployeeHasContractActive(int employeeId);
        Task<bool> IsEemployeeHasContractActive(int id,int employeeId);
    }
}
