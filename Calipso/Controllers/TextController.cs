using Calipso.Models;
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
    public class TextController : ControllerBase
    {
        public readonly IConfiguration _configuration;

        public TextController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpPost]
        public JsonResult Post(Review review)
        {
            string query = @"
                insert into Review 
                (userId,place_id,experience,rating) 
                values (@userId,@placeId,@experience,@rating);
            ";

            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("Calipso");
            MySqlDataReader myReader;
            using (MySqlConnection mycon = new MySqlConnection(sqlDataSource))
            {
                mycon.Open();
                using (MySqlCommand myCommand = new MySqlCommand(query, mycon))
                {
                    myCommand.Parameters.AddWithValue("@userId", review.userId);
                    myCommand.Parameters.AddWithValue("@placeId", review.place_id);
                    myCommand.Parameters.AddWithValue("@experience", review.experience);
                    myCommand.Parameters.AddWithValue("@rating", review.rating);


                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);

                    myReader.Close();
                    mycon.Close();
                }
            }

            return new JsonResult("Add review text access!!!!");
        }

    }
}
