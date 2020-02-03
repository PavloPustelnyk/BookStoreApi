using BookStore.WebAPI.ViewModels.SimplifiedViewModels;
using System.Collections.Generic;

namespace BookStore.WebAPI.ViewModels.DetailedViewModels
{
    public class BookDetailedViewModel : BaseViewModel
    {
        public string Title { get; set; }

        public int AuthorId { get; set; }

        public decimal Price { get; set; }

        public int ReviewCount { get; set; }

        public int SummaryRating { get; set; }

        public string BookImage { get; set; }

        public string Description { get; set; }

        public AuthorViewModel Author { get; set; }

        public ICollection<CategoryViewModel> Categories { get; set; }

        public ICollection<BookReviewViewModel> Reviews { get; set; }
    }
}
