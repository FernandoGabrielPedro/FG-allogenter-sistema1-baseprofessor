using AutoMapper;

namespace Univali.Api.Profiles;

public class CustomerProfile : Profile {
    public CustomerProfile() {
        //1º arg: objeto de origem
        //2º arg: objeto de destino
        CreateMap<Entities.Customer, Models.CustomerDto>();
        CreateMap<Models.CustomerForPatchDto, Entities.Customer>();
        CreateMap<Models.CustomerForCreationDto, Entities.Customer>();
        CreateMap<Models.CustomerForUpdateDto, Entities.Customer>();
        CreateMap<Entities.Customer, Models.CustomerWithAddressesDto>();
        CreateMap<Models.CustomerForCreationWithAddressesDto, Entities.Customer>();
        CreateMap<Models.CustomerForUpdateWithAddressesDto, Entities.Customer>();
        CreateMap<Entities.Customer, Entities.Customer>();

        CreateMap<Entities.Customer, Univali.Api.Features.Customers.Queries.GetCustomerDetail.GetCustomerDetailDto>();
        CreateMap<Univali.Api.Features.Customers.Commands.CreateCustomer.CreateCustomerCommand, Entities.Customer>();
        CreateMap<Entities.Customer, Univali.Api.Features.Customers.Commands.CreateCustomer.CreateCustomerDto>();
    }
}