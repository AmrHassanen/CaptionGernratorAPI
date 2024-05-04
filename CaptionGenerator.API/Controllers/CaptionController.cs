using Microsoft.AspNetCore.Mvc;
using System;
using System.Net.Http;
using System.Threading.Tasks;

[Route("api/[controller]")]
[ApiController]
public class CaptionController : ControllerBase
{
    private readonly HttpClient _client;

    public CaptionController()
    {
        _client = new HttpClient();
    }

    [HttpPost("generate_caption_eng")]
    public async Task<ActionResult<string>> GenerateCaption([FromBody] CaptionRequest request)
    {
        try
        {
            // Send image URL to the model endpoint for caption generation
            HttpResponseMessage response = await _client.PostAsync("http://0.0.0.0:8000/api/generate_caption_eng", new StringContent(request.ImageUrl));
            response.EnsureSuccessStatusCode();
            string caption = await response.Content.ReadAsStringAsync();

            return Ok(caption);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
}

public class CaptionRequest
{
    public string ImageUrl { get; set; }
}
