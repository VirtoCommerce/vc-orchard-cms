using Orchard.Mvc.Routes;
using System;
using System.Collections.Generic;
using System.Web.Mvc;
using System.Web.Routing;

namespace VirtoCommerce.WebStore
{
    public class Routes : IRouteProvider
    {
        public IEnumerable<RouteDescriptor> GetRoutes()
        {
            return new[]
            {
                new RouteDescriptor
                {
                    Priority = 99,
                    Route = new Route
                    (
                        "",
                        new RouteValueDictionary
                        {
                            { "area", "VirtoCommerce.WebStore" },
                            { "controller", "Catalog" },
                            { "action", "Category" }
                        },
                        new RouteValueDictionary(),
                        new RouteValueDictionary
                        {
                            { "area", "VirtoCommerce.WebStore" }
                        },
                        new MvcRouteHandler()
                    )
                },
                new RouteDescriptor
                {
                    Route = new Route
                    (
                        "category/{slug}",
                        new RouteValueDictionary
                        {
                            { "area", "VirtoCommerce.WebStore" },
                            { "controller", "Catalog" },
                            { "action", "Category" }
                        },
                        new RouteValueDictionary(),
                        new RouteValueDictionary
                        {
                            { "area", "VirtoCommerce.WebStore" }
                        },
                        new MvcRouteHandler()
                    )
                },
                new RouteDescriptor
                {
                    Route = new Route
                    (
                        "category/{slug}/{page}",
                        new RouteValueDictionary
                        {
                            { "area", "VirtoCommerce.WebStore" },
                            { "controller", "Catalog" },
                            { "action", "Category" }
                        },
                        new RouteValueDictionary(),
                        new RouteValueDictionary
                        {
                            { "area", "VirtoCommerce.WebStore" }
                        },
                        new MvcRouteHandler()
                    )
                },
                new RouteDescriptor
                {
                    Route = new Route
                    (
                        "product/{slug}",
                        new RouteValueDictionary
                        {
                            { "area", "VirtoCommerce.WebStore" },
                            { "controller", "Catalog" },
                            { "action", "Product" }
                        },
                        new RouteValueDictionary(),
                        new RouteValueDictionary
                        {
                            { "area", "VirtoCommerce.WebStore" }
                        },
                        new MvcRouteHandler()
                    )
                },
                new RouteDescriptor
                {
                    Route = new Route
                    (
                        "cart",
                        new RouteValueDictionary
                        {
                            { "area", "VirtoCommerce.WebStore" },
                            { "controller", "ShoppingCart" },
                            { "action", "Index" }
                        },
                        new RouteValueDictionary(),
                        new RouteValueDictionary
                        {
                            { "area", "VirtoCommerce.WebStore" }
                        },
                        new MvcRouteHandler()
                    )
                },
                new RouteDescriptor
                {
                    Route = new Route
                    (
                        "checkout",
                        new RouteValueDictionary
                        {
                            { "area", "VirtoCommerce.WebStore" },
                            { "controller", "Checkout" },
                            { "action", "Index" }
                        },
                        new RouteValueDictionary(),
                        new RouteValueDictionary
                        {
                            { "area", "VirtoCommerce.WebStore" }
                        },
                        new MvcRouteHandler()
                    )
                },
                new RouteDescriptor
                {
                    Route = new Route
                    (
                        "thanks",
                        new RouteValueDictionary
                        {
                            { "area", "VirtoCommerce.WebStore" },
                            { "controller", "Checkout" },
                            { "action", "Thanks" }
                        },
                        new RouteValueDictionary(),
                        new RouteValueDictionary
                        {
                            { "area", "VirtoCommerce.WebStore" }
                        },
                        new MvcRouteHandler()
                    )
                }
            };
        }

        public void GetRoutes(ICollection<RouteDescriptor> routes)
        {
            throw new NotImplementedException();
        }
    }
}