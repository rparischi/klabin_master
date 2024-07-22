using System.Collections.Generic;

namespace Klabin.Rml.ClientLogic
{
    public class Warning
    {
        public string Code { get; set; }

        public string Message { get; set; }
    }

    public class Metadata
    {
        public IEnumerable<Warning> Warning { get; set; }
    }

    public class ApiResponseBody
    {
        public Metadata Metadata { get; set; }
    }

    public class ApiResponseBody<T> : ApiResponseBody
    {
        public T Data { get; set; }
    }
}
