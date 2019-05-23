using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Amazon.Lambda.APIGatewayEvents;
using Amazon.Lambda.Core;
using Newtonsoft.Json;

// Assembly attribute to enable the Lambda function's JSON input to be converted into a .NET class.
[assembly : LambdaSerializer (typeof (Amazon.Lambda.Serialization.Json.JsonSerializer))]

namespace DebuggingExample {
    public class Functions {
        ITimeProcessor processor = new TimeProcessor();

        /// <summary>
        /// Default constructor that Lambda will invoke.
        /// </summary>
        public Functions () { }

        /// <summary>
        /// A Lambda function to respond to HTTP Get methods from API Gateway
        /// </summary>
        /// <param name="request">The http request that triggered this lambda function. Contains the request body, http headers, etc.</param>
        /// <param name="context">AWS context object passed to handler. Contains props w/ info about the invocation, function, and execution env</param>
        /// <returns>The list of blogs</returns>
        public APIGatewayProxyResponse Get (APIGatewayProxyRequest request, ILambdaContext context) {
            var result = processor.CurrentTimeUTC();

            return CreateResponse(result);
        }

        APIGatewayProxyResponse CreateResponse (DateTime? result) {
            int statusCode = (result != null) ?
                (int)HttpStatusCode.OK : (int)HttpStatusCode.InternalServerError;

            string body = (result != null) ?
                JsonConvert.SerializeObject(result) : string.Empty;

            var response = new APIGatewayProxyResponse {
                StatusCode = statusCode,
                Body = body,
                Headers = new Dictionary<string, string> { 
                    { "Content-Type", "application/json" },
                    { "Access-Control-Allow-Origin", "*" }
                }
            };

            return response;
        }
    }
}