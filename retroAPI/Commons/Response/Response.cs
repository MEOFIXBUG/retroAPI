using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace retroAPI.Commons.Response
{
    public class Response : BaseResponse
    {
        public object Data { get; private set; }

        private Response(bool success, string message, object data) : base(success, message)
        {
            Data = data;
        }

        /// <summary>
        /// Creates a success response.
        /// </summary>
        /// <param name="">.</param>
        /// <returns>Response.</returns>
        public Response(object data) : this(true, string.Empty, data)
        { }

        /// <summary>
        /// Creates am error response.
        /// </summary>
        /// <param name="message">Error message.</param>
        /// <returns>Response.</returns>
        public Response(string message) : this(false, message, null)
        { }


    }
}
