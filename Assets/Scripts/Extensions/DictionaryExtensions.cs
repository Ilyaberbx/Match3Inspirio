using System;
using System.Collections.Generic;
using Better.Commons.Runtime.Utility;
using Better.Locators.Runtime;

namespace EndlessHeresy.Core.Extensions
{
    public static class DictionaryExtensions
    {
        public static ILocator<TKey, TValue> ToLocator<TKey, TValue>(this IDictionary<TKey, TValue> dictionary)
        {
            if (dictionary == null)
            {
                DebugUtility.LogException<NullReferenceException>();
                return null;
            }

            var locator = new Locator<TKey, TValue>();

            foreach (var pair in dictionary)
            {
                locator.Add(pair.Key, pair.Value);
            }

            return locator;
        }
    }
}