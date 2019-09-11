using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Protocols;
using Newtonsoft.Json.Linq;
using System.IO;
using System.Web;
using System.Web.Http;
using Microsoft.Extensions.Options;

namespace CoreApi.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
            private AppSettings AppSettings { get; set; }
            public ValuesController(IOptions<AppSettings> settings)
            {
                AppSettings = settings.Value;
            }    

        // GET api/values
        [HttpGet]
        public ActionResult<IEnumerable<string>> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/values/5
        [HttpGet("{id}/{arrivalDate}")]
        public ActionResult<object> Get(int id, string arrivalDate)
        {
            var filePath = AppSettings.JsonFile;
            var JSONFile = JArray.Parse(System.IO.File.ReadAllText(filePath));

            dynamic resultJson = new JObject();
            var item = JSONFile.OfType<dynamic>().FirstOrDefault(json => json.hotel.hotelID == id);
            
            if (item != null)
            {
                resultJson.hotel = item.hotel;
                DateTime arrivalDateTime;
                if (DateTime.TryParse(arrivalDate, out arrivalDateTime))
                {
                    var rates = new JArray();
                    foreach (var hotelRate in item.hotelRates)
                    {
                        DateTime dateTime;
                        if (DateTime.TryParse(Convert.ToString(hotelRate.targetDay), out dateTime) && dateTime.Date.Equals(arrivalDateTime.Date))
                        {
                            rates.Add(hotelRate);
                        }
                    }
                    resultJson.hotelRates = rates;
                }
            }

            return resultJson;
        }

    }
}
