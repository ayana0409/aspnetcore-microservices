﻿using Shared.Configurations;

namespace Inventory.Product.API.Extensions
{
    public class DatabaseSettings : Shared.Configurations.DatabaseSettings
    {
        public string DatabaseName { get; set; } = string.Empty;
    }
}
