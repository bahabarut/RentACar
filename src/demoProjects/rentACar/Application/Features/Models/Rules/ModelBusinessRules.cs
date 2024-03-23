using Application.Services.Repositories;
using Core.CrossCuttingConcerns.Exceptions;
using Core.Persistence.Paging;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Models.Rules
{
    public class ModelBusinessRules
    {
        private readonly IModelRepository _modelRepository;

        public ModelBusinessRules(IModelRepository modelRepository)
        {
            _modelRepository = modelRepository;
        }

        public async Task ModelNameCanNotBeDuplicatedWhenInserted(string name)
        {
            IPaginate<Model> result = await _modelRepository.GetListAsync(x => x.Name == name);
            if (result.Items.Any()) throw new BusinessException("Model name exists");
        }
        public void ModelShouldExistWhenRequested(Model? model)
        {
            if (model == null) throw new BusinessException("Requested model does not exist");
        }
    }
}
