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

namespace Application.Features.Models.Commands.CreateModel
{
    public class CreateModelCommand : IRequest<CreatedModelDto>
    {
        public int BrandId { get; set; }
        public string Name { get; set; }
        public decimal DailyPrice { get; set; }
        public string ImageUrl { get; set; }

        public class CreateModelCommandHandler : IRequestHandler<CreateModelCommand, CreatedModelDto>
        {
            private readonly IMapper _mapper;
            private readonly IModelRepository _modelRepository;
            private readonly ModelBusinessRules _modelBusinessRules;
            public CreateModelCommandHandler(IMapper mapper, IModelRepository modelRepository, ModelBusinessRules modelBusinessRules)
            {
                _mapper = mapper;
                _modelRepository = modelRepository;
                _modelBusinessRules = modelBusinessRules;
            }
            public async Task<CreatedModelDto> Handle(CreateModelCommand request, CancellationToken cancellationToken)
            {
                await _modelBusinessRules.ModelNameCanNotBeDuplicatedWhenInserted(request.Name);

                Model model = _mapper.Map<Model>(request);
                Model createdModel = await _modelRepository.AddAsync(model);
                CreatedModelDto mappedModel = _mapper.Map<CreatedModelDto>(createdModel);
                return mappedModel;
            }
        }
    }
}
