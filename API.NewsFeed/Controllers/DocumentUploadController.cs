using API.NewsFeed.Helpers;
using HarfBuzzSharp;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OpenAI_API.Chat;
using Spire.Doc;

namespace API.NewsFeed.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DocumentUploadController : ControllerBase
    {
        //https://rssapi.baxterpearson.co.uk/api/documentupload/WordDocSummarize
        [HttpPost]
        [Route("WordDocSummarize")]
        public async Task<IActionResult> WordDocSummarize(IFormFile file)
        {
            if (file == null)
                return BadRequest("No files were uploaded");

            Document document = new Document();
            using (var stream = file.OpenReadStream())
            {
                document.LoadFromStream(stream, FileFormat.Docx);
            }

            if (document == null || document.Sections.Count == 0)
                return BadRequest("No content found in the document");

            var text = document.GetText();

            if (string.IsNullOrEmpty(text))
                return BadRequest("No content found in the document");

            OpenAIHelper openAIHelper = new OpenAIHelper();
            string systemPrompt = "You are required to summerise the following text, in no more 100 words";
            string userPrompt = text;

            var systemMessage = new ChatMessage(ChatMessageRole.System, systemPrompt);
            var userMessages = new List<ChatMessage> { new ChatMessage(ChatMessageRole.User, userPrompt) };

            var textResponse = await openAIHelper.GetReponseFromPrompts(systemMessage, userMessages);
            var response = new { data = textResponse };

            return Ok(response);
        }

        [HttpGet]
        [Route("ping")]
        public IActionResult GetPing()
        {
            return Ok();
        }
    }
}
