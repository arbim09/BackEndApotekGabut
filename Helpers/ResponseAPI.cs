using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Apotek.Helpers
{
    public class ResponseAPI
    {
        public string Token { get; set; } = string.Empty;
        public string RefreshToken { get; set; } = string.Empty;
        public bool Status { get; set; } = false;
        public int StatusCode { get; set; } = 200;
        public string Message { get; set; } = string.Empty;
        public dynamic Data { get; set; } = string.Empty;
    }
}