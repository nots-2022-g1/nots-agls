using api.Models;
using api.Parser;
using api.Services;
using Mapster;
using Microsoft.EntityFrameworkCore;
using Serilog;

Log.Logger = new LoggerConfiguration()
    .WriteTo.Console()
    .CreateBootstrapLogger();

Log.Information("Starting up");

var builder = WebApplication.CreateBuilder(args);

builder.Host.UseSerilog((ctx, lc) =>
    lc
        .WriteTo.Console()
        .MinimumLevel.Override("Microsoft.EntityFrameworkCore.Database.Command", Serilog.Events.LogEventLevel.Warning)
);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<ApplicationContext>(options =>
{
    options.UseNpgsql(builder.Configuration.GetConnectionString("default"));
});

builder.Services.AddScoped<IGitRepoService, GitRepoService>();
builder.Services.AddScoped<IGitCommitService, GitCommitService>();
builder.Services.AddScoped(typeof(IGenericCrudService<>), typeof(GenericCrudService<>));
builder.Services.AddScoped<ILabeledDataService, LabeledDataService>();
builder.Services.AddScoped<IKeywordService, KeywordService>();
builder.Services.AddScoped<IGitService, GitService>();
builder.Services.AddScoped<GitLogParser>();
builder.Services.AddScoped<IMachineLearningService, MachineLearningService>();

TypeAdapterConfig.GlobalSettings.Default.NameMatchingStrategy(NameMatchingStrategy.Flexible);

TypeAdapterConfig<GitLogParserGitCommit, GitCommit>
    .NewConfig()
    .Map(dest => dest.Message, src => src.message)
    .Map(dest => dest.Date, src => DateTime.Parse(src.commit_date).ToUniversalTime())
    .Map(dest => dest.Hash, src => src.commit_hash);

var app = builder.Build();

app.UseSerilogRequestLogging();

var scope = app.Services.CreateScope();
var context = scope.ServiceProvider.GetRequiredService<ApplicationContext>();
if (context.Database.GetPendingMigrations().Any())
{
    context.Database.Migrate();
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();