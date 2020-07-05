using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ShowCastWebApi2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ShowsController : ControllerBase
    {
        // GET: api/<ValuesController>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            string[] endpoints = new string[] { "" };
            endpoints[0] = "endpoint example: localhost:xxxx/api/shows/under the dome";

            return endpoints;
        }

        // GET api/<ValuesController>/5
        [HttpGet("{showname}")]
        public string Get(string showname)
        {
            string StoreFileName = "ShowCast_data.json";
            string path = Environment.GetFolderPath(Environment.SpecialFolder.Desktop)
            + "/" + StoreFileName;
            string allText = System.IO.File.ReadAllText(path);

            List<Pagenation> lsPagenations = (List<Pagenation>)DeserializeToList<Pagenation>(allText);


            var checkShownName = showname;
            string FoundShow = "Show not found";
            for (int i = 0; i < lsPagenations.Count; i++)
            {

                List<ResponseShow> m = lsPagenations[i].lsShow;

                var matchShow = m.Where(ResponseShow => ResponseShow.Name.ToLower() == checkShownName.ToLower()).ToList().FirstOrDefault();

                if (matchShow != null)
                {
                    FoundShow = JsonConvert.SerializeObject(matchShow, Formatting.Indented);
                    break;
                }


            }
            return FoundShow;
        }

        // POST api/<ValuesController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<ValuesController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<ValuesController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
         static List<string> InvalidJsonElements;
         static IList<T> DeserializeToList<T>(string jsonString)
        {
            InvalidJsonElements = null;
            var array = JArray.Parse(jsonString);
            IList<T> objectsList = new List<T>();
            foreach (var item in array)
            {
                try
                {
                    // CorrectElements
                    objectsList.Add(item.ToObject<T>());
                }
                catch (Exception ex)
                {
                    InvalidJsonElements = InvalidJsonElements ?? new List<string>();
                    InvalidJsonElements.Add(item.ToString());
                }
            }
            return objectsList;
        }
    }
}
