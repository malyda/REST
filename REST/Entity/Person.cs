using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace REST.Entity
{
    /// <summary>
    /// Entity class mirrors web entity
    /// </summary>
     public class Person
    {
        public string name { get; set; }
        public string surname { get; set; }
        public int rc { get; set; }
        public string datNar { get; set; }

         public override string ToString()
         {
             return $"name: {name}, surname: {surname}, rc: {rc}, datNar: {datNar}";
         }
    }
}
