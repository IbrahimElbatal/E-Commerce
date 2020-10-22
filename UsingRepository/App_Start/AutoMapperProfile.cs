using AutoMapper;
using UsingRepository.Core.Models;
using UsingRepository.Core.ViewModels;

namespace UsingRepository
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Product, ProductViewModel>(MemberList.None);
            CreateMap<ProductViewModel, Product>(MemberList.None);

            CreateMap<Category, CategoryViewModel>(MemberList.None);
            CreateMap<CategoryViewModel, Category>(MemberList.None);

            CreateMap<SliderViewModel, Slider>(MemberList.None);
            CreateMap<Slider, SliderViewModel>(MemberList.None);

            CreateMap<ProductDiscount, ProductDiscount>(MemberList.None);
        }
    }
}