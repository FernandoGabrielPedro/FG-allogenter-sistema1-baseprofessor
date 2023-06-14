using AutoMapper;

namespace Univali.Api.Profiles;

public class AddressProfile : Profile {
    public AddressProfile() {
        //1º arg: objeto de origem
        //2º arg: objeto de destino
        CreateMap<Entities.Address, Models.AddressDto>();
        CreateMap<Models.AddressForCreationDto, Entities.Address>();
        CreateMap<Models.AddressForUpdateDto, Entities.Address>();
        CreateMap<Entities.Address, Entities.Address>();

        CreateMap<Entities.Address, Features.Addresses.Queries.GetAddressesDetail.GetAddressesDetailDto>();
        CreateMap<Entities.Address, Features.Addresses.Queries.GetAddressDetail.GetAddressDetailDto>();
        CreateMap<Entities.Address, Features.Addresses.Queries.GetAddressesDetailByCustomerId.GetAddressesDetailByCustomerIdDto>();
        CreateMap<Univali.Api.Features.Addresses.Commands.CreateAddress.CreateAddressCommand, Entities.Address>();
        CreateMap<Entities.Address, Univali.Api.Features.Addresses.Commands.CreateAddress.CreateAddressDto>();
        CreateMap<Univali.Api.Features.Addresses.Commands.UpdateAddress.UpdateAddressCommand, Entities.Address>();
    }
}