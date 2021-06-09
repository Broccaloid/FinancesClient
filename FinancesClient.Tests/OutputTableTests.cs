using Bunit;
using FinancesClient.Data;
using System;
using System.Collections.Generic;
using Xunit;
using Moq;
using FinancesClient.Services;
using FinancesClient.Shared;

namespace FinancesClient.Tests
{
    public class OutputTableTests : TestContext
    {
        [Fact]
        public void OutputTableGetsData()
        {
            // Arrange
            var list = new List<FinancialOperation>() { new FinancialOperation() { Id = 1, BalanceChange = 1000, Date = new DateTime(2001, 11, 29), Type = "type" } };

            // Act
            var cut = RenderComponent<OutputTable>(parameters => parameters.Add(p => p.Operations, list));
            var tableRow = cut.Find(".data-output");

            // Assert
            tableRow.MarkupMatches(@"<tr class=""data-output"">
                <td class=""date"">29.11.2001</td>
                <td class=""type"">type</td>
                <td class=""balance-change"">1000</td>
            </tr>");
        }
    }
}
