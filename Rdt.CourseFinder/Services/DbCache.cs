using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using Rdt.CourseFinder.Entities;

namespace Rdt.CourseFinder.Services
{
    public class DbCache : ServiceBase
    {
        public static readonly DbCache Instance = new DbCache();

        private DbCache()
        {

        }


        //static List<Candidate> _candidates;
        //public static List<Candidate> Candidates
        //{
        //    get
        //    {
        //        if (_candidates == null)
        //        {
        //            var candidates = new List<Candidate>();
        //            var candLst = ReadFrom(Routes.CandidateFile);
        //            foreach (var item in candLst)
        //            {
        //                var arr = item.Split(',');
        //                var candidat = new Candidate
        //                {
        //                    Name = arr[1],
        //                    Category = arr[2],
        //                    Passport = arr[3],
        //                    City = arr[10],
        //                    Remarks = arr[13],
        //                    Status = arr[12],
        //                    Grade = arr[7]

        //                };
        //                candidates.Add(candidat);
        //            }
        //            _candidates = candidates;
        //        }

        //        return _candidates;
        //    }
        //}

        List<string> _categories;
        public List<string> Categories
        {
            get
            {
                if (_categories == null)
                {
                    _categories = ReadFrom(Routes.CategoryFile).ToList();
                    _categories = _categories.Concat(_db.Candidates.Select(c => c.Category)).ToList();
                    _categories = _categories.Distinct().ToList();
                }
                return _categories;
            }
        }

        //List<string> _categoriesFrmDb;
        //public List<string> CategoriesFrmDb
        //{
        //    get
        //    {
        //        if (_categoriesFrmDb == null)
        //        {

        //            _categoriesFrmDb = new List<string> { "[ -- All -- ]" };
        //            _categoriesFrmDb = _categoriesFrmDb.Concat(_db.Candidates.Select(c => c.Category)).ToList();
        //            _categoriesFrmDb = _categoriesFrmDb.Distinct().ToList();
        //        }
        //        return _categoriesFrmDb;
        //    }
        //}


        List<string> _countryNames;
        public List<string> CountryNames
        {
            get
            {
                if (_countryNames == null)
                {
                    _countryNames = _db.Candidates.Select(c => c.City).Distinct().ToList();
                }
                return _countryNames;
            }
        }

        List<string> _agents;
        public List<string> Agents
        {
            get
            {
                if (_agents == null)
                {
                    _agents = _db.Candidates.Select(c => c.Agent).Distinct().ToList();
                }
                return _agents;
            }
        }

        List<string> _projectNames;
        public List<string> ProjectNames
        {
            get
            {
                if (_projectNames == null)
                {
                    _projectNames = _db.Projects.Select(c => c.ProjectName).Distinct().ToList();
                }
                return _projectNames;
            }
        }

        List<CandidateStatus> _canditStatus;
        public List<CandidateStatus> CanditStatus
        {
            get
            {
                if (_canditStatus == null)
                {
                    if (!_db.CandidateStatuses.Any())
                    {
                        var statusLst = ReadFrom(Routes.StatusFile).ToList();

                        foreach (var item in statusLst)
                        {
                            var arr = item.Split(',');
                            _db.CandidateStatuses.Add(
                                new CandidateStatus
                                {
                                    Abbrevation = arr[0],
                                    Name = arr[1]
                                });

                        }
                        _db.SaveChanges();
                    }
                    _canditStatus = _db.CandidateStatuses.ToList();
                }
                return _canditStatus;
            }
        }

        public List<CandidateStatus> PostCanditStatus
        {
            get
            {
                return CanditStatus.Where(s => s.IsPositive).ToList();
            }
        }

        public static IEnumerable<string> ReadFrom(string file)
        {
            string line;
            using (var reader = File.OpenText(file))
            {
                while ((line = reader.ReadLine()) != null)
                {
                    yield return line;
                }
            }
        }
    }
}