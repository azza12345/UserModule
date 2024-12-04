using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core
{
    public enum CustomStatusCode
    {
        Success =200,
        BadRequest = 400,
        Unauthorized = 3,
        NotFound = 401,
        InternalServerError = 500
       
    }
}
