using System.Collections.Generic;
using Xunit;
using Moq;
using FinancesClient.Data;
using FinancesClient.Services;
using FinancesClient.Shared;
using Bunit;
using System;
using Microsoft.JSInterop;
using Microsoft.Extensions.DependencyInjection;
using FinancesClient.Pages;

namespace FinancesClient.Tests
{
    public class AddOperationsTests : TestContext
    {
        [Fact]
        public void AddButtonClickAddsItemToList()
        {
            // Arrange
            var mockFinancesService = new Mock<IFinancesService>();
            var mockJSRuntime = new Mock<IJSRuntime>();

            Services.AddSingleton<IJSRuntime>(mockJSRuntime.Object);
            Services.AddSingleton<IFinancesService>(mockFinancesService.Object);

            // Act
            var cut = RenderComponent<AddOperations>();
            var addButton = cut.Find(".add-button");
            addButton.Click();
            var POSTList = cut.Find(".data-output");

            // Assert
            POSTList.MarkupMatches(@"<tr class=""data-output"">
                <td class=""date"">01.01.0001</td>
                <td class=""type""></td>
                <td class=""balance-change"">0</td>
                </tr>");
        }
    }
}
