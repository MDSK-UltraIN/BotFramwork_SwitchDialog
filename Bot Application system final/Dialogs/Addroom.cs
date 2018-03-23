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
    public class Addroom : IDialog<object>
    {
        string time = "";
        public async Task StartAsync(IDialogContext context)
        {
            await context.PostAsync("借練團室嗎?");
            await context.PostAsync("輸入借用日期跟使用時間就可以囉(e.g. 2020/1/1 19:00 2hr)");
            context.Wait(this.keyintime);
            
        }

        private async Task keyintime(IDialogContext context, IAwaitable<object> result)
        {
            var msg = await result as Activity;
            time = msg.Text;
            await context.PostAsync("確認借用?");
            context.Wait(this.comfirm);
        }

        private async Task comfirm(IDialogContext context, IAwaitable<object> result)
        {
            var msg = await result as Activity;
            await context.PostAsync($"{time} 借用成功");
            context.Call(new Select(),ResumeAfterOptionDialog);
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
                context.Wait(this.keyintime);
            }
        }
    }
}