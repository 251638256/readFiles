using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace WebApplication2.Controllers {

    public class Test {
        public string name { get; set; }

        public string age { get; set; }
    }

    public class ValuesController : ApiController {
        // GET api/values
        public IEnumerable<string> Get() {
            return new string[] { "value1", "value2" };
        }

        // GET api/values/5
        public string Get(int id) {
            return "value";
        }

        // POST api/values
        public void Post([FromBody]string value) {
        }

        // PUT api/values/5
        public void Put(int id, [FromBody]string value) {
        }

        // DELETE api/values/5
        public void Delete(int id) {
        }

   
        public List<Test> getMyData() {
            return new List<Test>() { new Test() { age = "1", name = "1" }, new Test() { age = "2", name = "2" } };
        }
    }
}
