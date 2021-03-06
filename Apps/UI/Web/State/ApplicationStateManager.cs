﻿using System;
using System.Diagnostics;
using System.Web;
using System.Web.UI;
using Keane.CH.Framework.Apps.UI.Web;
using Keane.CH.Framework.Core.Utilities.Security;

namespace Keane.CH.Framework.Apps.UI.Web.State
{
    /// <summary>
    /// Encapsulates application state management funtionality.
    /// </summary>
    public class ApplicationStateManager
    {
        #region Ctor

        internal ApplicationStateManager(WebPageBase page)
        {
            Page = page;
        }

        #endregion Ctor

        #region Properties

        /// <summary>
        /// Gets the page currently being processed.
        /// </summary>
        private WebPageBase Page
        { get; set; }

        /// <summary>
        /// Gets the asp.net application state manager.
        /// </summary>
        private HttpApplicationState ApplicationState
        { get { return Page.Application; } }

        #region Standard cache items

        /// <summary>
        /// Gets the application version.
        /// </summary>
        internal string CachedApplicationVersion
        {
            get
            {
                return GetItem<string>(ApplicationCacheDefaultItemType.ApplicationVersion);
            }
        }

        /// <summary>
        /// Gets the application release date.
        /// </summary>
        internal string CachedApplicationReleaseDate
        {
            get
            {
                return GetItem<string>(ApplicationCacheDefaultItemType.ApplicationReleaseDate);
            }
        }

        /// <summary>
        /// Gets the client name that should have been setup in the application initialisation routine.
        /// </summary>
        internal string CachedClientName
        {
            get
            {
                return GetItem<string>(ApplicationCacheDefaultItemType.ClientName);
            }
        }

        /// <summary>
        /// Gets the config root folder path.
        /// </summary>
        internal string CachedConfigRootFolderPath
        {
            get
            {
                return GetItem<string>(ApplicationCacheDefaultItemType.ConfigRootFolderPath);
            }
        }

        /// <summary>
        /// Gets the default client email address.
        /// </summary>
        internal string DefaultClientEmailAddress
        {
            get
            {
                return GetItem<string>(ApplicationCacheDefaultItemType.DefaultClientEmailAddress);
            }
        }

        /// <summary>
        /// Gets the default culture that should have been setup in the application initialisation routine.
        /// </summary>
        internal string CachedDefaultCulture
        {
            get
            {
                return GetItem<string>(ApplicationCacheDefaultItemType.DefaultUserCulture);
            }
        }

        /// <summary>
        /// Gets the default culture id (should have been setup in the application initialisation routine).
        /// </summary>
        internal int CachedDefaultCultureId
        {
            get
            {
                return Convert.ToInt32(GetItem(ApplicationCacheDefaultItemType.DefaultUserCultureId));
            }
        }

        /// <summary>
        /// Gets the default user role type id.
        /// </summary>
        internal int CachedDefaultUserRoleId
        {
            get
            {
                return Convert.ToInt32(GetItem(ApplicationCacheDefaultItemType.DefaultUserRoleId));
            }
        }

        /// <summary>
        /// Gets the default user meta data.
        /// </summary>
        internal object CachedDefaultUserMetaData
        {
            get
            {
                return GetItem(ApplicationCacheDefaultItemType.DefaultUserMetaData);
            }
        }

        /// <summary>
        /// Gets the default UI culture that should have been setup in the application initialisation routine.
        /// </summary>
        internal string CachedDefaultUICulture
        {
            get
            {
                return GetItem<string>(ApplicationCacheDefaultItemType.DefaultUserUICulture);
            }
        }

        /// <summary>
        /// Gets the default theme.
        /// </summary>
        internal string CachedDefaultTheme
        {
            get
            {
                return GetItem<string>(ApplicationCacheDefaultItemType.DefaultUserTheme);
            }
        }

        /// <summary>
        /// Gets the client meta data that should have been setup in the application initialisation routine.
        /// </summary>
        internal object CachedClientMetaData
        {
            get 
            {
                return GetItem(ApplicationCacheDefaultItemType.ClientMetaData) ;
            }
            set 
            {
                SetItem(ApplicationCacheDefaultItemType.ClientMetaData, value) ;
            }
        }

        /// <summary>
        /// Gets the application meta data that should have been setup in the application initialisation routine.
        /// </summary>
        internal object CachedApplicationMetaData
        {
            get
            {
                return GetItem(ApplicationCacheDefaultItemType.ApplicationMetaData);
            }
            set
            {
                SetItem(ApplicationCacheDefaultItemType.ApplicationMetaData, value);
            }
        }

        /// <summary>
        /// The number of authenticaton attempts after which a user is locked out.
        /// </summary>
        internal int CachedAuthenticationLockout
        {
            get
            {
                return Convert.ToInt32(GetItem(ApplicationCacheDefaultItemType.AuthenticationLockout));
            }
            set
            {
                SetItem(ApplicationCacheDefaultItemType.AuthenticationLockout, value);
            }
        }

        /// <summary>
        /// Gets the admin password minimum length.
        /// </summary>
        internal int CachedPasswordAdminMinLength
        {
            get
            {
                return Convert.ToInt32(GetItem(ApplicationCacheDefaultItemType.PasswordAdminMinLength));
            }
        }
       
        #endregion Standard cache items

        #endregion Properties

        #region Methods

        /// <summary>
        /// Clears the cache.
        /// </summary>
        public void Clear()
        {
            ApplicationState.Clear();
        }

        /// <summary>
        /// Determines whether the cache item is already cached.
        /// </summary>
        /// <param name="itemKey">The cache item key.</param>
        /// <returns>True if the item is already cached.</returns>
        internal bool IsCached(
            ApplicationCacheDefaultItemType itemKey)
        {            
            return IsCached(itemKey.ToString());
        }

        /// <summary>
        /// Determines whether the cache item is already cached.
        /// </summary>
        /// <param name="itemKey">The cache item key.</param>
        /// <returns>True if the item is already cached.</returns>
        public bool IsCached(
            string itemKey)
        {
            return (ApplicationState[itemKey] != null);
        }

        /// <summary>
        /// Generic method to retrieve an item from the session cache.
        /// </summary>
        /// <typeparam name="T">The type to be cast to.</typeparam>
        /// <param name="itemKey">The cache item key.</param>
        /// <returns>The cast cached item.</returns>
        internal T GetItem<T>(
            ApplicationCacheDefaultItemType itemKey)
        {
            return GetItem<T>(itemKey.ToString());
        }

        /// <summary>
        /// Generic method to retrieve an item from the session cache.
        /// </summary>
        /// <typeparam name="T">The type to be cast to.</typeparam>
        /// <param name="itemKey">The cache item key.</param>
        /// <returns>The cast cached item.</returns>
        public T GetItem<T>(
            string itemKey)
        {
            if ((!String.IsNullOrEmpty(itemKey)) &&
                (ApplicationState[itemKey] != null))
                return (T)ApplicationState[itemKey];
            else
                return default(T);
        }

        /// <summary>
        /// Retrieves an item from the session cache.
        /// </summary>
        /// <param name="itemKey">The cache item key.</param>
        /// <returns>The cached item.</returns>
        internal object GetItem(
            ApplicationCacheDefaultItemType itemKey)
        {
            return GetItem(itemKey.ToString());
        }

        /// <summary>
        /// Retrieves an item from the session cache.
        /// </summary>
        /// <param name="itemKey">The cache item key.</param>
        /// <returns>The cached item.</returns>
        public object GetItem(
            string itemKey)
        {
            if ((!String.IsNullOrEmpty(itemKey)) &&
                (ApplicationState[itemKey] != null))
                return ApplicationState[itemKey];
            else
                return null;
        }

        /// <summary>
        /// Returns the cached item.
        /// </summary>
        /// <param name="itemKey">The cache item key.</param>
        /// <param name="cacheItem">The item being cached.</param>
        internal void SetItem(
            ApplicationCacheDefaultItemType itemKey,
            object cacheItem)
        {
            SetItem(itemKey.ToString(), cacheItem);
        }

        /// <summary>
        /// Returns the cached item.
        /// </summary>
        /// <param name="itemKey">The cache item key.</param>
        /// <param name="cacheItem">The item being cached.</param>
        public void SetItem(
            string itemKey,
            object cacheItem)
        {
            RemoveItem(itemKey);
            ApplicationState.Add(itemKey, cacheItem);
        }

        /// <summary>
        /// Removes the cached item.
        /// </summary>
        /// <param name="itemKey">The cache item key.</param>
        internal void RemoveItem(
            ApplicationCacheDefaultItemType itemKey)
        {
            RemoveItem(itemKey.ToString());
        }

        /// <summary>
        /// Removes the cached item.
        /// </summary>
        /// <param name="itemKey">The cache item key.</param>
        public void RemoveItem(
            string itemKey)
        {
            if (IsCached(itemKey))
            {
                ApplicationState.Remove(itemKey);
            }
        }

        #endregion Methods
    }
}
