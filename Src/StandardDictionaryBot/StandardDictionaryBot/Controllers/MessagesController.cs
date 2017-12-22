using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Connector;


namespace StandardDictionaryBot
{
    [BotAuthentication]
    public class MessagesController : ApiController
    {

        public async Task<HttpResponseMessage> Post([FromBody]Activity activity)
        {
            if (activity.Type == ActivityTypes.ConversationUpdate)
            {
                #region Show Welcome Message
                if (activity.MembersAdded != null && activity.MembersAdded.Any())
                {
                    foreach (var newMember in activity.MembersAdded)
                    {
                        if (newMember.Id != activity.Recipient.Id)
                        {
                            var connector = new ConnectorClient(new Uri(activity.ServiceUrl));
                            var message = $"Welcome { newMember.Name} !. I am a Dictionary Bot.<br/>" ;
                            message += "Please Type Some Word";
                            var reply = activity.CreateReply(message);
                            await connector.Conversations.ReplyToActivityAsync(reply);
                        }
                    }
                }
                #endregion
            }
            else if (activity.Type == ActivityTypes.Message)
            {
                try
                {
                    await Conversation.SendAsync(activity, () => new Dialogs.RootDialog());
                }
                catch(Exception)
                {
                    var connector = new ConnectorClient(new Uri(activity.ServiceUrl));
                    var reply = activity.CreateReply("bye");
                    await connector.Conversations.ReplyToActivityAsync(reply);
                    
                }
            }
            else if (activity.Type == ActivityTypes.DeleteUserData)
            {
                return null;
            }
            else if (activity.Type == ActivityTypes.ContactRelationUpdate)
            {
                return null;
            }
            else if (activity.Type == ActivityTypes.Typing)
            {
                return null;
            }
            else if (activity.Type == ActivityTypes.Ping)
            {
                return null;
            }
            
            var response = Request.CreateResponse(HttpStatusCode.OK);
            return response;
        }

       
    }
}