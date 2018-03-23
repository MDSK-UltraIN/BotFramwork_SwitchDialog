using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Connector;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Bot_Application_system_final.Dialogs
{
    [Serializable]
    public class login : IDialog<object>
    {
        string uid = "", pwd = "";

        private const string HeroCard = "Hero Card";
        private IEnumerable<string> options = new List<string> { HeroCard };


        public async Task StartAsync(IDialogContext context)
        {

            //context.Wait(this.MessageRecivedAsync);



            context.Wait(this.loginuidAsync);
        }

        private async Task loginuidAsync(IDialogContext context, IAwaitable<object> result)
        {
            var msg = await result as Activity;
            await context.PostAsync("請輸入帳號");

            context.Wait(this.loginpwdAsync);
        }

        private async Task loginpwdAsync(IDialogContext context, IAwaitable<object> result)
        {
            var msg = await result as Activity;
            await context.PostAsync("請輸入密碼");
            uid = msg.Text;

            context.Wait(MessageRecivedAsync);

        }

        private async Task MessageRecivedAsync(IDialogContext context, IAwaitable<object> result)
        {
            /*var message = await result;

            PromptDialog.Choice < string >(
                context,
                this.DisplaySelectedCard,
                this.options,
                "What card would like to test?",
                "Ooops, what you wrote is not a valid option, please try again",
                3,
                PromptStyle.PerLine);
            }*/

            await context.PostAsync($"{uid} 歡迎回來  我可以怎麼幫你?");
            context.Call(new Select(), this.ResumeAfterloginDialog);
            //this.ShowOption(context);
            /*var message = context.MakeMessage();

            var attachment = GetHeroCard();
            message.Attachments.Add(attachment);
            await context.PostAsync(message);
            context.Wait(borrowAsync);*/


        }
        private async Task ResumeAfterloginDialog(IDialogContext context, IAwaitable<object> result)
        {
            try
            {
                var message = await result;
            }
            catch (Exception ex)
            {
                await context.PostAsync($"Failed with message: {ex.Message}");
            }
            finally
            {
                context.Wait(this.MessageRecivedAsync);
            }



            /*private static Attachment GetHeroCard()
            {





                return heroCard.ToAttachment();

            }*/

        }
    }
}