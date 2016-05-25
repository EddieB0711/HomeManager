using System;
using FluentAssertions;
using HomeManager.Books.Module.ViewModels;
using HomeManager.Infrastructure.Events;
using HomeManager.Infrastructure.MVVM.Events;
using Moq;
using NUnit.Framework;

namespace HomeManager.Books.Module.Tests
{
    [TestFixture]
    public class BooksNavigationViewModelTests
    {
        [Test]
        public void When_ViewModel_is_constructed_should_not_throw()
        {
            var bookNavigationEventMock = new Mock<ModuleChangingEvent>();
            var eventAggregateMock = new Mock<IAsyncEventAggregator>();

            eventAggregateMock.Setup(ea => ea.GetEvent<ModuleChangingEvent>()).Returns(bookNavigationEventMock.Object);

            Action action = () => new BooksNavigationViewModel(eventAggregateMock.Object);

            action.ShouldNotThrow();
        }
    }
}