/*
 * Licensed under the Apache License, Version 2.0 (http://www.apache.org/licenses/LICENSE-2.0)
 * See https://github.com/KastraCMS/kastra-core for more information concerning
 * the license and the contributors participating to this project.
 */

using System;
using System.Collections.Generic;
using System.Linq;
using Kastra.Core.Constants;
using Microsoft.Extensions.Caching.Memory;

namespace Kastra.Core.Services
{
    public class CacheEngine
    {
        private const int CACHE_MAX_LENGTH = 1000;

        private readonly IMemoryCache _memoryCache = null;

        private static MemoryCacheEntryOptions _options = null;
        private IList<string> _entries = null;

        private bool? _isActivated = null;
        public bool IsActivated
        {
            get
            {
                return _isActivated ?? false;
            }
        }

        public MemoryCacheEntryOptions CacheOptions
        {
            get
            {
                if (_options == null)
                {
                    MemoryCacheEntryOptions options = new MemoryCacheEntryOptions()
                    {
                        SlidingExpiration = TimeSpan.FromHours(2)
                    };
                }

                return _options;
            }
        }

        public CacheEngine(IMemoryCache memoryCache)
        {
            _memoryCache = memoryCache;
            _entries = new List<string>(CACHE_MAX_LENGTH);
        }

        #region Site configuration

        /// <summary>
        /// Clears the site configuration.
        /// </summary>
        public void ClearSiteConfig()
        {
            _memoryCache.Remove(SiteConfiguration.SiteConfigCacheKey);
        }

        #endregion

        #region Utils

        /// <summary>
        /// Gets the cached object with a key.
        /// </summary>
        /// <returns><c>true</c>, if cache object was gotten, <c>false</c> otherwise.</returns>
        /// <param name="key">Key.</param>
        /// <param name="cacheObject">Cache object.</param>
        /// <typeparam name="T">The 1st type parameter.</typeparam>
        public Boolean GetCacheObject<T>(string key, out T cacheObject)
        {
            if(!IsActivated)
            {
                cacheObject = default(T);

                return false;
            }

            return _memoryCache.TryGetValue(key.ToLower(), out cacheObject);
        }

        /// <summary>
        /// Sets a cached object with a key.
        /// </summary>
        /// <returns>The cache object.</returns>
        /// <param name="key">Key.</param>
        /// <param name="cacheObject">Cache object.</param>
        /// <typeparam name="T">The 1st type parameter.</typeparam>
        public T SetCacheObject<T>(string key, T cacheObject)
        {
            if(!IsActivated)
            {
                return cacheObject;
            }

            key = key.ToLower();
            _entries.Add(key);

            return _memoryCache.Set(key, cacheObject, CacheOptions);
        }

        /// <summary>
        /// Clears the cache by key.
        /// </summary>
        /// <param name="key">Key.</param>
        public void ClearCache(string key)
        {
            key = key.ToLower();

            _entries.Remove(key);
            _memoryCache.Remove(key);
        }

        /// <summary>
        /// Clears the cache where the key was found.
        /// </summary>
        /// <param name="key">Key.</param>
        public void ClearCacheContains(string key)
        {
            var entriesToDelete = _entries.Where(e => e.Contains(key.ToLower()));

            foreach(String entry in entriesToDelete)
            {
                _memoryCache.Remove(entry);
            }

            _entries = _entries.Except(entriesToDelete).ToList();
        }

        /// <summary>
        /// Clears all cache.
        /// </summary>
        public void ClearAllCache()
        {
            foreach (string entry in _entries)
            {
                _memoryCache.Remove(entry);
            }

            _entries = new List<string>(CACHE_MAX_LENGTH);
        }

        /// <summary>
        /// Gets all entries in the cache.
        /// </summary>
        /// <returns>The all entries.</returns>
        public IList<string> GetAllEntries()
        {
            return _entries;
        }

        /// <summary>
        /// Enables the cache.
        /// </summary>
        public void EnableCache()
        {
            _isActivated = true;
        }

        /// <summary>
        /// Disables the cache.
        /// </summary>
        public void DisableCache()
        {
            _isActivated = false;
        } 

        #endregion
    }
}