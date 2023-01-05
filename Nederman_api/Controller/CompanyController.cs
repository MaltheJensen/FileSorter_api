using MongoDB.Driver;
using Nederman_api.Database;
using Nederman_api.Dto;
using Nederman_api.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Nederman_api.Controller
{
    public class CompanyController
    {
        private string databaseName = "CompanyInfoTable";
        private CompanyDB companyDB;
        private JobInstructionsDB jobInstructionsDB;
        private RapportDB rapportDB;
        public CompanyController()
        {
            companyDB = new CompanyDB(databaseName);
            rapportDB = new RapportDB(databaseName);
            jobInstructionsDB = new JobInstructionsDB(databaseName);

        }

        public Company GetCompanyByName(string name)
        {
            Company company = new Company();

            company = companyDB.LoadRecordByName<Company>("Company", name);

            company.JobInstrutions = new List<byte[]>();
            company.Rapports = new List<byte[]>();

            foreach (string s in company.JobInstrutionsFileName)
            {
                var jobs = jobInstructionsDB.DownloadFile(s);
                company.JobInstrutions.Add(jobs);
            }
            foreach (string s in company.RapportsFileName)
            {
                var rapport = rapportDB.DownloadFile(s);
                company.Rapports.Add(rapport);
            }
            return company;
        }

        public List<Company> GetAllCompanies() 
        {
            List<Company> allCompanies = new List<Company>();

            allCompanies = companyDB.GetAllRecords<Company>("Company");

            foreach (Company c in allCompanies)
            {
                c.JobInstrutions = new List<byte[]>();
                c.Rapports = new List<byte[]>();
                foreach (string s in c.JobInstrutionsFileName)
                {
                    var jobs = jobInstructionsDB.DownloadFile(s);
                    c.JobInstrutions.Add(jobs);
                }
                foreach (string s in c.RapportsFileName)
                {
                    var rapport = rapportDB.DownloadFile(s);
                    c.Rapports.Add(rapport);
                }
            }
            return allCompanies;
        }

        public void SaveNewReport(NewReportDto newReport) 
        {
            Company company = companyDB.LoadRecordByName<Company>("Company", newReport.CompanyName);

            company.RapportsFileName.Add(newReport.ReportName);

            rapportDB.UploadFile(newReport.ReportName, newReport.Report);

            companyDB.UpsertRecord<Company>("Company", company.Id, company);
        }
    }
}
