using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace GamePadKeyboard
{
    public static class ExceptionHandler
    {
        public static async Task Handle(Exception exception)
        {
            if (UserSettings.GetSendCrashData())
            {
                var client = new HttpClient();
                var url =
                    "https://s-duo-gamepad.azurewebsites.net/api/HttpTrigger1?code=f6AzlkW/gDPozqX9LoY67tnIaF1kUkjaaBQH/nlY3jGbi7V/MwRlQg==";
                var request = new HttpRequestMessage(HttpMethod.Post, url)
                {
                    Content = new StringContent(exception.ToString())
                };
                await client.SendAsync(request);
            }

            UserSettings.SaveCrashLog(exception.ToString());
        }
    }
}