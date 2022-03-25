using frontend.Services;
using frontend.ViewModels.Commits;
using frontend.ViewModels.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
builder.Services.AddHttpClient<IRepositoryService, RepositoryService>();
builder.Services.AddHttpClient<ICommitService, CommitService>();
// -- Repositories
builder.Services.AddScoped<IListRepositoriesViewModel, ListRepositoriesViewModel>();
builder.Services.AddScoped<ICreateRepositoryViewModel, CreateRepositoryViewModel>();
builder.Services.AddScoped<IViewRepositoryViewModel, ViewRepositoryViewModel>();
builder.Services.AddScoped<IEditRepositoryViewModel, EditRepositoryViewModel>();
// -- Commits
builder.Services.AddScoped<IListCommitsViewModel, ListCommitsViewModel>();

var app = builder.Build();

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
