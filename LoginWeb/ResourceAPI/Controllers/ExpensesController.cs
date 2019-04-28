using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ResourceAPI.Controllers
{
    [Produces("application/json")]
    [Route("api/Expenses")]
    
    public class ExpensesController : Controller
    {
        // GET: api/Expenses
        [HttpGet]
        [Authorize]
        public IEnumerable<string> Get()
        {
            return new string[] { "Expense 1", "Expense 2" };
        }

        // GET: api/Expenses/5
        [HttpGet("{id}", Name = "Get")]
        public string Get(int id)
        {
            return "value";
        }
        
        // POST: api/Expenses
        [HttpPost]
        public void Post([FromBody]string value)
        {
        }
        
        // PUT: api/Expenses/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }
        
        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
