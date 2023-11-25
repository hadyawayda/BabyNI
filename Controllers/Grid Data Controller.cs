using BabyAPI.Connection;
using Microsoft.AspNetCore.Mvc;
using Vertica.Data.VerticaClient;
//using Dapper.EntityFramework;

namespace BabyAPI
{
    [Route("api/grid")]
    [ApiController]
    public class GridDataController : ControllerBase
    {
        private VerticaCommand?                     query;
        private VerticaDataReader?                  reader;
        private Dictionary<string, object>?         dictionary;
        private List<Dictionary<string, object>>?   data;
        private readonly string[]                   keys;
        private readonly IDbConnection              _connection;

        public GridDataController(IDbConnection connection)
        {
            _connection = connection;

            keys = new string[]
                {
                    "DATETIME_KEY",
                    "TIME" ,
                    "NETWORK_SID" ,
                    "NEALIAS" ,
                    "NETYPE" ,
                    "RSL_INPUT_POWER" ,
                    "MAX_RX_LEVEL" ,
                    "RSL_DEVIATION" ,
                };
        }

        [HttpGet("daily")]
        public IActionResult GetDailyData(DateTime? startDate, DateTime? endDate)
        {
            query = _connection.QueryCommand();

            query!.CommandText = "SELECT * FROM TRANS_MW_AGG_SLOT_DAILY;";

            reader = query.ExecuteReader();

            data = new List<Dictionary<string, object>>();

            while (reader.Read())
            {
                dictionary = new Dictionary<string, object>();

                for (int i = 0; i < reader.FieldCount; i++)
                {
                    dictionary.Add(keys[i], reader.GetValue(i));
                }

                data.Add(dictionary);
            }

            reader.Close();

            return Ok(data);
        }

        [HttpGet("hourly")]
        public IActionResult GetHourlyData(DateTime? startDate, DateTime? endDate)
        {
            query = _connection.QueryCommand();

            query!.CommandText = "SELECT * FROM TRANS_MW_AGG_SLOT_HOURLY;";

            reader = query.ExecuteReader();

            data = new List<Dictionary<string, object>>();

            while (reader.Read())
            {
                dictionary = new Dictionary<string, object>();

                for (int i = 0; i < reader.FieldCount; i++)
                {
                    dictionary.Add(keys[i], reader.GetValue(i));
                }

                data.Add(dictionary);
            }

            reader.Close();
            
            return Ok(data);
        }
    }
}
