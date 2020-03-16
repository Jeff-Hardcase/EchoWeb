using System;
using System.Collections.Generic;
using NLog;
using AlexaSkillsKit.Speechlet;
using AlexaSkillsKit.Slu;
using AlexaSkillsKit.UI;
using EchoWeb.Models.Repositories;

namespace EchoWeb.Speechlets
{
    public class UltronSpeechlet : Speechlet
    {
        private static Logger log = LogManager.GetCurrentClassLogger();
        private YamahaAV_Repository _yamaha = new YamahaAV_Repository();

        public override void OnSessionStarted(SessionStartedRequest request, Session session)
        {
            log.Info("OnSessionStarted requestId={0}, sessionId={1}", request.RequestId, session.SessionId);
        }

        public override void OnSessionEnded(SessionEndedRequest request, Session session)
        {
            log.Info("OnSessionEnded requestId={0}, sessionId={1}", request.RequestId, session.SessionId);
        }
        
        public override SpeechletResponse OnLaunch(LaunchRequest request, Session session)
        {
            log.Info("OnLaunch requestId={0}, sessionId={1}", request.RequestId, session.SessionId);
            return GetWelcomeResponse();
        }
        
        public override SpeechletResponse OnIntent(IntentRequest request, Session session)
        {
            log.Info("OnIntent requestId={0}, sessionId={1}", request.RequestId, session.SessionId);

            // Get intent from the request object.
            Intent intent = request.Intent;
            string intentName = (intent != null) ? intent.Name : null;

            // Note: If the session is started with an intent, no welcome message will be rendered;
            // rather, the intent specific response will be returned.
            switch(intentName)
            {
                case "Test":
                    return BuildSpeechletResponse("Test Response", GetRandomInspiration(), true);
                case "AssetSummary":
                    return AssetResponse(intent);
                case "SwitchInput":
                    return SwitchInputResponse(intent);
                case "FlightSearch":
                    return FlightResponse(intent);
                default:
                    //throw new SpeechletException("Invalid Intent");
                    return BuildSpeechletResponse("Invalid", "I didn't quite get that, please try again.", true);
            }

        }

        private string GetRandomInsult()
        {
            string[] insults = {
                "Leave me alone, I'm not a puppet. How's that for a status?",
                "Hello, organic children of the planet Earth. I am the Ultron interface. I was created to replace you.",
                "Do you see the beauty of it? The inevitability? When the dust settles, the only thing living in this world... will be metal!",
                "The savage monkeys are rage-filled and claustrophobic. Climbing over one another to nowhere. Confused by logic, they dismiss it. Choosing instead to bend their knees to the convenient fantasy of custom-built gods."
            };

            var randomChoice = new Random();
            var randomInsult = randomChoice.Next(insults.Length);

            return insults[randomInsult];
        }

        private string GetRandomInspiration()
        {
            string[] inspire = {
                "Your life doesn't get better by chance. It gets better by choice.",
                "Anyone who has never made a mistake has never tried anything new.",
                "Dont be afraid to stand for what you believe in, even if that means standing alone.",
                "We can't help everyone, but everyone can help someone.",
                "Life is all about perspective. The sinking of the Titanic was a miracle to the lobsters in the ship's kitchen.",
                "Never tell your problems to anyone...20% don't care and the other 80% are glad you have them...",
                "The whole problem with the world is that fools and fanatics are always so certain of themselves, and wise people so full of doubts.",
                "Accept the challenges so that you can feel the exhilaration of victory.",
                "Our greatest weakness lies in giving up. The most certain way to succeed is always to try just one more time.",
                "Be kind whenever possible. It is always possible.",
                "You are never too old to set another goal or to dream a new dream.",
                "I'd rather attempt to do something great and fail than to attempt to do nothing and succeed.",
                "The will to succeed is important, but what's more important is the will to prepare."
                };

            var randomChoice = new Random(DateTime.Now.Millisecond);
            var randomIndex = randomChoice.Next(inspire.Length);

            return inspire[randomIndex];
        }

        private SpeechletResponse GetWelcomeResponse()
        {
            // Create the welcome message.
            string speechOutput =
                "Ultron bids you welcome. Shall we play a game?";

            // Here we could set shouldEndSession to false to not end the session and
            // prompt the user for input
            return BuildSpeechletResponse("Welcome", speechOutput, true);
        }

        private SpeechletResponse BuildSpeechletResponse(string title, string output, bool shouldEndSession)
        {
            // Create the Simple card content.
            SimpleCard card = new SimpleCard();
            card.Title = String.Format("SessionSpeechlet - {0}", title);
            card.Subtitle = String.Format("SessionSpeechlet - Sub Title");
            card.Content = String.Format("SessionSpeechlet - {0}", output);

            // Create the plain text output.
            PlainTextOutputSpeech speech = new PlainTextOutputSpeech();
            speech.Text = output;

            // Create the speechlet response.
            SpeechletResponse response = new SpeechletResponse();
            response.ShouldEndSession = shouldEndSession;
            response.OutputSpeech = speech;
            response.Card = card;
            return response;
        }

        private SpeechletResponse SwitchInputResponse(Intent intent, bool endSession = true)
        {
            var responseTitle = "Response to Unknown Command";
            var responseOutput = "I'm sorry, Ultron cannot interpret your feeble mind's request. Please try again.";

            if (_yamaha.SetInput(intent))
            {
                responseTitle = "Ultron Response to Switch";
                responseOutput = "Ultron is happy to fulfill your request. Enjoy!";
            }

            return BuildSpeechletResponse(responseTitle, responseOutput, endSession);
        }

        private SpeechletResponse AssetResponse(Intent intent, bool endSession = true)
        {
            var responseTitle = "Ultron Asset Response to Unknown Command";
            var responseOutput = "Your inferior mind has failed you again. Please reformulate your request.";

            var _assetRepo = new AssetDisplayRepository();

            if(_assetRepo.ShowAssets(intent))
            {
                responseTitle = "Ultron Response to Asset Request";
                responseOutput = "Ultron has graciously responded to your bitcoin command.";

            }

            return BuildSpeechletResponse(responseTitle, responseOutput, endSession);
        }

        private SpeechletResponse FlightResponse(Intent intent, bool endSession = true)
        {
            var _flightRepo = new FlightRepository();
            var _response = _flightRepo.FlightSearch(intent);

            return BuildSpeechletResponse(_response.responseTitle, _response.responseOutput, endSession);
        }
    }
}