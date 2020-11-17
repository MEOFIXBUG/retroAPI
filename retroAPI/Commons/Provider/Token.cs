using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace retroAPI.Commons.Provider
{
    public class Token
    {
        public string token { get; set; }
        public Token(string _token)
        {
            token = _token;
        }
    }
}
