using System.Diagnostics.CodeAnalysis;
using System.Net.Http.Headers;
using System.Security.Cryptography.Xml;
using System.Web.WebPages.Html;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SixLabors.ImageSharp;
using UTCN_ProjectsWebApp.Models;
using System.Net.Http;
using System.Net.Http.Headers;

namespace UTCN_ProjectsWebApp.Pages;

public class IndexModel : PageModel
{
    private readonly ILogger<IndexModel> _logger;

    public List<Message> messages;
    

    public IndexModel(ILogger<IndexModel> logger)
    {
        _logger = logger;
        messages = new List<Message>();
        

    }



public void OnGet()
    {
    }

    public void OnPost()
    {
        
    }
    public async Task OnPostButton()
    {
       
        if (Request.Form.ContainsKey("getall"))
        {

            using var client = new HttpClient();
            client.BaseAddress = new Uri(@"https://rest-apiproiect2utcn20221212231729.azurewebsites.net/api/Messages");
            
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));
            
            var response = client.GetAsync("").Result;
            if (response.IsSuccessStatusCode)
            {
                var dataObjects = response.Content
                    .ReadAsAsync<IEnumerable<Message>>().Result;
                foreach (var d in dataObjects)
                {
                    messages.Add(d);
                }
            }
        }
        else if (Request.Form.ContainsKey("getone"))
        {
            using var client = new HttpClient();
            client.BaseAddress = new Uri($"https://rest-apiproiect2utcn20221212231729.azurewebsites.net/api/Messages/paul/{Request.Form["idreq"]}");
            
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));
            
            var response = client.GetAsync("").Result;
            if (response.IsSuccessStatusCode)
            {
                var dataObjects = response.Content
                    .ReadAsAsync<IEnumerable<Message>>().Result;
                foreach (var d in dataObjects)
                {
                    messages.Add(d);
                }
            }
        }
        else if(Request.Form.ContainsKey("clear"))
        {
           
            messages.Clear();
        }
        
    }

}