using EjemploPersonasData.Services;
using EjemploInyeccion.Components;
using EjemploPersonasData.DALs.MSSDALs;
using Microsoft.Data.SqlClient;
using EjemploPersonasData.DALs;

#region contenedor
var builder = WebApplication.CreateBuilder(args);

builder.Services.AddRazorComponents().AddInteractiveServerComponents();

#region configuraciones
builder.Services.AddTransient<SqlConnection>(sp =>
{
    var connectionString = sp.GetService<IConfiguration>().GetConnectionString("ConnectionStrings");
    return new SqlConnection(connectionString);
});
#endregion

#region entidades
//persistencia
builder.Services.AddScoped<ITransaction<SqlTransaction>, SqlServerTransaction>();
builder.Services.AddScoped<PersonasMSSDAL>();

//servicios
builder.Services.AddScoped<PersonasService>();
#endregion

#region swagger
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddCors(options =>
{
    options.AddPolicy("PermitirTodo",
        policy =>
        {
            policy.AllowAnyOrigin() // Permitir cualquier origen
                  .AllowAnyMethod()  // Permitir cualquier método (GET, POST, etc.)
                  .AllowAnyHeader(); // Permitir cualquier encabezado
        });
});
#endregion

#endregion

#region aplicación
var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);    
    app.UseHsts();
}

//if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseStaticFiles();
app.UseAntiforgery();

app.MapRazorComponents<App>().AddInteractiveServerRenderMode();

app.Run();

#endregion