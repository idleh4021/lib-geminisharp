using Newtonsoft.Json.Linq;
using System.Net;
using System.Text;

namespace GeminiSharp
{
    public class GeminiAPI
    {
        public string API_KEY { private get; set; } = string.Empty;
        public string API_URL { get; set; } = "https://generativelanguage.googleapis.com/v1beta/models/gemini-2.0-flash:generateContent"; // Default URL, can be overridden
        private string END_POINT { get { return API_URL + "?key=" + API_KEY; } }
        
        ChatManager chatManager;

        public GeminiAPI(string apiKey, int maxTurns = 10)
        {
            API_KEY = apiKey;
            chatManager = new ChatManager(maxTurns);
        }

        public void SetSystemPrompt(string prompt)
        {
            chatManager.SystemPrompt = prompt;
        }

        public async Task<string> SendMessageAsync(string prompt)
        {
            //string prompt = richTextBox1.Text;
            chatManager.AddUserMessage(prompt);
            // txtResponse.Text = "Thinking...";
            string responseMessage = string.Empty;
            using (var client = new HttpClient())
            {
                //var requestBody = new
                //{
                //    contents = new[]
                //    {
                //        new
                //        {
                //            parts = new[]
                //            {
                //                new { text = prompt }
                //            }
                //        }
                //    }
                //};

                //var json = JsonConvert.SerializeObject(requestBody);
                var json = chatManager.GetRequestJson();
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                var response = await client.PostAsync(END_POINT, content);
                string result = await response.Content.ReadAsStringAsync();

                try
                {
                    JObject parsed = JObject.Parse(result);
                    var text = parsed["candidates"]?[0]?["content"]?["parts"]?[0]?["text"]?.ToString();
                    responseMessage = text ?? "No response.";
                    if (responseMessage != "No response.") chatManager.AddModelMessage(responseMessage);
                    //listBox1.Items.Add(tt);
                }
                catch (Exception ex)
                {
                    responseMessage = "Error parsing response: " + ex.Message;
                   // listBox1.Items.Add(tt);
                }
                return responseMessage; 
            }
        }
    }
}
