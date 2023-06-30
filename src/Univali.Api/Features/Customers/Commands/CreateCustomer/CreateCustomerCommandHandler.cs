using AutoMapper;
using FluentValidation;
using MediatR;
using Univali.Api.Entities;
using Univali.Api.Repositories;

namespace Univali.Api.Features.Customers.Commands.CreateCustomer;

public class CreateCustomerCommandHandler : IRequestHandler<CreateCustomerCommand, CreateCustomerCommandResponse>
{
    private readonly ICustomerRepository _customerRepository;
    private readonly IMapper _mapper;
    private readonly IValidator<CreateCustomerCommand> _validator; 

    public CreateCustomerCommandHandler(ICustomerRepository customerRepository, IMapper mapper, IValidator<CreateCustomerCommand> validator)
    {
        _customerRepository = customerRepository;
        _mapper = mapper;
        _validator = validator;
    }

    public async Task<CreateCustomerCommandResponse> Handle(CreateCustomerCommand request, CancellationToken cancellationToken)
    {
        CreateCustomerCommandResponse createCustomerCommandResponse = new();

        var validationResult = _validator.Validate(request);
        if(!validationResult.IsValid) {
            foreach(var error in validationResult.ToDictionary()) {
                createCustomerCommandResponse.Errors.Add(error.Key, error.Value);
            }

            createCustomerCommandResponse.IsSuccess = false;
            return createCustomerCommandResponse;
        }

        Customer customerEntity = _mapper.Map<Customer>(request);
        _customerRepository.CreateCustomer(customerEntity);
        await _customerRepository.SaveChangesAsync();
        createCustomerCommandResponse.CusomerDto = _mapper.Map<CreateCustomerDto>(customerEntity);
        return createCustomerCommandResponse;
    }
}