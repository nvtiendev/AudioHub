<<<<<<< HEAD
using AudioHub.Core;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
=======
var builder = WebApplication.CreateBuilder(args);

>>>>>>> 8c372e7 (Initialize professional full-stack AudioHub project)
builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.PropertyNamingPolicy = System.Text.Json.JsonNamingPolicy.CamelCase;
        options.JsonSerializerOptions.PropertyNameCaseInsensitive = true;
    });
<<<<<<< HEAD

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo { 
        Title = "AudioHub API", 
        Version = "v1",
        Description = "API cung cấp tính năng tải nhạc và lấy thông tin từ ZingMP3."
    });
    
    var xmlFile = $"{System.Reflection.Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    if (File.Exists(xmlPath))
    {
        c.IncludeXmlComments(xmlPath);
    }
});
builder.Services.AddOpenApi();
builder.Services.AddHttpClient();

// Register AudioClient as a singleton
builder.Services.AddSingleton<AudioClient>(sp => {
    var client = new AudioClient();
    // We attempt to initialize synchronously for the singleton instance 
    // to ensure the first request has a valid key.
    try {
        client.InitializeAsync().GetAwaiter().GetResult();
    } catch (Exception ex) {
        Console.WriteLine($"[AudioHub API] WARNING: Initial client setup failed: {ex.Message}");
    }
    return client;
});

=======
builder.Services.AddEndpointsApiExplorer();
>>>>>>> 8c372e7 (Initialize professional full-stack AudioHub project)
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", builder => builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
});

var app = builder.Build();

<<<<<<< HEAD
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment() || true) // Always enable for now as requested
{
    app.UseSwagger();
    app.UseSwaggerUI(c => {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "AudioHub API V1");
        c.RoutePrefix = "swagger"; // Access at /swagger
    });
}

=======
>>>>>>> 8c372e7 (Initialize professional full-stack AudioHub project)
app.UseCors("AllowAll");
app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();
