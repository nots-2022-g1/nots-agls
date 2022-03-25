using System.Globalization;
using api.Model;
using api.Services;
using Mapster;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

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
builder.Services.AddScoped<ILabelService, LabelService>();
builder.Services.AddScoped<IGitCommitService, GitCommitService>();
builder.Services.AddScoped<IGenericCrudService<DataSet>, GenericCrudService<DataSet>>();
builder.Services.AddScoped<ILabeledDataService, LabeledDataService>();

TypeAdapterConfig<GitLogParserGitCommit, GitCommit>
    .NewConfig()
    .Map(dest => dest.Message, src => src.message)
    .Map(dest => dest.Date, src => DateTime.Parse(src.commit_date).ToUniversalTime())
    .Map(dest => dest.Hash, src => src.commit_hash);

var app = builder.Build();

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
