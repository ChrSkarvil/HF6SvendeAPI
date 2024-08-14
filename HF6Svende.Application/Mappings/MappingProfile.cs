﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using HF6Svende.Application.DTO;
using HF6Svende.Application.DTO.Customer;
using HF6Svende.Application.DTO.Listing;
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
                .ForMember(dest => dest.CountryCode, opt => opt.MapFrom(src => src.Country.CountryCode));
            //.ForMember(dest => dest.PostalCode, opt => opt.MapFrom(src => src.PostalCode.PostCode));

            // CreateCustomerDTO
            CreateMap<CustomerCreateDTO, Customer>()
                .ForMember(dest => dest.PostalCodeId, opt => opt.MapFrom(src => src.PostalCodeId))
                .ForMember(dest => dest.CountryId, opt => opt.MapFrom(src => src.CountryId))
                .ForMember(dest => dest.PostalCode, opt => opt.Ignore());

            // UpdateCustomerDTO
            CreateMap<CustomerUpdateDTO, Customer>()
                .ForMember(dest => dest.PostalCodeId, opt => opt.MapFrom(src => src.PostalCodeId))
                .ForMember(dest => dest.CountryId, opt => opt.MapFrom(src => src.CountryId))
                .ForMember(dest => dest.PostalCode, opt => opt.Ignore());
        }
    }
}
