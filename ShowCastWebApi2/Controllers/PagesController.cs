using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ShowCastWebApi2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PagesController : ControllerBase
    {
        // GET: api/<PagesController>
        [HttpGet]
        public IEnumerable<string> Get()
        {

            string[] endpoints = new string[] { ""};
            endpoints[0] = "endpoint example: localhost:xxxx/api/pages/1";

            return endpoints;
        }

        // GET api/<PagesController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {

            string StoreFileName = "ShowCast_data.json";
            string path = Environment.GetFolderPath(Environment.SpecialFolder.Desktop)
            + "/" + StoreFileName;
            string allText = System.IO.File.ReadAllText(path);
            List<Pagenation> lsPage = (List<Pagenation>)DeserializeToList<Pagenation>(allText);

            var maxID = lsPage.Count;
            string Response = "Page not found";

            
            if(id>=0 && id <maxID)
            {
                try
                {
                    var matchPage = lsPage.Where(Pagenation => Pagenation.PageID == id).ToList().FirstOrDefault();
                    Response = JsonConvert.SerializeObject(matchPage.lsShow, Formatting.Indented);
                }
                catch (Exception)
                {
                    Response = "Server error";
                    throw;
                }
                

            }

            return Response;
        }

        // POST api/<PagesController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<PagesController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<PagesController>/5
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
