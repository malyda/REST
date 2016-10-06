using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using REST.Entity;

namespace REST.Interfaces
{
    interface IWebClient
    {
       Task< List<Person> > GetPersonsListAsync();
    }
}
