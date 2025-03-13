//----------------------------------------
// .Net Core WebApi project create script 
//           v9.2.0 from 2025-01-25
//   (C)Robert Grueneis/HTL Grieskirchen 
//----------------------------------------

using GrueneisR.RestClientGenerator;

using Microsoft.OpenApi.Models;

string corsKey = "_myCorsKey";
string swaggerVersion = "v1";
string swaggerTitle = "Musikverwaltung";
string restClientFolder = Environment.CurrentDirectory;
string restClientFilename = "_requests.http";

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddScoped<DbService>();

#region -------------------------------------------- ConfigureServices
builder.Services.AddControllers();
builder.Services
  .AddEndpointsApiExplorer()
  .AddAuthorization()
  .AddSwaggerGen(x => x.SwaggerDoc(
    swaggerVersion,
    new OpenApiInfo { Title = swaggerTitle, Version = swaggerVersion }
  ))
  .AddCors(options => options.AddPolicy(
    corsKey,
    x => x.SetIsOriginAllowed(_ => true).AllowAnyMethod().AllowAnyHeader().AllowCredentials()
  ))
  .AddRestClientGenerator(options => options
    .SetFolder(restClientFolder)
    .SetFilename(restClientFilename)
    .SetAction($"swagger/{swaggerVersion}/swagger.json")
  //.EnableLogging()
  );
builder.Services.AddLogging(x => x.AddCustomFormatter());

string? connectionString = builder.Configuration.GetConnectionString("Music");
string location = System.Reflection.Assembly.GetEntryAssembly()!.Location;
string dataDirectory = Path.GetDirectoryName(location)!;
connectionString = connectionString?.Replace("|DataDirectory|", dataDirectory + Path.DirectorySeparatorChar);
Console.ForegroundColor = ConsoleColor.Cyan;
Console.WriteLine($"++++ ConnectionString: {connectionString}");
Console.ResetColor();
builder.Services.AddDbContext<MusicContext>(options => options.UseSqlServer(connectionString));
#endregion

var app = builder.Build();


#region -------------------------------------------- Middleware pipeline
if (app.Environment.IsDevelopment())
{
  app.UseDeveloperExceptionPage();
  Console.ForegroundColor = ConsoleColor.Green;
  Console.WriteLine("++++ Swagger enabled: http://localhost:5000");
  app.UseSwagger();
  Console.WriteLine($@"++++ RestClient generating (after first request) to {restClientFolder}\{restClientFilename}");
  app.UseRestClientGenerator();
  app.UseSwaggerUI(x => x.SwaggerEndpoint($"/swagger/{swaggerVersion}/swagger.json", swaggerTitle));
  Console.ResetColor();
}

app.UseCors(corsKey);
//app.UseHttpsRedirection();
app.UseAuthorization();
#endregion

app.Map("/", () => Results.Redirect("/swagger"));


app.MapControllers();
Console.WriteLine($"Ready for clients at {DateTime.Now:HH:mm:ss} ...");
app.Run();
