using kiseleva_nastia_42_21.Database;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using kiseleva_nastia_42_21.Filters;
using kiseleva_nastia_42_21.Models;
namespace kiseleva_nastia_42_21.Interfaces.WorkloadInterfaces
{
    public interface IWorkloadService
    {
        public Task<Workload[]> GetWorkloadByEducationSubjectForNumOfHourse(NumberOfHourseFilter filter, CancellationToken cancellationToken = default);
        public Task<Workload[]> GetWorkloadsByProfessorNameAsync(string firstName, string lastName, string middleName, CancellationToken cancellationToken);
    }
    public class WorkloadService : IWorkloadService
    {
        public Task<Workload[]> GetWorkloadByEducationSubjectForNumOfHourse(NumberOfHourseFilter filter, CancellationToken cancellationToken = default)
        {
            var workload = _dbContext.Set<Workload>()
            .Where(w => (w.NumberOfHours >= filter.minHours) && (w.NumberOfHours <= filter.maxHours))
            .ToArrayAsync(cancellationToken);

            return workload;
        }
        //private readonly SmirnovDbContext _dbContext;
        private KiselevaDbContext _dbContext;
        public WorkloadService(KiselevaDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<Workload[]> GetWorkloadsByProfessorNameAsync(string firstName, string lastName, string middleName, CancellationToken cancellationToken = default)
        {
            var professor = await _dbContext.Set<Professor>()
                .FirstOrDefaultAsync(p =>
                    p.LastName.Contains(lastName) ||
                    p.FirstName.Contains(firstName) ||
                    p.MiddleName.Contains(middleName), cancellationToken);
            if (professor == null)
            {
                return Array.Empty<Workload>(); // Или выбросьте исключение
            }
            return await _dbContext.Workloads
                .Where(w => w.ProfessorId == professor.Id)
                .ToArrayAsync(cancellationToken);

            
        }
    }
}