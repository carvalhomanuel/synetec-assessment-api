using SynetecAssessmentApi.Persistence.Infrastructure;

namespace SynetecAssessmentApi.Application.Services
{
    public abstract class BaseService
    {
        protected readonly IUnitOfWork UnitOfWork;

        protected BaseService(IUnitOfWork unitOfWork)
        {
            UnitOfWork = unitOfWork;
        }
    }
}
