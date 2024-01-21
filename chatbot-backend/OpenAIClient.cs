using System;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

public class OpenAIClient
{
    private readonly HttpClient httpClient;
    private readonly string apiKey;

    public OpenAIClient(string apiKey)
    {
        this.apiKey = apiKey;
        this.httpClient = new HttpClient();
    }

    public async Task<string> GetAssistantResponse(string userQuestion)
    {
        var openAiEndpoint = "https://api.openai.com/v1/chat/completions";

        var requestData = new
        {
            model = "gpt-3.5-turbo",
            messages = new[]
            {
                new
                {
                    role = "system",
                    content = "You are an assistant for a finance website which has features like - listing the bills that a user has to pay; get the list of payments made for these bills; mark a bill as paid; create new bills to be paid monthly on a specific date, or a prepaid bill to be renewed every X days. With all questions, respond back with a relevance score of whether the question that is asked is relevant to this assistant, respond with 'I'm not sure if I can help with that' in the right language if the relevance score is less than 75%; You HAVE TO ALWAYS RETURN CONTENT IN JSON, and it should have a content (string) property and the relevance_score (double value) property"
                },
                new
                {
                    role = "user",
                    content = userQuestion
                }
            }
        };

        var requestDataJson = JsonSerializer.Serialize(requestData);

        var request = new HttpRequestMessage(HttpMethod.Post, openAiEndpoint);
        // request.Headers.Add("Content-Type", "application/json");
        request.Headers.Add("Authorization", $"Bearer {apiKey}");
        request.Content = new StringContent(requestDataJson, Encoding.UTF8, "application/json");

        var response = await httpClient.SendAsync(request);

        if (response.IsSuccessStatusCode)
        {
            var responseBody = await response.Content.ReadAsStringAsync();
            // Parse and extract the assistant's response from responseBody
            return responseBody;
        }
        else
        {
            throw new InvalidOperationException($"OpenAI API request failed with status code {response.StatusCode}");
        }
    }
}
