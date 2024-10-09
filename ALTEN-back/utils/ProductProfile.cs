using AutoMapper;

public class ProductProfile : Profile
{
    public ProductProfile()
    {
        CreateMap<CreateProductDto, Product>();

        CreateMap<UpdateProductDto, Product>()
            .ForAllMembers(opts => opts.Condition((src, dest, srcMember) =>
                (srcMember is not string || !string.IsNullOrEmpty((string)srcMember))
                && (srcMember is not int || (int)srcMember != 0)                      
                && (srcMember != null)
        ));
    }
}