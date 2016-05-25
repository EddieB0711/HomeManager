using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Input;
using HomeManager.Books.Business;
using HomeManager.Books.Module.Models;
using HomeManager.Books.Module.Views;
using HomeManager.Infrastructure.Extensions;
using HomeManager.Infrastructure.MVVM.Attributes;
using HomeManager.Infrastructure.MVVM.Commands;
using HomeManager.Infrastructure.RegionNames;
using HomeManager.Infrastructure.Services.Repositories.Json;
using Prism.Mvvm;

namespace HomeManager.Books.Module.ViewModels
{
    [DependentViewModel(typeof(BooksNavigationView), Regions.TopRegion)]
    public class BooksWorkspaceViewModel : BindableBase
    {
        private WorkspaceModel _model;
        private IJsonRepository<BookRecord> _bookRepository;
        private IJsonRepository<AuthorRecord> _authorRepository;
        private IJsonRepository<GenreRecord> _genreRepository;
        
        public BooksWorkspaceViewModel(IJsonRepository<BookRecord> bookRepository,
            IJsonRepository<AuthorRecord> authorRepository,
            IJsonRepository<GenreRecord> genreRepository)
        {
            bookRepository.NullGuard();
            authorRepository.NullGuard();
            genreRepository.NullGuard();

            _bookRepository = bookRepository;
            _authorRepository = authorRepository;
            _genreRepository = genreRepository;

            LoadedCommand = AsyncCommandBuilder.Create(LoadBooks);
        }

        public WorkspaceModel Model
        {
            get
            {
                return _model;
            }

            set
            {
                SetProperty(ref _model, value);
            }
        }

        public ICommand LoadedCommand { get; set; }

        private async Task LoadBooks()
        {
            Model = ConvertToModel(await _bookRepository.GetAllAsync(),
                await _authorRepository.GetAllAsync(),
                await _genreRepository.GetAllAsync());
        }

        private WorkspaceModel ConvertToModel(IEnumerable<BookRecord> books, IEnumerable<AuthorRecord> authors, IEnumerable<GenreRecord> genres)
        {
            return WorkspaceModel.Create(books, authors, genres);
        }
    }
}