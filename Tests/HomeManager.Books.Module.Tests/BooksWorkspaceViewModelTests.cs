using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using FluentAssertions;
using HomeManager.Books.Business;
using HomeManager.Books.Module.ViewModels;
using HomeManager.Infrastructure.Services.Repositories.Json;
using Moq;
using NUnit.Framework;

namespace HomeManager.Books.Module.Tests
{
    [TestFixture]
    public class BooksWorkspaceViewModelTests
    {
        [Test]
        public void When_ViewModel_is_constructed_should_not_throw()
        {
            var bookRepoMock = new Mock<IJsonRepository<BookRecord>>();
            var authorRepoMock = new Mock<IJsonRepository<AuthorRecord>>();
            var genreRepoMock = new Mock<IJsonRepository<GenreRecord>>();

            bookRepoMock.Setup(repo => repo.GetAllAsync()).Returns(
                Task.Run(
                    () =>
                        {
                            return (IList<BookRecord>)new List<BookRecord>
                                {
                                    new BookRecord { Id = 0, Title = "Testing" }
                                };
                        }));
            authorRepoMock.Setup(repo => repo.GetAllAsync()).Returns(
                Task.Run(
                    () =>
                        {
                            return (IList<AuthorRecord>)new List<AuthorRecord>
                                {
                                    new AuthorRecord { Id = 0, FirstName = "Testing", LastName = "Testing" }
                                };
                        }));
            genreRepoMock.Setup(repo => repo.GetAllAsync()).Returns(
                Task.Run(
                    () =>
                        {
                            return (IList<GenreRecord>)new List<GenreRecord>
                                {
                                    new GenreRecord { Id = 0, Genre = "Testing" }
                                };
                        }));

            Action action = () => new BooksWorkspaceViewModel(bookRepoMock.Object, authorRepoMock.Object, genreRepoMock.Object);

            action.ShouldNotThrow();
        }

        [Test]
        public void When_two_books_are_in_repository_ViewModel_should_load_two_books()
        {
            var bookRepoMock = new Mock<IJsonRepository<BookRecord>>();
            var authorRepoMock = new Mock<IJsonRepository<AuthorRecord>>();
            var genreRepoMock = new Mock<IJsonRepository<GenreRecord>>();

            bookRepoMock.Setup(repo => repo.GetAllAsync()).Returns(
                Task.Run(
                    () =>
                        {
                            return (IList<BookRecord>)new List<BookRecord>
                                {
                                    new BookRecord { Id = 0, Title = "Testing 1" },
                                    new BookRecord { Id = 1, Title = "Testing 2" }
                                };
                        }));
            authorRepoMock.Setup(repo => repo.GetAllAsync()).Returns(
                Task.Run(
                    () =>
                        {
                            return (IList<AuthorRecord>)new List<AuthorRecord>
                                {
                                    new AuthorRecord { Id = 0, FirstName = "Testing", LastName = "Testing" }
                                };
                        }));
            genreRepoMock.Setup(repo => repo.GetAllAsync()).Returns(
                Task.Run(
                    () =>
                        {
                            return (IList<GenreRecord>)new List<GenreRecord>
                                {
                                    new GenreRecord { Id = 0, Genre = "Testing" }
                                };
                        }));

            var vm = new BooksWorkspaceViewModel(bookRepoMock.Object, authorRepoMock.Object, genreRepoMock.Object);
            vm.LoadedCommand.Execute(null);

            vm.Model.Books.Should().HaveCount(2);
        }
    }
}