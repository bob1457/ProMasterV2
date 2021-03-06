﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Mvc;
using ProMaster.Web.Filters;

namespace ProMaster.Web.Controllers
{
    public class LoginController : ApiController
    {
        // GET api/login
        [BasicAuthorize(Roles = "SuperAdmin")]
        //[System.Web.Http.Authorize]
        public string Get()
        {
            //return new string[] { "value1", "value2" };
            return "Authorized & Authenticated to use API.";

        }

        // GET api/login/5
        [System.Web.Http.Authorize]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/login
        public void Post([FromBody]string value)
        {
        }

        // PUT api/login/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/login/5
        public void Delete(int id)
        {
        }
    }
}
