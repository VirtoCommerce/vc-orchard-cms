using System;
using System.Collections.Generic;
using System.Data;
using Orchard.ContentManagement.Drivers;
using Orchard.ContentManagement.MetaData;
using Orchard.ContentManagement.MetaData.Builders;
using Orchard.Core.Contents.Extensions;
using Orchard.Data.Migration;

namespace VirtoCommerce.Webshop {
    public class Migrations : DataMigrationImpl
    {
        public int Create()
        {
            ContentDefinitionManager.AlterTypeDefinition("CategoryListWidget", cfg => cfg
                .WithPart("CategoryListPart")
                .WithPart("WidgetPart")
                .WithPart("CommonPart")
                .WithSetting("Stereotype", "Widget"));

            return 1;
        }

        public int UpdateFrom1()
        {
            ContentDefinitionManager.AlterTypeDefinition("ProductListWidget", cfg => cfg
                .WithPart("ProductListPart")
                .WithPart("WidgetPart")
                .WithPart("CommonPart")
                .WithSetting("Stereotype", "Widget"));

            return 2;
        }

        public int UpdateFrom2()
        {
            ContentDefinitionManager.AlterTypeDefinition("ProductWidget", cfg => cfg
                .WithPart("ProductPart")
                .WithPart("WidgetPart")
                .WithPart("CommonPart")
                .WithSetting("Stereotype", "Widget"));

            return 3;
        }

        public int UpdateFrom3()
        {
            ContentDefinitionManager.AlterTypeDefinition("ShoppingCartPreviewWidget", cfg => cfg
                .WithPart("ShoppingCartPreviewPart")
                .WithPart("WidgetPart")
                .WithPart("CommonPart")
                .WithSetting("Stereotype", "Widget"));

            return 4;
        }

        public int UpdateFrom4()
        {
            ContentDefinitionManager.AlterTypeDefinition("ShoppingCartWidget", cfg => cfg
                .WithPart("ShoppingCartPart")
                .WithPart("WidgetPart")
                .WithPart("CommonPart")
                .WithSetting("Stereotype", "Widget"));

            return 5;
        }

        public int UpdateFrom5()
        {
            ContentDefinitionManager.AlterTypeDefinition("CheckoutWidget", cfg => cfg
                .WithPart("CheckoutPart")
                .WithPart("WidgetPart")
                .WithPart("CommonPart")
                .WithSetting("Stereotype", "Widget"));

            return 6;
        }

        public int UpdateFrom6()
        {
            ContentDefinitionManager.AlterTypeDefinition("ThanksWidget", cfg => cfg
                .WithPart("ThanksPart")
                .WithPart("WidgetPart")
                .WithPart("CommonPart")
                .WithSetting("Stereotype", "Widget"));

            return 7;
        }
    }
}