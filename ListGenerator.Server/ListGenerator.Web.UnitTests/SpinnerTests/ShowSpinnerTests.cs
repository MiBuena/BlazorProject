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
    public class ShowSpinnerTests
    {
        [Test]
        public void Should_HaveEmptyMarkUp_When_SpinnerIsNotShown()
        {
            //Arrange
            using var ctx = new Bunit.TestContext();
            var mockSpinnerService = new Mock<SpinnerService>();
            ctx.Services.AddSingleton(mockSpinnerService.Object);

            //Act
            var cut = ctx.RenderComponent<Spinner>();

            // Assert
            cut.MarkupMatches(string.Empty);
        }

        [Test]
        public void Should_HaveDivWithSpinnerContainerClass_When_SpinnerIsShown()
        {
            //Arrange
            using var ctx = new Bunit.TestContext();
            var mockSpinnerService = new Mock<SpinnerService>();
            ctx.Services.AddSingleton(mockSpinnerService.Object);

            //Act
            var cut = ctx.RenderComponent<Spinner>();
            cut.InvokeAsync(() => cut.Instance.ShowSpinner());


            // Assert
            var renderedMarkup = cut.Find(".spinner-container");
            Assert.IsNotNull(renderedMarkup);
        }
    }
}
