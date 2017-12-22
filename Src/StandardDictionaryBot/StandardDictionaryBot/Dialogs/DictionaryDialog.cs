using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Connector;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Xml;
using System.Xml.Linq;
using AIMLbot;

namespace StandardDictionaryBot.Dialogs
{
    [Serializable]
    public class DictionaryDialog : IDialog<object>
    {
        public string WordToSearch { get; set; }

        public async Task StartAsync(IDialogContext context)
        {
            await context.PostAsync("Enter some word");
            context.Wait(MessageReceivedAsync);
        }

        private async Task MessageReceivedAsync(IDialogContext context, IAwaitable<object> result)
        {
            var message = await result as Activity;
            this.WordToSearch = message.Text.Trim().ToLower();
            if (this.WordToSearch == "end")
            {
                context.Fail(new Exception("bye bye"));
            }
            else
            {
                await context.PostAsync("Meaning fo the word " + this.WordToSearch);
                context.Done("End");
            }
        }
       
    }
}