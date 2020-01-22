using AutoMapper;
using BookStore.Domain.Entities;
using BookStore.WebAPI.Converters;
using BookStore.WebAPI.ViewModels;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.WebAPI.MappingProfiles
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<UserRegisterViewModel, User>();
            CreateMap<User, UserRegisterViewModel>();

            CreateMap<BookViewModel, Book>();
            CreateMap<Book, BookViewModel>();

            CreateMap<CategoryViewModel, Category>();
            CreateMap<Category, CategoryViewModel>();

            CreateMap<AuthorViewModel, Author>();
            CreateMap<Author, AuthorViewModel>();

            CreateMap<BookReviewViewModel, BookReview>();
            CreateMap<BookReview, BookReviewViewModel>();

            CreateMap<FavoriteBookViewModel, FavoriteBook>();
            CreateMap<FavoriteBook, FavoriteBookViewModel>();

            CreateMap<IFormFile, byte[]>().ConvertUsing(new FormFileToByteArrayConverter());
            CreateMap<byte[], IFormFile>().ConvertUsing(new ByteArrayToFormFileConverter());
        }
    }
}
