using BlazorApp2;
using BlazorApp2.Components;
using BlazorApp2.DB;
using System.Net.Http;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

builder.Services.AddHttpClient("apiClient", s => s.BaseAddress = new Uri("https://192.168.1.85:5000/") );
builder.Services.AddHttpClient("rssClient", s => s.BaseAddress = new Uri("https://primamedia.ru/export/new/"));

builder.Services.AddTransient<User02Context>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
}

app.UseStaticFiles();
app.UseAntiforgery();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

// ��� ���������� ��� ��������� ����� ics �� �������
app.Map("/download/{id}", async (int id, IHttpClientFactory httpClientFactory) => {

    var httpClient = httpClientFactory.CreateClient("apiClient");
    // ������ � ���, ����� ������ �� ������ �������
   // var eventObj = await httpClient.GetFromJsonAsync<Event>($"event/{id}");
    string file = "��������� ���� �� eventObj �� ������ �� ��������, ��� ��� ����������";
    // �������� ����� �� ������
    var bytes = System.Text.Encoding.UTF8.GetBytes(file); 
    // ���������� ���������� � ���� ���������� �����
    // �� ��������, �� �������� ������������ �����
    return Results.File(bytes, "text/plain", $"{id}.ics");
});

app.Run();
