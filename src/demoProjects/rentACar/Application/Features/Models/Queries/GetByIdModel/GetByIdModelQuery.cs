using Application.Features.Models.Dtos;
using Application.Features.Models.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Models.Queries.GetByIdModel
{
    public class GetByIdModelQuery : IRequest<ModelGetByIdDto>
    {
        public int Id { get; set; }

        public class GetByIdModelQueryHandler : IRequestHandler<GetByIdModelQuery, ModelGetByIdDto>
        {
            private readonly IMapper _mapper;
            private readonly IModelRepository _modelRepository;
            private readonly ModelBusinessRules _modelBusinessRules;
            public GetByIdModelQueryHandler(IMapper mapper, IModelRepository modelRepository, ModelBusinessRules modelBusinessRules)
            {
                _mapper = mapper;
                _modelRepository = modelRepository;
                _modelBusinessRules = modelBusinessRules;
            }

            public async Task<ModelGetByIdDto> Handle(GetByIdModelQuery request, CancellationToken cancellationToken)
            {
                Model model = await _modelRepository.GetAsync(x => x.Id == request.Id);
                _modelBusinessRules.ModelShouldExistWhenRequested(model);
                ModelGetByIdDto mappedModel = _mapper.Map<ModelGetByIdDto>(model);
                return mappedModel;
            }
        }
    }
}
