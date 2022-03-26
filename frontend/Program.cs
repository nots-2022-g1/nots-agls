using frontend.Services;
using frontend.ViewModels;
using frontend.ViewModels.Commits;
using frontend.ViewModels.DataSets;
using frontend.ViewModels.LabeledDatas;
using frontend.ViewModels.Repositories;
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


// -- Keywords
builder.Services.AddHttpClient<IKeywordService, KeywordService>();
builder.Services.AddSingleton<KeywordService>();
// -- Datasets
builder.Services.AddHttpClient<IDatasetService, DatasetService>();
builder.Services.AddSingleton<DatasetService>();
// -- Repositories
builder.Services.AddHttpClient<IRepositoryService, RepositoryService>();
builder.Services.AddSingleton<RepositoryService>();
builder.Services.AddScoped<IListRepositoriesViewModel, ListRepositoriesViewModel>();
builder.Services.AddScoped<ICreateRepositoryViewModel, CreateRepositoryViewModel>();
builder.Services.AddScoped<IViewRepositoryViewModel, ViewRepositoryViewModel>();
builder.Services.AddScoped<IEditRepositoryViewModel, EditRepositoryViewModel>();
// -- Commits
builder.Services.AddHttpClient<ICommitService, CommitService>();
builder.Services.AddScoped<IListCommitsViewModel, ListCommitsViewModel>();
// -- Datasets
builder.Services.AddHttpClient<IDatasetService, DatasetService>();
builder.Services.AddScoped<IListDatasetsViewModel, ListDatasetsViewModel>();
builder.Services.AddScoped<ICreateDatasetViewModel, CreateDatasetViewModel>();
builder.Services.AddScoped<IViewDatasetViewModel, ViewDatasetViewModel>();
builder.Services.AddScoped<IEditDatasetViewModel, EditDatasetViewModel>();
// -- LabeledDatas
builder.Services.AddHttpClient<ILabeledDataService, LabeledDataService>();
builder.Services.AddScoped<IListLabeledDataViewModel, ListLabeledDataViewModel>();

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