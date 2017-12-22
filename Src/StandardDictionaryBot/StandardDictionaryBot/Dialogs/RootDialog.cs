using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Connector;
using StandardDictionaryBot.Utils;

namespace StandardDictionaryBot.Dialogs
{
    [Serializable]
    public class RootDialog : IDialog<object>
    {
        public async Task StartAsync(IDialogContext context)
        {
            await Task.FromResult("rootdialog"); 
            var dictionaryDialog = new DictionaryDialog();
            context.Call(dictionaryDialog, this.ShowPromptForNextWord);
        }

        private async Task MessageReceivedAsync(IDialogContext context, IAwaitable<object> result)
        {
            var message = await result as Activity;
            var dictionaryDialog = new DictionaryDialog();
            context.Call(dictionaryDialog, this.ShowPromptForNextWord);
        }

        public async Task ShowPromptForNextWord(IDialogContext context, IAwaitable<object> result)
        {

            if (context.Activity.AsMessageActivity().Text == "end")
            {
                context.Fail(new Exception());
            }
            else
            {
                var dictionaryDialog = new DictionaryDialog();
                await Task.Run(() => context.Call(dictionaryDialog, this.ShowPromptForNextWord));
            }
            
        }
    }
}