using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace Calipso.Controllers
{
    [Route("api/reviews/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        public readonly IConfiguration _configuration;

        public UsersController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpGet("{id}", Name = "Get")]
        public JsonResult Get(int id)
        {
            string query = @"SELECT * FROM review WHERE userId= '" + id + "'" ;

            DataTable table = new DataTable();
            string sqlDatabase = _configuration.GetConnectionString("Calipso");
            MySqlDataReader myReader;

            using (MySqlConnection mycon = new MySqlConnection(sqlDatabase))
            {
                mycon.Open();
                using (MySqlCommand myCommand = new MySqlCommand(query, mycon))
                {
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);

                    myReader.Close();
                    mycon.Close();
                }
            }

            return new JsonResult(table);
        }
    }
}
