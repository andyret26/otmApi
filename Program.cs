using Discord;
using Discord.WebSocket;
using dotenv.net;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.RateLimiting;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Otm.Services.RabbitMQ;
using OtmApi.Data;
using OtmApi.Services.Apis;
using OtmApi.Services.Discord;
using OtmApi.Services.HostService;
using OtmApi.Services.MapService;
using OtmApi.Services.OsuApi;
using OtmApi.Services.Players;
using OtmApi.Services.RabbitMQ;
using OtmApi.Services.RoundService;
using OtmApi.Services.ScheduleService;
using OtmApi.Services.StaffService;
using OtmApi.Services.TournamentService;

DotEnv.Load();
var builder = WebApplication.CreateBuilder(args);
builder.Services.AddScoped<IDiscordService, DiscordService>();
builder.Services.AddSingleton(new DiscordSocketClient());
builder.Services.AddCors();
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddAutoMapper(typeof(Program));
builder.Services.AddScoped<ITourneyService, TourneyService>();
builder.Services.AddScoped<IPlayerService, PlayerService>();
builder.Services.AddScoped<IOsuApiService, OsuApiService>();
builder.Services.AddScoped<IChallongeApiService, ChallongeApiService>();
builder.Services.AddScoped<IHostService, HostService>();
builder.Services.AddScoped<IRoundService, RoundService>();
builder.Services.AddScoped<IMapService, MapService>();
builder.Services.AddScoped<IScheduleService, ScheduleService>();
builder.Services.AddScoped<IStaffService, StaffService>();
builder.Services.AddSingleton<RabbitMQPublisher>();


builder.Services.AddRateLimiter(_ => _
    .AddFixedWindowLimiter(policyName: "fixed", options =>
    {
        options.PermitLimit = 20;
        options.Window = TimeSpan.FromSeconds(60);
    }));

builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Otm!", Version = "v1" });


    // Add JWT Authentication support in Swagger
    var securityScheme = new OpenApiSecurityScheme
    {
        Name = "JWT Authentication",
        Description = "Enter your JWT token",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.Http,
        Scheme = "bearer", // The name of the HTTP Authorization scheme to be used in the Swagger UI
        BearerFormat = "JWT", // JWT format
        Reference = new OpenApiReference
        {
            Type = ReferenceType.SecurityScheme,
            Id = "Bearer"
        }
    };
    c.AddSecurityDefinition("Bearer", securityScheme);
    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        { securityScheme, new List<string>() }
    });
});

var test = await FetchJwksAsync(Environment.GetEnvironmentVariable("JWKS_URI")!);

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    // Configure the token validation parameters
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidIssuer = "https://oth.eu.auth0.com/", // iss in token
        ValidAudience = "3pZ5jlt7hfCvxjxT06tSYtxRIVm8aZdj", // aud in token
        IssuerSigningKey = test[0], // singing key set (some/url/certs)
        ValidateIssuer = true, // Validate the token's issuer
        ValidateAudience = false, // Validate the token's audience
        ValidateLifetime = true, // Check if the token is expired
        ValidateIssuerSigningKey = true
    };
});
builder.Services.AddAuthorization();

builder.Services.AddDbContext<DataContext>(options =>
{
    var con = Environment.GetEnvironmentVariable("DB_STRING");
    if (con == null) System.Console.WriteLine("No connection string found");
    options.UseNpgsql(con, options => options.UseQuerySplittingBehavior(QuerySplittingBehavior.SplitQuery));
});


builder.Services.AddSingleton<RabbitMQService>(sp =>
{
    string hostname = Environment.GetEnvironmentVariable("RMQ_HOSTNAME") ?? throw new Exception("RabbitMQ hostName not in ENV");
    string userName = Environment.GetEnvironmentVariable("RMQ_USERNAME") ?? throw new Exception("RabbitMQ userName not in ENV");
    string password = Environment.GetEnvironmentVariable("RMQ_PASSWORD") ?? throw new Exception("RabbitMQ password not in ENV");

    return new RabbitMQService(hostname, userName, password);
});




var app = builder.Build();
var discordClient = app.Services.GetService<DiscordSocketClient>();
discordClient!.LoginAsync(TokenType.Bot, Environment.GetEnvironmentVariable("DISCORD_BOT_TOKEN")).GetAwaiter().GetResult();
discordClient.StartAsync().GetAwaiter().GetResult();


app.UseCors(builder =>
{
    builder.WithOrigins("http://localhost:5173", "https://osu-tm.vercel.app", "https://ot-timer.azurewebsites.net")
            .AllowAnyMethod()
            .AllowAnyHeader();
});

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
app.UseRateLimiter();
app.Services.GetRequiredService<RabbitMQService>();
if (app.Environment.IsDevelopment()) app.Run();
else app.Run("http://::80");



static async Task<SecurityKey[]> FetchJwksAsync(string jwksUri)
{
    using (var httpClient = new HttpClient())
    {
        var jwksJson = await httpClient.GetStringAsync(jwksUri);

        // Parse the JWKS JSON and build the SecurityKey array
        var jwks = JsonWebKeySet.Create(jwksJson);
        return jwks.Keys.ToArray();
    }
}