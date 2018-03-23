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
    public class Select : IDialog<object>
    {
        private const string addoption = "新增借用";
        private const string checkoption = "查詢借用";
        private const string deloption = "刪除借用";
        public async Task StartAsync(IDialogContext context)
        {
            this.ShowOption(context);
        }

        private async Task MessageRecivedAsync(IDialogContext context, IAwaitable<object> result)
        {
            this.ShowOption(context);
        }

        private void ShowOption(IDialogContext context)
        {
            PromptDialog.Choice(context, this.optionselected, new List<string>() { addoption, checkoption, deloption }, "請選擇功能", "錯誤", 3);
        }

        private async Task optionselected(IDialogContext context, IAwaitable<string> result)
        {
            try
            {
                string select = await result;

                switch (select)
                {
                    case addoption:
                        context.Call(new Addroom(), this.ResumeAfterOptionDialog);
                        break;
                    case checkoption:
                        context.Call(new Chkroom(), this.ResumeAfterOptionDialog);
                        break;
                    case deloption:
                        context.Call(new Delroom(), this.ResumeAfterOptionDialog);
                        break;
                }
            }
            catch (TooManyAttemptsException ex)
            {
                await context.PostAsync($"Ooops! Too many attemps :(. But don't worry, I'm handling that exception and you can try again!");

                context.Wait(this.MessageRecivedAsync);
            }
        }

        private async Task borrowAsync(IDialogContext context, IAwaitable<object> result)
        {
            var msg = await result as Activity;
            await context.PostAsync(msg.Text);
        }
        private async Task ResumeAfterOptionDialog(IDialogContext context, IAwaitable<object> result)
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
        }
    }
   
}