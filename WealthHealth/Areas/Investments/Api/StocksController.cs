﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace WealthHealth.Areas.Investments.Api
{
    public class StocksController : ApiController
    {
        // GET api/stocks
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/stocks/5
        public string Get(int id)
        {
            return "value";
        }

        // POST api/stocks
        public void Post([FromBody]string value)
        {
        }

        // PUT api/stocks/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/stocks/5
        public void Delete(int id)
        {
        }
    }
}
