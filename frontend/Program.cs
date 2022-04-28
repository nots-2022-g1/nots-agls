using BlazorDownloadFile;
using frontend.Services;
using frontend.ViewModels.Commits;
using frontend.ViewModels.Datasets;
using frontend.ViewModels.Repositories;
using frontend.ViewModels.KeywordSets;
using Serilog;

Log.Logger = new LoggerConfiguration()
    .WriteTo.Console()
    .CreateBootstrapLogger();

Log.Information("Starting up");

var builder = WebApplication.CreateBuilder(args);

builder.Host.UseSerilog((ctx, lc) => lc
    .WriteTo.Console());

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
builder.Services.AddBlazorDownloadFile();

// -- KeywordSets
builder.Services.AddHttpClient<IKeywordService, KeywordService>();
builder.Services.AddSingleton<KeywordService>();
builder.Services.AddTransient<IKeywordSetListViewModel, KeywordSetListViewModel>();
builder.Services.AddTransient<IKeywordSetDetailViewModel, KeywordSetDetailViewModel>();
builder.Services.AddTransient<IKeywordSetAddViewModel, KeywordSetAddViewModel>();
builder.Services.AddTransient<IKeywordSetCreateViewModel, KeywordSetCreateViewModel>();
builder.Services.AddTransient<IKeywordSetEditViewModel, KeywordSetEditViewModel>();
// -- Datasets
builder.Services.AddHttpClient<IDatasetService, DatasetService>();
builder.Services.AddSingleton<DatasetService>();
builder.Services.AddTransient<IEditDatasetViewModel, EditDatasetViewModel>();
// -- OpenAI
builder.Services.AddHttpClient<IOpenAIService, OpenAIService>();
builder.Services.AddSingleton<OpenAIService>();
// -- Repositories
builder.Services.AddHttpClient<IRepositoryService, RepositoryService>();
builder.Services.AddSingleton<RepositoryService>();
builder.Services.AddScoped<IListRepositoriesViewModel, ListRepositoriesViewModel>();
builder.Services.AddScoped<ICreateRepositoryViewModel, CreateRepositoryViewModel>();
builder.Services.AddScoped<IViewRepositoryViewModel, ViewRepositoryViewModel>();
builder.Services.AddScoped<IEditRepositoryViewModel, EditRepositoryViewModel>();
// -- Commits
builder.Services.AddHttpClient<ICommitService, CommitService>();
builder.Services.AddSingleton<CommitService>();
builder.Services.AddScoped<IListCommitsViewModel, ListCommitsViewModel>();
// -- Machine Learning
builder.Services.AddHttpClient<IMachineLearningService, MachineLearningService>();
builder.Services.AddSingleton<MachineLearningService>();

var app = builder.Build();

app.UseSerilogRequestLogging();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
}

app.UseStaticFiles();

app.UseRouting();

app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

app.Run();