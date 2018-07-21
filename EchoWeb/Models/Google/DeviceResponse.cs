using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EchoWeb.Models.Google
{
    public class DeviceResponse
    {
        public string fulfillmentText { get; set; }
        public List<ResponseFulfillmentMessage> fulfillmentMessages { get; set; }
        public string source { get; set; }
        public Payload payload { get; set; }
        public List<OutputContext> outputContexts { get; set; }
        public FollowupEventInput followupEventInput { get; set; }
    }

    public class Button
    {
        public string text { get; set; }
        public string postback { get; set; }
    }

    public class Card
    {
        public string title { get; set; }
        public string subtitle { get; set; }
        public string imageUri { get; set; }
        public List<Button> buttons { get; set; }
    }

    public class ResponseFulfillmentMessage
    {
        public Card card { get; set; }
    }

    public class SimpleResponse
    {
        public string textToSpeech { get; set; }
    }

    public class Item
    {
        public SimpleResponse simpleResponse { get; set; }
    }

    public class RichResponse
    {
        public List<Item> items { get; set; }
    }

    public class Google
    {
        public bool expectUserResponse { get; set; }
        public RichResponse richResponse { get; set; }
    }

    public class Facebook
    {
        public string text { get; set; }
    }

    public class Slack
    {
        public string text { get; set; }
    }

    public class Payload
    {
        public Google google { get; set; }
        public Facebook facebook { get; set; }
        public Slack slack { get; set; }
    }

    public class FollowupEventInput
    {
        public string name { get; set; }
        public string languageCode { get; set; }
        public Parameters parameters { get; set; }
    }
}