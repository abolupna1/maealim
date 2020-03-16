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
        Task<EmployeeContract> GetEmployeeContractByEmpId(int employeeId);

        Task<bool> IsEemployeeHasContractActive(int employeeId);
        Task<bool> IsEemployeeHasContractActive(int id,int employeeId);
        Task<IEnumerable<MGuide>> GetGuides();
        Task<MGuide> GetGuide(int id);
        Task<MGuide> GetGuideByUserId(int userId);

        Task<IEnumerable<GuideContract>> GetGuideContracts();
        Task<GuideContract> GetGuideContract(int id);
        Task<bool> IsGuideHasContractActive( int guideId);
        Task<bool> IsGuideHasContractActive(int id, int guideId);
        Task<GuideContract> GetGuideContractByGuideId(int guideId);


        Task<IEnumerable<TypeOfProduct>> GetTypeOfProducts();
        Task<TypeOfProduct> GetTypeOfProduct(int id);

        Task<IEnumerable<ItemOfProduct>> GetItemOfProducts();
        Task<ItemOfProduct> GetItemOfProduct(int id);

        Task<IEnumerable<ItemExport>> GetItemExports();
        Task<ItemExport> GetItemExport(int id);

        Task<IEnumerable<ItemImport>> GetItemImports();
        Task<ItemImport> GetItemImport(int id);
        Task<bool> CheckIfImportsEqualExport(int itemOfProductId,int qty);

        Task<IEnumerable<TypeNotable>> GetTypeNotables();
        Task<TypeNotable> GetTypeNotable(int id);

        Task<IEnumerable<JobNotable>> GetJobNotables();
        Task<JobNotable> GetJobNotable(int id);

        Task<IEnumerable<Sheikh>> GetSheikhs();
        Task<Sheikh> GetSheikh(int id);

        Task<IEnumerable<GuestReservation>> GetGuestReservations();
        Task<IEnumerable<GuestReservation>> GetGuestReservationsByGuideId(int guideId);
        Task<GuestReservation> GetGuestReservation(int id);
        Task<bool> GetCheckGuidHaveWorkInTheDay(int guideId,DateTime date);



        Task<IEnumerable<Notable>> GetNotables();
        Task<IEnumerable<Notable>> GetJobNotablesNormal(int guestReservationId);
        Task<IEnumerable<Notable>> GetJobNotablesNotNormal(int guestReservationId);
        Task<IEnumerable<Notable>> GetNotablesByCountry(int countryId);
        Task<Notable> GetNotable(int id);

        Task<IEnumerable<Gift>> GetGiftsByGuestReservationId(int guestReservationId);
        Task<IEnumerable<Gift>> GetGifts();
        Task<Gift> GetGift(int id);

        Task<IEnumerable<TypesMessage>> GetTypesMessages();
        Task<TypesMessage> GetTypesMessage(int id);

        Task<IEnumerable<WjhaaMessage>> GetWjhaaMessages();
        Task<WjhaaMessage> GetWjhaaMessage(int id);
        Task<int> CountMessagesSentsToCountry(int countryId);


        Task<IEnumerable<Attend>> GetAttends();
        Task<IEnumerable<Attend>> GetEmployeeAttends();
        Task<IEnumerable<Attend>> GetGuideAttends();
        Task<Attend> GetAttend(int id);
        Task<IEnumerable<Employee>> GetEmployeeContractActive();
        Task<IEnumerable<MGuide>> GetGuideContractActive();


        




    }
}
