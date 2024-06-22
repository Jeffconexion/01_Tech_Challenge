using LocalFriendzApi.Commom.Api;
using LocalFriendzApi.Endpoints;

var builder = WebApplication.CreateBuilder(args);
builder.AddDataContexts();
builder.AddDocumentation();
builder.AddServices();

var app = builder.Build();
app.MapEndpoints();


if (app.Environment.IsDevelopment())
{
    app.ConfigureDevEnvironment();
}

app.UseHttpsRedirection();

app.Run();
