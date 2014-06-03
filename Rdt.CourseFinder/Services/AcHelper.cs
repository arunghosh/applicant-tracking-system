using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Rdt.CourseFinder.Services
{
    public static class AcHelper
    {
        static DbCache _dbCache = DbCache.Instance;

        public static List<string> Agents(string term)
        {
            var names = _dbCache.Agents;
            return Filter(names, term);
        }


        public static List<string> Categories(string term)
        {
            var names = _dbCache.Categories;
            return Filter(names, term);
        }

        public static List<string> ProjectNames(string term)
        {
            var names = _dbCache.ProjectNames;
            return Filter(names, term);
        }

        public static List<string> CountryNames(string term)
        {
            var names = _dbCache.CountryNames;
            return Filter(names, term);
        }

        private static List<string> Filter(List<string> data, string term)
        {
            term = term.ToLower();
            var filtered = data.Where(n => n.ToLower().Contains(term))
                                .Take(5)
                                .ToList();
            filtered.Sort();
            return filtered;
        }

        private static List<string> Filter(List<int> data, string term)
        {
            term = term.ToLower();
            var filtered = data.Select(d => d.ToString())
                .Where(n => n.Contains(term))
                .Take(5)
                .ToList();
            filtered.Sort();
            return filtered;
        }

    }
}