using Microsoft.Bot.Builder.Dialogs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Threading.Tasks;
using Microsoft.Bot.Connector;

namespace Bot_Application_system_final.Dialogs
{
    [Serializable]
    public class Delroom : IDialog<object>
    {
        public async Task StartAsync(IDialogContext context)
        {
            context.Wait(GoCard);
            var msg = context.MakeMessage();
            var attachment = GetHeroCard();
            msg.Attachments.Add(attachment);

            await context.PostAsync(msg);

            context.Done<object>(null);
        }

        private async Task GoCard(IDialogContext context, IAwaitable<object> result)
        {
            /*var msg = context.MakeMessage();
            var attachment = GetHeroCard();
            msg.Attachments.Add(attachment);

            await context.PostAsync(msg);

            context.Done<object>(null);*/
        }

        private static Attachment GetHeroCard()
        {
            var herocard = new HeroCard
            {
                Title = "練團室聯絡平台",
                Subtitle = "yeee",
                Text = "解除會員、取消借用時段、客服疑問等",
                Buttons = new List<CardAction> { new CardAction(ActionTypes.OpenUrl, "聯絡客服", value: "") }
            };
            return herocard.ToAttachment();
        }
    }
}