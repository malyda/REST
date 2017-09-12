using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using REST.Entity;
using REST.Interfaces;

namespace REST
{
    class JsonParser : IParser
    {
       /// <summary>
       /// Parse JSON string to object entity
       /// </summary>
       /// <typeparam name="T">Object entity</typeparam>
       /// <param name="response">Json string</param>
       /// <returns>Task with deserilization</returns>
       public async Task<T> ParseStringAsync<T>(string response)
       {
            return await Task.Factory.StartNew(() => JsonConvert.DeserializeObject<T>(response));
       }

    }
}
