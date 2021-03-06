﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using Keane.CH.Framework.Core.ExtensionMethods;
using Keane.CH.Framework.Services.Entity.Contracts.Data;

namespace Keane.CH.Framework.Services.Resources.Contracts
{
    /// <summary>
    /// Represents an binary resource collection within a system.
    /// </summary>
    [CollectionDataContract(Namespace = "www.Keane.com/CH/2009/01/Services/Resources")]
    [Serializable]
    public class BinaryResourceCollection :
        EntityBaseCollection<BinaryResource>
    {
        #region Methods

        /// <summary>
        /// Returns a collection of resources matched by category.
        /// </summary>
        /// <param name="category">The category whose resources are being sought.</param>
        /// <returns>A collection of resources mathced by category.</returns>
        public EntityBaseCollection<BinaryResource> MatchByCategory(string category)
        {
            var result =
                from
                    resource in this
                where
                    resource.Category.Trim().ToUpperInvariant().Equals(category.Trim().ToUpperInvariant())
                select
                    resource;
            return result.AsCustomCollection<EntityBaseCollection<BinaryResource>, BinaryResource>();
        }

        /// <summary>
        /// Returns a collection of resources matched by category & sub-category.
        /// </summary>
        /// <param name="category">The category whose resources are being sought.</param>
        /// <param name="subCategory">The sub-category whose resources are being sought.</param>
        /// <returns>A collection of resources mathced by category.</returns>
        public EntityBaseCollection<BinaryResource> MatchBySubCategory(string category, string subCategory)
        {
            var result =
                from
                    resource in this
                where
                    resource.Category.Trim().ToUpperInvariant().Equals(category.Trim().ToUpperInvariant()) &&
                    resource.SubCategory.Trim().ToUpperInvariant().Equals(subCategory.Trim().ToUpperInvariant())
                select
                    resource;
            return result.AsCustomCollection<EntityBaseCollection<BinaryResource>, BinaryResource>();
        }

        #endregion Methods
    }
}
