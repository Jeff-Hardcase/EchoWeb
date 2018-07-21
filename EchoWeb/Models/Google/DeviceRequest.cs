using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EchoWeb.Models.Google
{
    public class DeviceRequest
    {
        public string responseId { get; set; }
        public string session { get; set; }
        public QueryResult queryResult { get; set; }
        public OriginalDetectIntentRequest originalDetectIntentRequest { get; set; }
    }

    public class Parameters
    {
        public string param { get; set; }
    }

    public class Text
    {
        public List<string> text { get; set; }
    }

    public class RequestFulfillmentMessage
    {
        public Text text { get; set; }
    }

    public class OutputContext
    {
        public string name { get; set; }
        public int lifespanCount { get; set; }
        public Parameters parameters { get; set; }
    }

    public class Intent
    {
        public string name { get; set; }
        public string displayName { get; set; }
    }

    public class DiagnosticInfo
    {
    }

    public class QueryResult
    {
        public string queryText { get; set; }
        public Parameters parameters { get; set; }
        public bool allRequiredParamsPresent { get; set; }
        public string fulfillmentText { get; set; }
        public List<RequestFulfillmentMessage> fulfillmentMessages { get; set; }
        public List<OutputContext> outputContexts { get; set; }
        public Intent intent { get; set; }
        public int intentDetectionConfidence { get; set; }
        public DiagnosticInfo diagnosticInfo { get; set; }
        public string languageCode { get; set; }
    }

    public class OriginalDetectIntentRequest
    {
    }
}