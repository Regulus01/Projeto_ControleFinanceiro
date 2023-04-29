using System.Text;
using Domain.Authentication.Commands;
using Domain.Authentication.Shared.Response;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using SendGrid;
using SendGrid.Helpers.Mail;

namespace Domain.Authentication.Handle;

public partial class UsuarioCommandHandler
{
    private async Task<PessoaCriadaReponse?> RegistrarPessoaAnotherService(RegisterPessoaCommand pessoa)
    {
        var httpClient = new HttpClient();
        var url = "https://artemiswebapi.azurewebsites.net/api/Pessoa/v1/RegistrarPessoa";
        var json = JsonConvert.SerializeObject(pessoa);

        var content = new StringContent(json, Encoding.UTF8, "application/json");

        var response = await httpClient.PostAsync(url, content);
        var responseContent = await response.Content.ReadAsStringAsync();
        var jsonResponse = JObject.Parse(responseContent);
        var jsonResult = jsonResponse["result"]?.ToString();
        
        if (jsonResult == null)
            return null;
        
        var responseDs = JsonConvert.DeserializeObject<PessoaCriadaReponse>(jsonResult);

        return response.IsSuccessStatusCode ? responseDs : null;
    }

    private async Task EnviarEmailDeBoasVidas(string userEmail, string userName)
    {
        var apiKey = Environment.GetEnvironmentVariable("SENDGRID_API_KEY");

        if (apiKey == null)
            return;

        var client = new SendGridClient(apiKey);

        var from = new EmailAddress("josecssj.games@gmail.com", "Aline Ramos");
        var to = new EmailAddress(userEmail, userName);

        var userObject = new { username = userName };

        var templateMessage =
            MailHelper.CreateSingleTemplateEmail(from, to, "d-381280346da44f5794bac24e52acbb5f", userObject);
        await client.SendEmailAsync(templateMessage);
    }
}