using System.Net;
using Newtonsoft.Json;

namespace Presentation.WebAPI.Middleware
{
    public class ApiError
    {
        public int Status { get; set; }

        public string Description { get; set; }

        public string Message { get; set; }

        public ApiError()
        {
            this.Status = (int)HttpStatusCode.InternalServerError;
            this.Description = HttpStatusCode.InternalServerError.ToString();
            this.Message = "Oops! Something went wrong";
        }

        public ApiError(int status, string errorDescription, string message)
        {
            this.Status = status;
            this.Description = errorDescription;
            this.Message = message;
        }

        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
}