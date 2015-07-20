using System;
using System.Collections.Generic;
using System.Data;
using Orchard.ContentManagement.Drivers;
using Orchard.ContentManagement.MetaData;
using Orchard.ContentManagement.MetaData.Builders;
using Orchard.Core.Contents.Extensions;
using Orchard.Data.Migration;

namespace VirtoCommerce.Webshop {
    public class Migrations : DataMigrationImpl {

        public int Create() {
			// Creating table ProductListPartRecord
			SchemaBuilder.CreateTable("ProductListPartRecord", table => table
				.ContentPartRecord()
				.Column("PageSize", DbType.Int32)
			);

            ContentDefinitionManager.AlterTypeDefinition("CategoryList", cfg => cfg
                .WithPart("CategoryListPart")
                .WithPart("WidgetPart")
                .WithPart("CommonPart")
                .WithSetting("Stereotype", "Widget"));

            ContentDefinitionManager.AlterTypeDefinition("ProductList", cfg => cfg
                .WithPart("ProductListPart")
                .WithPart("WidgetPart")
                .WithPart("CommonPart")
                .WithSetting("Stereotype", "Widget"));

            ContentDefinitionManager.AlterTypeDefinition("Product", cfg => cfg
                .WithPart("ProductPart")
                .WithPart("WidgetPart")
                .WithPart("CommonPart")
                .WithSetting("Stereotype", "Widget"));

            return 1;
        }
    }
}