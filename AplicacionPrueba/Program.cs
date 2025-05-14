using AplicacionPrueba.Components;

#region contenedor
var builder = WebApplication.CreateBuilder(args);

builder.Services.AddRazorComponents().AddInteractiveServerComponents();

#region
builder.Services.AddControllers();
#endregion

#region swagger
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

#region aplicacion
var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    app.UseHsts();
}

#region swagger
//if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
#endregion

app.UseRouting();
app.UseHttpsRedirection();

app.UseStaticFiles();
app.UseAntiforgery();

#region controladores
app.MapControllers();
#endregion

app.MapRazorComponents<App>().AddInteractiveServerRenderMode();

app.Run();
#endregion