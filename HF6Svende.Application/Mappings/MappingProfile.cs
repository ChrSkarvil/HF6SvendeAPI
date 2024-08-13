﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using HF6Svende.Application.DTO;
using HF6Svende.Application.DTO.Listing;
using HF6SvendeAPI.Data.Entities;

namespace HF6Svende.Application.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // Listings
            CreateMap<Listing, ListingDto>()
                .ForMember(dest => dest.CustomerName, opt =>
                    opt.MapFrom(src => src.Customer.FirstName + " " + src.Customer.LastName))
                .ForMember(dest => dest.Product, opt =>
                    opt.MapFrom(src => new ProductDto
                    {
                        Id = src.Product.Id,
                        Brand = src.Product.Brand,
                        Description = src.Product.Description,
                        Size = src.Product.Size
                    }));

            // CreateListingDto
            CreateMap<CreateListingDto, Listing>()
            .ForMember(dest => dest.Customer, opt => opt.Ignore())
            .ForMember(dest => dest.ProductId, opt => opt.MapFrom(src => src.ProductId));

            // UpdateListingDto
            CreateMap<UpdateListingDto, Listing>()
                .ForMember(dest => dest.Product, opt => opt.Ignore());

            // Mapping for Product
            CreateMap<UpdateListingDto, Product>()
                .ForMember(dest => dest.Brand, opt => opt.MapFrom(src => src.Brand))
                .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description))
                .ForMember(dest => dest.Size, opt => opt.MapFrom(src => src.Size));
        }
    }
}
