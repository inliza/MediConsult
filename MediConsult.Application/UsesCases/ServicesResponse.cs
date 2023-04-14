using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediConsult.Application.UsesCases
{
    public class ServicesResponse
    {
        public string Message { get; set; }
        public object Data { get; set; }
        public int Code { get; set; }

        public ServicesResponse(int code, string message, object data)
        {
            this.Code = code;
            this.Message = message;
            this.Data = data;
        }

    }
    public class ServicesResponse<T>
    {
        public string Message { get; set; }
        public T Data { get; set; }
        public int Code { get; set; }

        public ServicesResponse(int code, string message, T data)
        {
            this.Code = code;
            this.Message = message;
            this.Data = data;
        }

    }
}
