using Bunit;
using ListGenerator.Client.Pages;
using ListGenerator.Client.Services;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace ListGenerator.Web.UnitTests.SpinnerTests
{
    [TestFixture]
    public class ShowSpinnerTests : BUnitTestContext
    {
        [Test]
        public void Should_HaveEmptyMarkUp_When_SpinnerIsNotShown()
        {
            //Arrange
            var mockSpinnerService = new Mock<ISpinnerService>();
            Services.AddSingleton(mockSpinnerService.Object);

            //Act
            var cut = RenderComponent<Spinner>();

            // Assert
            cut.MarkupMatches(string.Empty);
        }

        [Test]
        public void Should_HaveDivWithSpinnerContainerClassWithElements_When_SpinnerIsShown()
        {
            //Arrange
            var mockSpinnerService = new Mock<ISpinnerService>();
            Services.AddSingleton(mockSpinnerService.Object);

            //Act
            var cut = RenderComponent<Spinner>();
            cut.InvokeAsync(() => cut.Instance.ShowSpinner());


            // Assert
            var renderedMarkup = cut.Find(".spinner-container");
            Assert.IsTrue(renderedMarkup.ChildElementCount > 0);
        }
    }
}
