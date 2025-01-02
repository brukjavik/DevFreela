using DevFreela.Application.Models;
using DevFreela.Core.Repositories;
using DevFreela.Infrastructure.Persistence;
using MediatR;

namespace DevFreela.Application.Commands.DeleteProject
{
    public class DeleteProjectHandler : IRequestHandler<DeleteProjectCommand, ResultViewModel>
    {
        private readonly IProjectRepository _repository;
        public DeleteProjectHandler(DevFreelaDbContext context, IProjectRepository repository)
        {
            _repository = repository;
        }
        public async Task<ResultViewModel> Handle(DeleteProjectCommand request, CancellationToken cancellationToken)
        {
            var project = await _repository.GetById(request.Id);

            if (project == null)
            {
                return ResultViewModel.Error("Projeto não encontrado");
            }
            project.SetAsDeleted();
            await _repository.Update(project);

            return ResultViewModel.Success();
        }
    }
}
