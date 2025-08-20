using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Objects;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.SharePoint.Search.Query;
using Microsoft.SharePoint.Client;
using Microsoft.SharePoint;

namespace NETMVCBlot.Controllers
{
    public class SQLInjController : Controller
    {
        [ValidateAntiForgeryToken]
        public ActionResult Index(string input)
        {
            using (ObjectContext studentContext = new ObjectContext("name=StudentEntities"))
            {
                // Use parameterized queries to prevent SQL Injection
                var query = studentContext.CreateQuery<Student>(
                    "SELECT VALUE s FROM students AS s WHERE s.Name = @name",
                    new ObjectParameter("name", input));

                studentContext.ExecuteStoreCommand(
                    "SELECT * FROM students WHERE Name = @name",
                    new System.Data.SqlClient.SqlParameter("@name", input));

                studentContext.ExecuteStoreQuery<Student>(
                    "SELECT * FROM students WHERE Name = @name",
                    new System.Data.SqlClient.SqlParameter("@name", input));

            // Use parameterized query for SharePoint FullTextSqlQuery if possible
            string safeInput = input.Replace("'", "''"); // Basic escaping, consider more robust validation
            FullTextSqlQuery myQuery = new FullTextSqlQuery(SPContext.Current.Site)
            {
                QueryText = $"SELECT Path FROM SCOPE() WHERE  \"SCOPE\" = '{safeInput}'",
                ResultTypes = ResultType.RelevantResults

            };
                // CTSECISSUE: SQLInjection
                QueryText = "SELECT Path FROM SCOPE() WHERE  \"SCOPE\" = '" + input + "'",
                ResultTypes = ResultType.RelevantResults

            };

            return View();
        }
    }

    class SchoolContext : DbContext
    {
        public DbSet<Student> Students { get; set; }
    }

    class Student
    {
    }
}