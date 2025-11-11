using Microsoft.EntityFrameworkCore.Storage;
using TalentMatch.Core.Interfaces.Repositories;
using TalentMatch.Domain.Entities;
using TalentMatch.Infrastructure.Repositories.RespositoryAsync;
using TalentMatch.Infrastructure.Settings;

namespace TalentMatch.Infrastructure.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly Context _context;
        private IDbContextTransaction _transaction;

        public UnitOfWork(Context context)
        {
            _context = context;
        }

        public IGenericRepository<User> UserRepositoryAsync => new UserRepositoryAsync(_context);
        public IGenericRepository<EmployerProfile> EmployerProfileRepositoryAsync => new EmployerProfileRepositoryAsync(_context);
        public IGenericRepository<JobSeekerProfile> JobSeekerProfileRepositoryAsync => new JobSeekerProfileRepositoryAsync(_context);
        public IGenericRepository<WorkExperience> WorkExperienceRepositoryAsync => new WorkExperienceRepositoryAsync(_context);
        public IGenericRepository<Certification> CertificationRepositoryAsync => new CertificationRepositoryAsync(_context);
        public IGenericRepository<JobMatch> JobMatchRepositoryAsync => new JobMatchRepositoryAsync(_context);
        public IGenericRepository<JobPosting> JobPostingRepositoryAsync => new JobPostingRepositoryAsync(_context);
        public IGenericRepository<ApplicationQuestion> ApplicationQuestionRepositoryAsync => new ApplicationQuestionRepositoryAsync(_context);
        public IGenericRepository<Application> ApplicationRepositoryAsync => new ApplicationRepositoryAsync(_context);
        public IGenericRepository<ApplicationAnswer> ApplicationAnswerRepositoryAsync => new ApplicationAnswerRepositoryAsync(_context);

        public async Task BeginTransactionAsync()
        {
            _transaction = await _context.Database.BeginTransactionAsync().ConfigureAwait(false);
        }

        public async Task CommitAsync()
        {
            try
            {
                await BeginTransactionAsync();
                await _context.SaveChangesAsync();
                await _transaction.CommitAsync();
            }
            catch
            {
                await RollbackAsync();
                throw;
            }
            finally
            {
                _transaction.Dispose();
                Dispose();
            }
        }

        private bool disposed = false;

        public void Dispose(bool disposing)
        {
            if (disposed)
            {
                _context.Dispose();
            }
            else
            {
                disposed = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public Task RollbackAsync()
        {
            _transaction.Rollback();
            _transaction.Dispose();
            return Task.CompletedTask;
        }
    }
}