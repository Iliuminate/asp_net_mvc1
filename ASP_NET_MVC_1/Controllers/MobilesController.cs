using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Script.Serialization;

namespace ASP_NET_MVC_1.Controllers
{
    public class MobilesController : ApiController
    {
        // GET: api/Mobiles
        public IHttpActionResult Get()
        {
            using (var dataList = new Models.db_pruebasEntities())
            {
                var data = (dataList.Empleado).ToList();

                //List<Models.EmployeeClass> mydata = new List<Models.EmployeeClass>();
                //foreach (var item in data)
                //{
                //    mydata.Add(new Models.EmployeeClass { id=item.id, mail=item.mail, name=item.name, pass=item.pass});
                //}
                //return Ok(mydata);

                return Ok(data);
            }                
        }

        // GET: api/Mobiles/5
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/Mobiles
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/Mobiles/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/Mobiles/5
        public void Delete(int id)
        {
        }
    }
}
