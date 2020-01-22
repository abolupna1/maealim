using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using maealim.Models;
using Microsoft.EntityFrameworkCore;

namespace maealim.Data.Repositories
{
    public class MaealimRepository : IMaealimRepository
    {
        private readonly ApplicationDbContext _context;

        public MaealimRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public void Add<T>(T entity) where T : class
        {

            _context.Add(entity);
        }

        public void Delete<T>(T entity) where T : class
        {
            _context.Remove(entity);
        }

        public async Task<Bank> GetBank(int id)
        {
            return await _context.Banks.SingleOrDefaultAsync(b=>b.Id==id);
        }

        public async Task<IEnumerable<Bank>> GetBanks()
        {
            return await _context.Banks.ToListAsync();
        }

        public async Task<College> GetCollege(int id)
        {
            return await _context.Colleges.SingleOrDefaultAsync(c=>c.Id==id);
        }

        public async Task<IEnumerable<College>> GetColleges()
        {
            return await _context.Colleges.ToListAsync();

        }

        public async Task<IEnumerable<Continent>> GetContinents()
        {
            return await _context.Continents.ToListAsync();
        }

        public async Task<IEnumerable<Country>> GetCountries()
        {
            return await _context.Countries.Include(d=>d.Continent).ToListAsync();
        }

        public async Task<Country> GetCountry(int id)
        {
            return await _context.Countries.SingleOrDefaultAsync(c=>c.Id==id);

        }

        public async Task<Employee> GetEmployee(int id)
        {
            return await _context.Employees.SingleOrDefaultAsync(s => s.Id == id);

        }

        public async Task<IEnumerable<Employee>> GetEmployees()
        {
            return await _context.Employees.Include(d=>d.User).Include(d=>d.Jop).ToListAsync();
        }

        public async Task<EmployeeContract> GetEmployeeContract(int id)
        {
            return await _context.EmployeeContracts.SingleOrDefaultAsync(e => e.Id == id);
        }

        public async Task<IEnumerable<EmployeeContract>> GetEmployeeContracts()
        {
            return await _context.EmployeeContracts
                  .Include(d => d.Employee)
                .Include(d => d.Season)
                .Include(d => d.Jop)
                .ToListAsync();
        }

        public async Task<Jop> GetJop(int id)
        {
            return await _context.Jops.SingleOrDefaultAsync(s => s.Id == id);
        }

        public async Task<IEnumerable<Jop>> GetJops()
        {
            return await _context.Jops.ToListAsync();
        }

        public async Task<Language> GetLanguage(int id)
        {
            return await _context.Languages.SingleOrDefaultAsync(l=>l.Id==id);
        }

        public async Task<IEnumerable<Language>> GetLanguages()
        {
            return await _context.Languages.ToListAsync();
        }

        public async Task<Level> GetLevel(int id)
        {
            return await _context.Levels.SingleOrDefaultAsync(s=>s.Id==id);
        }

        public async Task<IEnumerable<Level>> GetLevels()
        {
            return await _context.Levels.ToListAsync();
        }

        public async Task<Season> GetSeason(int id)
        {
            return await _context.Seasons.SingleOrDefaultAsync(s => s.Id == id);
        }

        public async Task<IEnumerable<Season>> GetSeasons()
        {
            return await _context.Seasons.ToListAsync();
        }

        public async Task<Specialization> GetSpecialization(int id)
        {
            return await _context.Specializations.SingleOrDefaultAsync(s=>s.Id.Equals(id));
        }

        public async Task<IEnumerable<Specialization>> GetSpecializations()
        {
            return await _context.Specializations.ToListAsync();

        }

        public async Task<Stage> GetStage(int id)
        {
            return await _context.Stages.SingleOrDefaultAsync(s=>s.Id==id);
        }

        public async Task<IEnumerable<Stage>> GetStages()
        {
            return await _context.Stages.ToListAsync();
        }

        public async Task<IEnumerable<University>> GetUniversities()
        {
            return await _context.Universities.ToListAsync();
        }

        public async Task<University> GetUniversity(int id)
        {
            return await _context.Universities.SingleOrDefaultAsync(u=>u.Id==id);

        }

        public async Task<IEnumerable<AppUser>> GetUsers()
        {
        return  await _context.Users.ToListAsync();
        }

        public async Task<WorkSeason> GetWorkSeason(int id)
        {
            return await _context.WorkSeasons.SingleOrDefaultAsync(w=>w.Id == id);
        }

        public async Task<IEnumerable<WorkSeason>> GetWorkSeasons()
        {
            return await _context.WorkSeasons.Include(d=>d.Season).ToListAsync();
        }

        public async Task<bool> SavaAll()
        {
            return await _context.SaveChangesAsync() > 0;
        }

        public void Update<T>(T entity) where T : class
        {
            _context.Update(entity);
        }

        public async Task<bool> IsEemployeeHasContractActive(int employeeId)
        {
            return await _context.EmployeeContracts.AnyAsync(e=>e.EmployeeId==employeeId&&e.Status==true);
        }

        public async Task<bool> IsEemployeeHasContractActive(int id, int employeeId)
        {
            var contract =  await _context.EmployeeContracts.SingleOrDefaultAsync(d=>d.Id==id);
            if (contract.EmployeeId == employeeId)
            {
                var contractCheck = await _context.EmployeeContracts.
                    AnyAsync(e =>e.Id != id && e.EmployeeId == employeeId && e.Status == true);
                return contractCheck;
            }
            else
            {
                var contractCheck = await _context.EmployeeContracts.
                    AnyAsync(e => e.Id != id && e.EmployeeId == employeeId && e.Status == true);
                return contractCheck;
            }
        }

        public async Task<IEnumerable<MGuide>> GetGuides()
        {
            return await _context.MGuides
                .Include(d=>d.AppUser)
                .Include(d=>d.Bank)
                .Include(d=>d.College)
                .Include(d=>d.Country)
                .Include(d=>d.Language)
                .Include(d=>d.Level)
                .Include(d=>d.Specialization)
                .Include(d=>d.Stage)
                .Include(d=>d.University)
                .ToListAsync();
        }

        public async Task<MGuide> GetGuide(int id)
        {
            return await _context.MGuides.SingleOrDefaultAsync(d=>d.Id==id);
        }

        public async Task<IEnumerable<GuideContract>> GetGuideContracts()
        {
            return await _context.GuideContracts
                .Include(g=>g.Guide)
                .Include(g => g.Jop)
                .Include(g => g.Season)
                .ToListAsync();
        }

        public async Task<bool> IsGuideHasContractActive(int id, int guideId)
        {
            var contract = await _context.GuideContracts.SingleOrDefaultAsync(d => d.Id == id);
            
            if (contract.GuideId == guideId)
            {
                var contractCheck = await _context.GuideContracts.
                    AnyAsync(e => e.Id != id && e.GuideId == guideId && e.Status == true);
                return contractCheck;
            }
            else
            {
                var contractCheck = await _context.GuideContracts.
                    AnyAsync(e => e.Id != id && e.GuideId == guideId && e.Status == true);
                return contractCheck;
            }
       
        }

        public async Task<GuideContract> GetGuideContract(int id)
        {
            return await _context.GuideContracts.SingleOrDefaultAsync(c=>c.Id==id);
        }

        public async Task<bool> IsGuideHasContractActive(int guideId)
        {
            return await _context.GuideContracts.AnyAsync(e => e.GuideId == guideId && e.Status == true);

        }

        public async Task<IEnumerable<TypeOfProduct>> GetTypeOfProducts()
        {
            return await _context.TypeOfProducts.ToListAsync();
        }

        public async Task<TypeOfProduct> GetTypeOfProduct(int id)
        {
            return await _context.TypeOfProducts.SingleOrDefaultAsync(d=>d.Id==id);
        }
    }
}
