using System.Collections.Generic;
using Xunit;
using Moq;
using FinancesClient.Pages;
using FinancesClient.Data;
using FinancesClient.Services;
using FinancesClient.Shared;
using Bunit;
using System;
using Microsoft.JSInterop;
using Microsoft.Extensions.DependencyInjection;
using System.Threading.Tasks;
using FinancesClient.Services.UseCases;
using Microsoft.Extensions.Logging;
using FinancesClient.Services.UseCases.Implementations;

namespace FinancesClient.Tests
{
    public class IndexTests : TestContext
    {
        private FinancialOperation Operation { get; set; }
        private Mock<IFinancesService> MockFinancesService { get; set; }
        private Mock<IJSRuntime> MockJSRuntime { get; set; }
        private Mock<INoReturnUseCase> MockNoReturnUseCase { get; set; }
        private Mock<ILogger<Pages.Index>> MockLogger { get; set; }

        private Mock<IOperationsUseCase> MockOperationsUseCase { get; set; }
        public IndexTests()
        {
            Operation = new FinancialOperation() { Id = 1, BalanceChange = 1000, Date = new DateTime(2001, 11, 29), Type = "type" };

            MockNoReturnUseCase = new Mock<INoReturnUseCase>();
            MockNoReturnUseCase.Setup(mock => mock.Launch());

            MockOperationsUseCase = new Mock<IOperationsUseCase>();
            MockOperationsUseCase.Setup(mock => mock.Launch()).Returns(Task.FromResult(new List<FinancialOperation>() { Operation }));

            MockFinancesService = new Mock<IFinancesService>();
            MockFinancesService.Setup(mock => mock.ChangeOperation(Operation)).Returns(MockNoReturnUseCase.Object);
            MockFinancesService.Setup(mock => mock.DeleteOperation(Operation)).Returns(MockNoReturnUseCase.Object);
            MockFinancesService.Setup(mock => mock.GetAllOperations()).Returns(MockOperationsUseCase.Object);

            MockJSRuntime = new Mock<IJSRuntime>();

            MockLogger = new Mock<ILogger<Pages.Index>>();

            Services.AddSingleton<ILogger<Pages.Index>>(MockLogger.Object);
            Services.AddSingleton<IJSRuntime>(MockJSRuntime.Object);
            Services.AddSingleton<IFinancesService>(MockFinancesService.Object);
        }

        [Fact]
        public void ChangeButtonClickOpensChangeSection()
        {
            // Arrange
            
            // Act
            var cut = RenderComponent<Pages.Index>();
            var addButton = cut.Find("#change-button");
            addButton.Click();
            var POSTList = cut.Find(".change");

            // Assert
            POSTList.MarkupMatches(@"<div class=""change"">
        <h3>Please, enter new data for the operation</h3>
        <p>Date</p>
        <input value=""2001-11-29"" type = ""date"" />
        <br />
        <br />
        <p>Type</p>
        <input value=""type"" />
        <br />
        <br />
        <p>Balance Change</p>
        <input value=""1000"" />
        <br />
        <br />
        <button >Save changes</button>
        </div>");
        }

        [Fact]
        public void DeleteButtonClickDeletedLastOperation()
        {
            // Arrange

            // Act
            var cut = RenderComponent<Pages.Index>();
            var deleteButton = cut.Find("#delete-button");
            deleteButton.Click();
            var POSTList = cut.Find("tbody");

            // Assert
            POSTList.MarkupMatches(@"<tbody></tbody>");
        }
    }
}
