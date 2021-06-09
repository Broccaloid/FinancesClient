using System.Collections.Generic;
using Xunit;
using Moq;
using FinancesClient.Data;
using FinancesClient.Services;
using FinancesClient.Shared;
using Bunit;
using System;

namespace FinancesClient.Tests
{
    public class FinancialStatementOutputTests : TestContext
    {
        [Fact]
        public void FinancialStatementOutputGetsFinancialStatement()
        {
            // Arrange
            var statement = new FinancialStatement() { FinancialOperations = new List<FinancialOperation>() { new FinancialOperation() { Id = 1, BalanceChange = 1000, Date = new DateTime(2001, 11, 29), Type = "type" } }, TotalExpense = 0, TotalIncome = 1000 };

            // Act
            var cut = RenderComponent<FinancialStatementOutput>(parameters => parameters.Add(p => p.Statement, statement));
            var tableRow = cut.Find(".data-output");
            var incomeExpenses = cut.Find("p");

            // Assert
            tableRow.MarkupMatches(@"<tr class=""data-output"">
                <td class=""date"">29.11.2001</td>
                <td class=""type"">type</td>
                <td class=""balance-change"">1000</td>
                </tr>");
            incomeExpenses.MarkupMatches(@"<p>
                Total income = 1000 <br />
                Total expenses = 0
                </p>");
        }
    }
}
