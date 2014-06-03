using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Linq;
using System.Web;
using Rdt.CourseFinder.Entities;

namespace Rdt.CourseFinder.Services
{
    public class ImportSrv : ServiceBase
    {
        // Save File
        // Open File
        // Validate
        // ReadData

        HttpPostedFileBase _file;
        string _serverXlsPath;
        Import _model;
        DataSet _dataSet = new DataSet();
        int _sucessCnt = 0;
        int _skipCnt = 0;
        User _currUser;

        public string Summary
        {
            get
            {
                return string.Format("Data imported successfully. \n{0} imported. \n{1} skipped.", _model.SuccessCount, _model.SkippedCount);
            }
        }
        public ImportSrv(HttpPostedFileBase file, Import model)
        {
            _file = file;
            _model = model;
            _currUser = CurrentUser;
        }

        public void SaveToDB()
        {
            SaveExcelFile();
            ConvertXlsToDataSet();
            ValidateXls();
            ImportCandidates();
            UpdateImportModel();
            AddProjectLog();
            AddUserLog();
            _db.SaveChanges();
        }

        private void SaveExcelFile()
        {
            _serverXlsPath = Routes.ExcelPath();
            _model.Path = _serverXlsPath;
            _file.SaveAs(_serverXlsPath);
        }

        private void ConvertXlsToDataSet()
        {
            string strConn = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + _serverXlsPath + ";Extended Properties='Excel 12.0 xml;HDR=YES;'";
            OleDbConnection conn = new OleDbConnection(strConn);
            conn.Open();
            OleDbDataAdapter myCommand = null;
            string strExcel = "select * from [sheet1$]";
            myCommand = new OleDbDataAdapter(strExcel, strConn);
            myCommand.Fill(_dataSet, "table1");
        }

        private void ValidateXls()
        {
            //TODO
        }

        private void ImportCandidates()
        {
            var skip = true;
            var passports = _db.Candidates
                                .Where(c => c.ProjectId == _model.ProjectId)
                                .Select(p => p.Passport)
                                .ToList();
            var blankCnt = 0;
            foreach (var item in _dataSet.Tables[0].Rows)
            {
                if (skip)
                {
                    skip = false;
                    continue;
                }
                var arr = ((DataRow)item).ItemArray;
                var candidat = CreateCandidate(arr);
                if (candidat == null)
                {
                    blankCnt++;
                    if (blankCnt > 2) break;
                    continue;
                }
                if (passports.Contains(candidat.Passport))
                {
                    _skipCnt++;
                }
                else
                {
                    _sucessCnt++;
                    _db.Candidates.Add(candidat);
                }
            }
        }

        private void UpdateImportModel()
        {
            _model.TotalCount = _sucessCnt + _skipCnt;
            _model.SuccessCount = _sucessCnt;
            _model.UserId = _currUser.UserId;
            _model.UserName = _currUser.Name;
            _db.Imports.Add(_model);
        }

        private void AddProjectLog()
        {
            var prjLog = new ProjectLog()
            {
                UserId = _currUser.UserId,
                UserName = _currUser.Name,
                Log = Summary,
                ProjectId = _model.ProjectId
            };
            _db.ProjectLogs.Add(prjLog);
        }

        private void AddUserLog()
        {
            var log = new UserLog()
            {
                UserId = _currUser.UserId,
                Comment = Summary,
                ByUserName = _currUser.Name,
                ByUserId = _currUser.UserId
            };
            _db.UserLogs.Add(log);
        }

        private Candidate CreateCandidate(object[] arr)
        {
            if(string.IsNullOrEmpty(arr[1].ToString()))
            {
                return null;
            }
            var statusStr = arr[12].ToString().Trim();
            var status = DbCache.Instance.CanditStatus.SingleOrDefault(s => s.Abbrevation == statusStr);
            if (status == null)
            {
                throw new SimpleException(string.Format("Invalid Status '{0}' found. Candidate Name: {1}", statusStr, arr[1].ToString()));
            }
            var candidate = new Candidate
            {
                Name = arr[1].ToString().Trim(),
                Category = arr[2].ToString().Trim(),
                Passport = arr[3].ToString().Trim(),
                Bsro = arr[4].ToString().Trim(),
                Age = arr[5].ToString().Trim(),
                Experience = arr[6].ToString().Trim(),
                Grade = arr[7].ToString().Trim(),
                ContactNo = arr[8].ToString().Trim(),
                SeleDate = arr[9].ToString().Trim(),
                City = arr[10].ToString().Trim(),
                Agent = arr[11].ToString().Trim(),
                CandidateStatusId = status.CandidateStatusId,
                Remarks = arr[13].ToString().Trim(),
                CreationType = CandidateCreateType.ExcelImport,
                ProjectId = _model.ProjectId
            };
            if (candidate.Experience != null && candidate.Experience.Length > 64)
            {
                candidate.Experience = candidate.Experience.Substring(0, 62);
            }
            candidate.CandidateLogs.Add(new CandidateLog
            {
                ByUserId = _currUser.UserId,
                ByUserName = _currUser.Name,
                Log = "Added via Excel Import"
            });
            return candidate;
        }


    }
}