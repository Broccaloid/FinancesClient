using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FinancesClient.Data
{
    public class FinancialOperation
    {
        public int Id { get; set; }
        public decimal BalanceChange { get; set; }
        public string Type { get; set; }
        public DateTime Date { get; set; }
    }
}
