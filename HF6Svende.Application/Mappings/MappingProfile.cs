using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using HF6Svende.Application.DTO;
using HF6Svende.Application.DTO.Color;
using HF6Svende.Application.DTO.Country;
using HF6Svende.Application.DTO.Customer;
using HF6Svende.Application.DTO.Delivery;
using HF6Svende.Application.DTO.Employee;
using HF6Svende.Application.DTO.Image;
using HF6Svende.Application.DTO.Listing;
using HF6Svende.Application.DTO.Login;
using HF6Svende.Application.DTO.Order;
using HF6Svende.Application.DTO.Payment;
using HF6Svende.Application.DTO.Product;
using HF6SvendeAPI.Data.Entities;
using Microsoft.AspNetCore.Http;

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
                    opt.MapFrom(src => new ProductDTO
                    {
                        Id = src.Product.Id,
                        Brand = src.Product.Brand,
                        Description = src.Product.Description,
                        Size = src.Product.Size,
                        CategoryId = src.Product.CategoryId,
                        CategoryName = src.Product.Category.Name,
                        Gender = src.Product.Category.Gender.Name,
                        Images = src.Product.Images.Select(image => new ImageDTO
                        {
                            Id = image.Id,
                            FileBase64 = Convert.ToBase64String(image.File),
                            CreateDate = image.CreateDate,
                            IsVerified = image.IsVerified
                        }).ToList(),
                        Colors = src.Product.ProductColors.Select(pc => new ProductColorDTO
                        {
                            Id = pc.Color.Id,
                            Name = pc.Color.Name
                        }).ToList()
                    }));


            // CreateListingDTO
            CreateMap<ListingCreateDTO, Listing>()
                .ForMember(dest => dest.Product, opt => opt.Ignore())
                .ForMember(dest => dest.Customer, opt => opt.Ignore());

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
                 .ForMember(dest => dest.Images, opt =>
                opt.MapFrom(src => src.Images.Select(image => new ImageDTO
                {
                    Id = image.Id,
                    FileBase64 = Convert.ToBase64String(image.File),
                    CreateDate = image.CreateDate,
                    IsVerified = image.IsVerified
                }).ToList()))
                .ForMember(dest => dest.CategoryName, opt => opt.MapFrom(src => src.Category.Name))
                .ForMember(dest => dest.Gender, opt => opt.MapFrom(src => src.Category.Gender.Name))
                .ForMember(dest => dest.Colors, opt => opt.MapFrom(src => src.ProductColors.Select(pc => new ProductColorDTO
                {
                    Id = pc.Id,
                    Name = pc.Color.Name
                })));

            // CreateProductDTO
            CreateMap<ProductCreateDTO, Product>()
                .ForMember(dest => dest.Images, opt => opt.MapFrom(src => ConvertToImages(src.Images)))
                .ForMember(dest => dest.ProductColors, opt => opt.Ignore())
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

            // Colors
            CreateMap<Color, ColorDTO>();

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

            // LoginAuthDTO
            CreateMap<Login, LoginAuthDTO>()
                .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email))
                .ForMember(dest => dest.Password, opt => opt.MapFrom(src => src.Password));

            // CreateLoginDTO
            CreateMap<LoginCreateDTO, Login>()
                .ForMember(dest => dest.EmployeeId, opt => opt.MapFrom(src => src.EmployeeId))
                .ForMember(dest => dest.CustomerId, opt => opt.MapFrom(src => src.CustomerId))
                .ForMember(dest => dest.UserType, opt => opt.MapFrom(src => (UserType)src.UserType))
                .ForMember(dest => dest.Password, opt => opt.Ignore());

            // UpdateLoginDTO
            CreateMap<LoginUpdateDTO, Login>()
                .ForMember(dest => dest.Password, opt => opt.Ignore());

            // Images
            CreateMap<Image, ImageDTO>()
                .ForMember(dest => dest.FileBase64, opt => opt.MapFrom(src => Convert.ToBase64String(src.File)))
                .ForMember(dest => dest.ProductId, opt => opt.MapFrom(src => src.ProductId));

            // CreateImageDTO
            CreateMap<ImageCreateDTO, Image>()
                .ForMember(dest => dest.File, opt => opt.MapFrom(src => ConvertToBytes(src.File)))
                .ForMember(dest => dest.ProductId, opt => opt.MapFrom(src => src.ProductId));

            // UpdateImageDTO
            CreateMap<ImageUpdateDTO, Image>();

            // Orders
            CreateMap<Order, OrderDTO>()
                .ForMember(dest => dest.OrderItems, opt => opt.MapFrom(src => src.OrderItems))
                .ForMember(dest => dest.Customer, opt => opt.MapFrom(src => src.Customer))
                .ForMember(dest => dest.Payment, opt => opt.MapFrom(src => src.Payment))
                .ForMember(dest => dest.Delivery, opt => opt.MapFrom(src => src.Delivery));

            CreateMap<OrderCreateDTO, Order>()
                .ForMember(dest => dest.OrderItems, opt => opt.Ignore());


            // OrderItems
            CreateMap<OrderItem, OrderItemDTO>()
                .ForMember(dest => dest.Listing, opt => opt.MapFrom(src => src.Listing));

            // OrderItemCreate
            CreateMap<OrderItemCreateDTO, OrderItem>();

            // Payments
            CreateMap<Payment, PaymentDTO>();

            // Deliveries
            CreateMap<Delivery, DeliveryDTO>()
                .ForMember(dest => dest.PostalCode, opt => opt.MapFrom(src => src.PostalCode.PostCode))
                .ForMember(dest => dest.Country, opt => opt.MapFrom(src => src.Country.Name));





        }

        private byte[] ConvertToBytes(IFormFile file)
        {
            using (var memoryStream = new MemoryStream())
            {
                // Copy the uploaded file data to the memorystream
                file.CopyTo(memoryStream);

                // Convert it to byte array
                return memoryStream.ToArray();
            }
        }
        private List<Image> ConvertToImages(List<IFormFile> files)
        {
            var images = new List<Image>();
            foreach (var file in files)
            {
                images.Add(new Image
                {
                    File = ConvertToBytes(file)
                });
            }
            return images;
        }
    }
}
