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
        public ActionResult Index(string input)
        {
            using (ObjectContext studentContext = new ObjectContext("name=StudentEntities"))
            {
                // FIXED: Use parameterized query to prevent SQL Injection
                var query = studentContext.CreateQuery<Student>(
                    "select * from students where Name = @name",
                    new ObjectParameter("name", input)
                );

                // FIXED: Use parameterized query to prevent SQL Injection
                studentContext.ExecuteStoreCommand("select * from students where Name = @name", new ObjectParameter("name", input));

                // FIXED: Use parameterized query to prevent SQL Injection
                studentContext.ExecuteStoreQuery<Student>(
                    "select * from students where Name = @name",
                    new ObjectParameter("name", input)
                );

                // FIXED: Use parameterized query to prevent SQL Injection
                studentContext.ExecuteStoreQuery<Student>(
                    "select * from students where Name = @name",
                    new ObjectParameter("name", input)
                );
            }

            FullTextSqlQuery myQuery = new FullTextSqlQuery(SPContext.Current.Site)
            {
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
