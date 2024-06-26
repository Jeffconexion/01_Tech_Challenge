using LocalFriendzApi.Commom.Api;
using LocalFriendzApi.Endpoints;
using LocalFriendzApi.Middlewares;

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

app.UseLoggingMiddleware();
app.UseHttpsRedirection();

app.Run();
