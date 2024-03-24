using CircitApi.Infrastructure.Filters;
using CircitApi.Infrastructure.Helpers;
using CircitApi.Infrastructure.Models;
using CircitApi.Infrastructure.Services;
using CircitApi.Services;

var builder = WebApplication.CreateBuilder(args);


builder.Services.Configure<AppSettings>(builder.Configuration.GetSection("AppSettings"));
builder.Services.Configure<RapidApiSettings>(builder.Configuration.GetSection("ExternalApis:RapidApi"));


builder.Services.AddSingleton<IEventLogWriter, EventLogWriter>();
builder.Services.AddSingleton<IEventLoggerHelpers, EventLoggerHelpers>();
builder.Services.AddSingleton<IEventLogger, EventLogger>();
builder.Services.AddSingleton<IRapidApi, RapidApi>();


var rapidApiBaseUrl = builder.Configuration.GetSection("ExternalApis:RapidApi").GetValue<string>("BaseUrl");


builder.Services.AddHttpClient("RapidApi", httpClient => {
    httpClient.BaseAddress = new Uri(rapidApiBaseUrl);
    httpClient.DefaultRequestHeaders.Add("X-RapidAPI-Key", "6b24bd0214mshd015cb8b7d427cap1a45f2jsn5fa2a0a67801");
});


builder.Services.AddControllers(options =>
{
    options.Filters.Add<CustomExceptionFilter>();
});


var app = builder.Build();
app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();