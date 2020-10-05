﻿using Bunit;
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
    public class HideSpinnerTests : BUnitTestContext
    {
        [Test]
        public void Should_HaveEmptyMarkUp_When_SpinnerIsShownAndThenHidden()
        {
            //Act
            var cut = RenderComponent<Spinner>();
            cut.InvokeAsync(() => cut.Instance.ShowSpinner());
            cut.InvokeAsync(() => cut.Instance.HideSpinner());

            // Assert
            cut.MarkupMatches(string.Empty);
        }
    }
}