using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeminiSharp
{
    // 대화 한 턴을 저장하는 클래스
    public class ChatTurn
    {
        public string role { get; set; }    // "user" 또는 "model"
        public Part[] parts { get; set; }

        public ChatTurn(string role, string text)
        {
            this.role = role;
            this.parts = new Part[] { new Part { text = text } };
        }
    }

    public class Part
    {
        public string text { get; set; }
    }

    internal class ChatManager
    {
        private Queue<ChatTurn> conversationQueue = new Queue<ChatTurn>();
        private int maxTurns;
        private ChatTurn systemPromptTurn;
        internal string SystemPrompt
        {
            set
            {
                systemPromptTurn = new ChatTurn("user", value);
            }
        }

        internal ChatManager(int maxTurns = 10)
        {
            this.maxTurns = maxTurns;
            // AddSystemPrompt();
        }

        //private void AddSystemPrompt()
        //{
        //    if (string.IsNullOrEmpty(SystemPrompt)) return;
        //    var systemPrompt = SystemPrompt;
        //    var promptTurn = new ChatTurn("user", systemPrompt);
        //    conversationQueue.Enqueue(promptTurn);
        //}

        internal void AddUserMessage(string message)
        {
            EnqueueTurn(new ChatTurn("user", message));
        }

        internal void AddModelMessage(string message)
        {
            EnqueueTurn(new ChatTurn("model", message));
        }

        private void EnqueueTurn(ChatTurn turn)
        {
            conversationQueue.Enqueue(turn);

            while (conversationQueue.Count > maxTurns)
            {
                conversationQueue.Dequeue();
            }
        }

        internal string GetRequestJson()
        {
            var contents = new List<ChatTurn>();

            if (systemPromptTurn != null) contents.Add(systemPromptTurn);
            contents.AddRange(conversationQueue.ToArray());
            var requestBody = new { contents = contents };
            return JsonConvert.SerializeObject(requestBody);
        }
    }
}
