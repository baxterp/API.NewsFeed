using OpenAI_API.Chat;
using OpenAI_API.Models;
using OpenAI_API;

namespace API.NewsFeed.Helpers
{
    public class OpenAIHelper
    {
        private OpenAIAPI CreateChatClient()
        {
            try
            {
                var openAiApiKey = Environment.GetEnvironmentVariable("OpenAIKey", EnvironmentVariableTarget.User);

                APIAuthentication aPIAuthentication = new APIAuthentication(openAiApiKey);
                var openAiApi = new OpenAIAPI(aPIAuthentication);

                return openAiApi;
            }
            catch (Exception ex)
            {
                System.IO.File.WriteAllText("CreateChatClient.txt", ex.Message);
                throw;
            }
        }

        public async Task<string> GetReponseFromPrompts(ChatMessage systemPrompt, List<ChatMessage> userPrompts)
        {
            try
            {
                var openAiApi = CreateChatClient();
                var maxTokens = 500;
                var combinedMessages = new List<ChatMessage> { systemPrompt };
                foreach (var item in userPrompts) combinedMessages.Add(item);

                var completionRequest = new ChatRequest()
                {
                    Model = Model.ChatGPTTurbo,
                    Temperature = 0.0,
                    MaxTokens = maxTokens,
                    Messages = combinedMessages.ToArray()
                };

                var completionResult = await openAiApi.Chat.CreateChatCompletionAsync(completionRequest);
                var generatedText = completionResult.Choices[0].Message.TextContent;

                return generatedText;
            }
            catch (Exception ex)
            {
                System.IO.File.WriteAllText("OpenAIErrorLog.txt", ex.Message);
                throw;
            }
        }
    }
}
