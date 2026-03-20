using System;
using System.Collections.Generic;
using System.Linq;
using ExpenseManager.Models.Enums;

namespace ExpenseManager.ViewModels
{
    public static class EnumHelper
    {
        public static IEnumerable<string> GetAllCurrencies()
        {
            return Enum.GetNames(typeof(Currency));
        }

        public static IEnumerable<string> GetAllTransactionCategories()
        {
            return Enum.GetNames(typeof(TransactionCategory));
        }
    }
}