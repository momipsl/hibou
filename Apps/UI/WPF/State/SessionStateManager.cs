﻿using System;
using System.Threading;
using Microsoft.Practices.EnterpriseLibrary.Caching;

namespace Keane.CH.Framework.Apps.UI.WPF.State
{
    /// <summary>
    /// Encapsulates user session state management funtionality.
    /// </summary>
    public class SessionStateManager
    {
        #region Ctor

        internal SessionStateManager(WPFWindowBase window)
        {
            Window = window;
        }

        #endregion Ctor

        #region Constants

        private const string SESSION_STATE_CACHE_STORE = "SessionStateCacheStore{0}";

        #endregion Constants

        #region Private methods

        /// <summary>
        /// Retrieves a cache manager from the managed collection.
        /// </summary>
        /// <param name="cacheStore">A cache store key within which items are cached.</param>
        private static ICacheManager GetCache(
            string cacheStore)
        {
            ICacheManager cache = CacheFactory.GetCacheManager(cacheStore);
            if (cache == null)
                throw new ApplicationException("ServiceCache manager must be assigned in EntLib 4.0 configuration.");
            return cache;
        }

        #endregion Private methods

        #region Properties

        /// <summary>
        /// Gets the window currently being processed.
        /// </summary>
        private WPFWindowBase Window
        { get; set; }

        /// <summary>
        /// Gets the session state manager.
        /// </summary>
        private ICacheManager SessionState
        { get { return GetCache(string.Format(SESSION_STATE_CACHE_STORE, Thread.CurrentPrincipal.Identity.Name)); } }

        #region Standard cache items

        /// <summary>
        /// Gets or sets the number of failed authentication attempts within the current session.
        /// </summary>
        internal int AuthenticationFailureCount
        {
            get
            {
                object cacheItem = GetItem(SessionCacheDefaultItemType.AuthenticationFailureCount);
                if (cacheItem != null)
                    return Convert.ToInt32(cacheItem);
                else
                    return default(int);
            }
            set
            {
                SetItem(SessionCacheDefaultItemType.AuthenticationFailureCount, value);
            }
        }

        /// <summary>
        /// Gets or sets the session culture.
        /// </summary>
        internal string CachedUserCulture
        {
            get
            {
                object cacheItem = GetItem(SessionCacheDefaultItemType.UserCulture);
                if (cacheItem != null)
                    return Convert.ToString(cacheItem);
                else
                    return default(string);
            }
            set
            {
                SetItem(SessionCacheDefaultItemType.UserCulture, value);
            }
        }

        /// <summary>
        /// Gets or sets the session culture id.
        /// </summary>
        internal int CachedUserCultureId
        {
            get
            {
                object cacheItem = GetItem(SessionCacheDefaultItemType.UserCultureId);
                if (cacheItem != null)
                    return Convert.ToInt32(cacheItem);
                else
                    return default(int);
            }
            set
            {
                SetItem(SessionCacheDefaultItemType.UserCultureId, value);
            }
        }

        /// <summary>
        /// Gets or sets the session user id.
        /// </summary>
        internal int CachedUserId
        {
            get
            {
                object cacheItem = GetItem(SessionCacheDefaultItemType.UserId);
                if (cacheItem != null)
                    return Convert.ToInt32(cacheItem);
                else
                    return default(int);
            }
            set
            {
                SetItem(SessionCacheDefaultItemType.UserId, value);
            }
        }

        /// <summary>
        /// Gets or sets the session array of user role type id.
        /// </summary>
        internal int[] CachedUserRoleIds
        {
            get
            {
                object cacheItem = GetItem(SessionCacheDefaultItemType.UserRoleTypeIds);
                if (cacheItem != null)
                    return (int[])cacheItem;
                else
                    return default(int[]);
            }
            set
            {
                SetItem(SessionCacheDefaultItemType.UserRoleTypeIds, value);
            }
        }

        /// <summary>
        /// Gets or sets the session user name.
        /// </summary>
        internal string CachedUserName
        {
            get
            {
                object cacheItem = GetItem(SessionCacheDefaultItemType.UserName);
                if (cacheItem != null)
                    return Convert.ToString(cacheItem);
                else
                    return default(string);
            }
            set
            {
                SetItem(SessionCacheDefaultItemType.UserName, value);
            }
        }

        /// <summary>
        /// Gets or sets the session user display name.
        /// </summary>
        internal string CachedUserDisplayName
        {
            get
            {
                object cacheItem = GetItem(SessionCacheDefaultItemType.UserDisplayName);
                if (cacheItem != null)
                    return Convert.ToString(cacheItem);
                else
                    return default(string);
            }
            set
            {
                SetItem(SessionCacheDefaultItemType.UserDisplayName, value);
            }
        }

        /// <summary>
        /// Gets or sets the session user roles.
        /// </summary>
        internal string[] CachedUserRoleNames
        {
            get
            {
                object cacheItem = GetItem(SessionCacheDefaultItemType.UserRoles);
                if (cacheItem != null)
                    return (string[])cacheItem;
                else
                    return default(string[]);
            }
            set
            {
                SetItem(SessionCacheDefaultItemType.UserRoles, value);
            }
        }

        /// <summary>
        /// Gets or sets the user meta data.
        /// </summary>
        internal object CachedUserMetaData
        {
            get
            {
                object cacheItem = GetItem(SessionCacheDefaultItemType.UserMetaData);
                if (cacheItem != null)
                    return cacheItem;
                else
                    return default(object);
            }
            set
            {
                SetItem(SessionCacheDefaultItemType.UserMetaData, value);
            }
        }

        /// <summary>
        /// Gets or sets the session theme.
        /// </summary>
        internal string CachedUserTheme
        {
            get
            {
                object cacheItem = GetItem(SessionCacheDefaultItemType.UserTheme);
                if (cacheItem != null)
                    return Convert.ToString(cacheItem);
                else
                    return default(string);
            }
            set
            {
                SetItem(SessionCacheDefaultItemType.UserTheme, value);
            }
        }

        /// <summary>
        /// Gets or sets the session ui culture.
        /// </summary>
        internal string CachedUserUICulture
        {
            get
            {
                object cacheItem = GetItem(SessionCacheDefaultItemType.UserUICulture);
                if (cacheItem != null)
                    return Convert.ToString(cacheItem);
                else
                    return default(string);
            }
            set
            {
                SetItem(SessionCacheDefaultItemType.UserUICulture, value);
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
            SessionState.Flush();
        }

        /// <summary>
        /// Determines whether the cache item is already cached.
        /// </summary>
        /// <param name="itemKey">The cache item key.</param>
        /// <returns>True if the item is already cached.</returns>
        internal bool IsCached(
            SessionCacheDefaultItemType itemKey)
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
            return SessionState.Contains(itemKey);
        }

        /// <summary>
        /// Generic method to retrieve an item from the session cache.
        /// </summary>
        /// <typeparam name="T">The type to be cast to.</typeparam>
        /// <param name="itemKey">The cache item key.</param>
        /// <returns>The cast cached item.</returns>
        internal T GetItem<T>(
            SessionCacheDefaultItemType itemKey)
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
            if (String.IsNullOrEmpty(itemKey))
                return default(T);
            object cacheItem = SessionState.GetData(itemKey);
            if (cacheItem == null)
                return default(T);
            else
                return (T)cacheItem;
        }

        /// <summary>
        /// Retrieves an item from the session cache.
        /// </summary>
        /// <param name="itemKey">The cache item key.</param>
        /// <returns>The cached item.</returns>
        internal object GetItem(
            SessionCacheDefaultItemType itemKey)
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
            if (String.IsNullOrEmpty(itemKey))
                return null;
            return SessionState.GetData(itemKey);
        }

        /// <summary>
        /// Returns the cached item.
        /// </summary>
        /// <param name="itemKey">The cache item key.</param>
        /// <param name="cacheItem">The item being cached.</param>
        internal void SetItem(
            SessionCacheDefaultItemType itemKey,
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
            SessionState.Add(itemKey, cacheItem);
        }

        /// <summary>
        /// Removes the cached item.
        /// </summary>
        /// <param name="itemKey">The cache item key.</param>
        internal void RemoveItem(
            SessionCacheDefaultItemType itemKey)
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
                SessionState.Remove(itemKey);
            }
        }

        #endregion Methods
    }
}
