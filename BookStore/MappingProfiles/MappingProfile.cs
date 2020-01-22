using AutoMapper;
using BookStore.Domain.Entities;
using BookStore.WebAPI.Converters;
using BookStore.WebAPI.ViewModels;
using BookStore.WebAPI.ViewModels.DetailedViewModels;
using BookStore.WebAPI.ViewModels.SimplifiedViewModels;
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
            CreateMap<IFormFile, byte[]>().ConvertUsing(new FormFileToByteArrayConverter());
            CreateMap<byte[], IFormFile>().ConvertUsing(new ByteArrayToFormFileConverter());

            CreateMap<UserRegisterViewModel, User>();

            CreateMap<User, UserDetailedViewModel>();

            CreateMap<BookViewModel, Book>();
            CreateMap<Book, BookDetailedViewModel>();
            CreateMap<Book, BookViewModel>();

            CreateMap<AuthorViewModel, Author>();
            CreateMap<Author, AuthorDetailedViewModel>();
            CreateMap<Author, AuthorViewModel>();

            CreateMap<CategoryViewModel, Category>();
            CreateMap<Category, CategoryViewModel>();

            CreateMap<BookReviewViewModel, BookReview>();
            CreateMap<BookReview, BookReviewViewModel>();

            CreateMap<FavoriteBookViewModel, FavoriteBook>();
            CreateMap<FavoriteBook, FavoriteBookViewModel>();
        }
    }
}
