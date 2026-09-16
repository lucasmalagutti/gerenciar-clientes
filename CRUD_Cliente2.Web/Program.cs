using CRUD_Cliente2.Web.Data;
using CRUD_Cliente2.Web.Facade;
using CRUD_Cliente2.Web.Strategy;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

if (builder.Environment.IsDevelopment())
{
    builder.WebHost.UseUrls("https://localhost:7123", "http://localhost:5123");
}

builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<AppDbContext>(options =>
{
    options.UseInMemoryDatabase("AppDb");
});
builder.Services.AddScoped<IEnderecoDAO, EnderecoDAO>();
builder.Services.AddScoped<IClienteDAO, ClienteDAO>();

builder.Services.AddScoped<CadastrarClienteStrategy>();
builder.Services.AddScoped<EditarClienteStrategy>();
builder.Services.AddScoped<InativarClienteStrategy>();
builder.Services.AddScoped<AlterarSenhaStrategy>();
builder.Services.AddScoped<ConsultarClienteStrategy>();
builder.Services.AddScoped<ICriptografarSenhaStrategy, CriptografarSenhaStrategy>();
builder.Services.AddScoped<IPopularDropdownsStrategy, PopularDropdownsStrategy>();
builder.Services.AddScoped<IAdicionarEnderecoStrategy, AdicionarEnderecoStrategy>();
builder.Services.AddScoped<IAdicionarCartaoStrategy, AdicionarCartaoStrategy>();

builder.Services.AddScoped<ClienteFacade>();

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");


using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var context = services.GetRequiredService<AppDbContext>();
    DbInitializer.Inicializar(context);
}

app.Run();