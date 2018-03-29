﻿using Neptuo;
using Neptuo.Queries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Money.Services.Models.Queries
{
    /// <summary>
    /// Gets a symbol for currency with <see cref="UniqueCode"/>.
    /// </summary>
    public class GetCurrencySymbol : IQuery<string>
    {
        /// <summary>
        /// Gets an unique code of the currency.
        /// </summary>
        public string UniqueCode { get; private set; }

        /// <summary>
        /// Creates a new instance.
        /// </summary>
        /// <param name="uniqueCode">An unique code of the currency.</param>
        public GetCurrencySymbol(string uniqueCode)
        {
            Ensure.NotNullOrEmpty(uniqueCode, "uniqueCode");
            UniqueCode = uniqueCode;
        }
    }
}