using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Dapper;
using webapi1.Models;

namespace webapi1.Controllers
{
    public class BlogsController : ApiController
    {
        // GET: api/Blogs
        public IEnumerable<Blog> Get()
        {
            using (var conn = new SqlConnection(ConfigurationManager.ConnectionStrings["DevConnectionString"].ConnectionString))
            {
                var sqlString = "select * from dbo.Blog;";
                IEnumerable<Blog> records = conn.Query<Blog>(sqlString).ToList();

                return records;
            }
        }

        // GET: api/Blogs/5
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/Blogs
        public void Post([FromBody]BlogVm value)
        {
            using (var conn = new SqlConnection(ConfigurationManager.ConnectionStrings["DevConnectionString"].ConnectionString))
            {
                var sqlString = string.Format("insert into dbo.Blog (Title , Body , CreatedDate , UpdatedDate) values ('{0}' , '{1}' , GETDATE() , GETDATE() );", value.Title, value.Body);
                conn.Execute(sqlString);
            }
        }

        // PUT: api/Blogs/5
        public void Put(int id, [FromBody]BlogVm value, int Id)
        {
            try
            {
                using (var conn = new SqlConnection(ConfigurationManager.ConnectionStrings["DevConnectionString"].ConnectionString))
                {
                    var sqlString = string.Format("UPDATE dbo.Blog SET	Title = '{0}' , Body = '{1}' , UpdatedDate = GETDATE() WHERE Id = {2};", value.Title, value.Body, Id);
                    conn.Execute(sqlString);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        // DELETE: api/Blogs/5
        public void Delete(int id)
        {
            using (var conn = new SqlConnection(ConfigurationManager.ConnectionStrings["DevConnectionString"].ConnectionString))
            {
                var sqlString = string.Format("DELETE dbo.Blog WHERE Id = {0};", id);
                conn.Execute(sqlString);
            }
        }
    }
}