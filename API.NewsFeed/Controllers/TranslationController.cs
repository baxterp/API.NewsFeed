using API.NewsFeed.Helpers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OpenAI_API.Chat;

namespace API.NewsFeed.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TranslationController : ControllerBase
    {
        //https://rssapi.baxterpearson.co.uk/api/translation/gettranslation
        [HttpPost]
        [Route("gettranslation")]
        public IActionResult GetTranslation([FromBody] List<string> prompts)
        {
            var languageFrom = prompts[0];
            var languageTo = prompts[1];
            var textToTranslate = prompts[2];

            OpenAIHelper openAIHelper = new OpenAIHelper();
            string systemPrompt = "You will be provided with a sentence in "
                + languageFrom + ", and your task is to translate it into "
                + languageTo;
            string userPrompt = textToTranslate;

            var systemMessage = new ChatMessage(ChatMessageRole.System, systemPrompt);
            var userMessages = new List<ChatMessage> { new ChatMessage(ChatMessageRole.User, userPrompt) };

            var textResponse = openAIHelper.GetReponseFromPrompts(systemMessage, userMessages);

            return Ok(textResponse);
        }

        [HttpGet]
        [Route("ping")]
        public IActionResult GetPing()
        {
            return Ok();
        }
    }
}
