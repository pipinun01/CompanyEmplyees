using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;

namespace Entities.ErrorModel
{
    public class ErrorDetails
    {
        public int statusCode {  get; set; }
        public string? message { get; set; }

        public override string ToString()=> JsonSerializer.Serialize(this);
    }
}
