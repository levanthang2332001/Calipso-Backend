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
    public class ScoreController : ControllerBase
    {
        public readonly IConfiguration _configuration;

        public ScoreController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpPost]
        public JsonResult Post(Score score)
        {
            string query = @"
                insert into Review 
                (userId,place_id) 
                values (@userId,@placeId);
            ";

            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("Calipso");
            MySqlDataReader myReader;
            using (MySqlConnection mycon = new MySqlConnection(sqlDataSource))
            {
                mycon.Open();
                using (MySqlCommand myCommand = new MySqlCommand(query, mycon))
                {
                    myCommand.Parameters.AddWithValue("@userId", score.userId);
                    myCommand.Parameters.AddWithValue("@placeId", score.place_id);

/*                    if (score.rating == 0 && score.wifi == 0 && score.cost == 0 && score.fun == 0 && score.air_quanity == 0 && score.people_desity == 0 && score.trafic == 0 && score.huminity == 0)
                    {
                        return new JsonResult("Fail add scores");
                    }*/


                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);

                    myReader.Close();
                    mycon.Close();
                }
            }

            return new JsonResult("Add scores access!!!!");
        }
    }
}
