﻿using System;
using System.Collections.Generic;
using System.Linq;
using Repository.Analysis;
using Repository.Model;

namespace Repository
{
    internal static class IEnumerables
    {
        public static IEnumerable<T> Concat<T>(params IEnumerable<T>[] lists)
        {
            return lists.SelectMany(x => x);
        }

        public static IEnumerable<SymbolReference> GetSymbolReferences(this IEnumerable<SymbolReferenceProvider> providers)
        {
            return providers.SelectMany(x => x.GetSymbolReferences());
        }

        internal static bool AllResolved(this IEnumerable<Resolveable> symbols)
        {
            return symbols.All(x => x.GetIsResolved());
        }

        internal static void Strip<T>(this IEnumerable<T> symbols) where T : Symbol
        {
            foreach (var symbol in symbols)
                symbol.Strip();
        }
    }
}
