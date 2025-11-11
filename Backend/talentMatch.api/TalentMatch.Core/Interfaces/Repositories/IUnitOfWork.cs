using TalentMatch.Domain.Entities;

namespace TalentMatch.Core.Interfaces.Repositories
{
    public interface IUnitOfWork : IDisposable
    {
        IGenericRepository<User> UserRepositoryAsync { get; }
        IGenericRepository<EmployerProfile> EmployerProfileRepositoryAsync { get; }
        IGenericRepository<JobSeekerProfile> JobSeekerProfileRepositoryAsync { get; }
        IGenericRepository<WorkExperience> WorkExperienceRepositoryAsync { get; }
        IGenericRepository<Certification> CertificationRepositoryAsync { get; }
        IGenericRepository<JobMatch> JobMatchRepositoryAsync { get; }
        IGenericRepository<JobPosting> JobPostingRepositoryAsync { get; }
        IGenericRepository<ApplicationQuestion> ApplicationQuestionRepositoryAsync { get; }
        IGenericRepository<Application> ApplicationRepositoryAsync { get; }
        IGenericRepository<ApplicationAnswer> ApplicationAnswerRepositoryAsync { get; }

        Task BeginTransactionAsync();

        Task CommitAsync();

        Task RollbackAsync();
    }
}