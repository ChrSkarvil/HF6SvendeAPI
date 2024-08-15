using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using HF6Svende.Application.DTO;
using HF6Svende.Application.DTO.Country;
using HF6Svende.Application.DTO.Customer;
using HF6Svende.Application.DTO.Employee;
using HF6Svende.Application.DTO.Listing;
using HF6Svende.Application.DTO.Login;
using HF6Svende.Application.DTO.Product;
using HF6SvendeAPI.Data.Entities;

namespace HF6Svende.Application.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // Listings
            CreateMap<Listing, ListingDTO>()
                .ForMember(dest => dest.CustomerName, opt =>
                    opt.MapFrom(src => src.Customer.FirstName + " " + src.Customer.LastName))
                .ForMember(dest => dest.Product, opt =>
                    opt.MapFrom(src => new ProductInListingDTO
                    {
                        Id = src.Product.Id,
                        Brand = src.Product.Brand,
                        Description = src.Product.Description,
                        Size = src.Product.Size
                    }));


            // CreateListingDTO
            CreateMap<ListingCreateDTO, Listing>()
                .ForMember(dest => dest.Customer, opt => opt.Ignore())
                .ForMember(dest => dest.ProductId, opt => opt.MapFrom(src => src.ProductId));

            // UpdateListingDto
            CreateMap<ListingUpdateDTO, Listing>()
                .ForMember(dest => dest.Product, opt => opt.Ignore());

            // Mapping for Product
            CreateMap<ListingUpdateDTO, Product>()
                .ForMember(dest => dest.Brand, opt => opt.MapFrom(src => src.Brand))
                .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description))
                .ForMember(dest => dest.Size, opt => opt.MapFrom(src => src.Size));

            // Products
            CreateMap<Product, ProductDTO>()
                .ForMember(dest => dest.CustomerName, opt =>
                    opt.MapFrom(src => src.Customer.FirstName + " " + src.Customer.LastName))
                .ForMember(dest => dest.CategoryName, opt => opt.MapFrom(src => src.Category.Name));

            // CreateProductDTO
            CreateMap<ProductCreateDTO, Product>()
                .ForMember(dest => dest.CustomerId, opt => opt.MapFrom(src => src.CustomerId))
                .ForMember(dest => dest.CategoryId, opt => opt.MapFrom(src => src.CategoryId));

            // CreateProductDTO
            CreateMap<ProductUpdateDTO, Product>()
                .ForMember(dest => dest.CategoryId, opt => opt.MapFrom(src => src.CategoryId));

            // Customers
            CreateMap<Customer, CustomerDTO>()
                .ForMember(dest => dest.PostalCode, opt => opt.MapFrom(src => src.PostalCode.PostCode))
                .ForMember(dest => dest.CountryName, opt => opt.MapFrom(src => src.Country.Name));

            // CreateCustomerDTO
            CreateMap<CustomerCreateDTO, Customer>()
                .ForMember(dest => dest.PostalCodeId, opt => opt.MapFrom(src => src.PostalCodeId))
                .ForMember(dest => dest.CountryId, opt => opt.MapFrom(src => src.CountryId))
                .ForMember(dest => dest.Country, opt => opt.Ignore())
                .ForMember(dest => dest.PostalCode, opt => opt.Ignore());

            // UpdateCustomerDTO
            CreateMap<CustomerUpdateDTO, Customer>()
                .ForMember(dest => dest.PostalCodeId, opt => opt.MapFrom(src => src.PostalCodeId))
                .ForMember(dest => dest.CountryId, opt => opt.MapFrom(src => src.CountryId))
                .ForMember(dest => dest.Country, opt => opt.Ignore())
                .ForMember(dest => dest.PostalCode, opt => opt.Ignore());

            // Countries
            CreateMap<Country, CountryDTO>();

            // Employees
            CreateMap<Employee, EmployeeDTO>()
                .ForMember(dest => dest.PostalCode, opt => opt.MapFrom(src => src.PostalCode.PostCode))
                .ForMember(dest => dest.CountryName, opt => opt.MapFrom(src => src.Country.Name))
                .ForMember(dest => dest.Role, opt => opt.MapFrom(src => src.Role.Name))
                .ForMember(dest => dest.Department, opt => opt.MapFrom(src => src.Department.Name));

            // CreateEmployeeDTO
            CreateMap<EmployeeCreateDTO, Employee>()
                .ForMember(dest => dest.PostalCodeId, opt => opt.MapFrom(src => src.PostalCodeId))
                .ForMember(dest => dest.CountryId, opt => opt.MapFrom(src => src.CountryId))
                .ForMember(dest => dest.RoleId, opt => opt.MapFrom(src => src.RoleId))
                .ForMember(dest => dest.DepartmentId, opt => opt.MapFrom(src => src.DepartmentId))
                .ForMember(dest => dest.Country, opt => opt.Ignore())
                .ForMember(dest => dest.PostalCode, opt => opt.Ignore())
                .ForMember(dest => dest.Role, opt => opt.Ignore())
                .ForMember(dest => dest.Department, opt => opt.Ignore());

            // UpdateEmployeeDTO
            CreateMap<EmployeeUpdateDTO, Employee>()
                .ForMember(dest => dest.PostalCodeId, opt => opt.MapFrom(src => src.PostalCodeId))
                .ForMember(dest => dest.CountryId, opt => opt.MapFrom(src => src.CountryId))
                .ForMember(dest => dest.RoleId, opt => opt.MapFrom(src => src.RoleId))
                .ForMember(dest => dest.DepartmentId, opt => opt.MapFrom(src => src.DepartmentId))
                .ForMember(dest => dest.Country, opt => opt.Ignore())
                .ForMember(dest => dest.PostalCode, opt => opt.Ignore())
                .ForMember(dest => dest.Role, opt => opt.Ignore())
                .ForMember(dest => dest.Department, opt => opt.Ignore());

            // Logins
            CreateMap<Login, LoginDTO>()
                .ForMember(dest => dest.UserType, opt => opt.MapFrom(src => (UserType)src.UserType))
                .ForMember(dest => dest.FullName, opt => opt.MapFrom(src =>
                    src.CustomerId.HasValue && src.Customer != null
                        ? src.Customer.FirstName + " " + src.Customer.LastName
                        : src.Employee != null
                        ? src.Employee.FirstName + " " + src.Employee.LastName 
                        : string.Empty))
                .ForMember(dest => dest.Role, opt => opt.MapFrom(src =>
                    src.CustomerId.HasValue
                        ? "Customer"
                        : src.Employee != null && src.Employee.Role != null
                        ? src.Employee.Role.Name
                        : string.Empty));

            // CreateLoginDTO
            CreateMap<LoginCreateDTO, Login>()
                .ForMember(dest => dest.EmployeeId, opt => opt.MapFrom(src => src.EmployeeId))
                .ForMember(dest => dest.CustomerId, opt => opt.MapFrom(src => src.CustomerId))
                .ForMember(dest => dest.UserType, opt => opt.MapFrom(src => (UserType)src.UserType))
                .ForMember(dest => dest.Password, opt => opt.Ignore());

            // UpdateLoginDTO
            CreateMap<LoginUpdateDTO, Login>()
                .ForMember(dest => dest.Password, opt => opt.Ignore());

        }
    }
}
