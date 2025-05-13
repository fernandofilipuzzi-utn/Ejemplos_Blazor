using AutentificacionAutorizacion.Components;
using Microsoft.AspNetCore.Authentication.Cookies;

var builder = WebApplication.CreateBuilder(args);

#region restapi y swagger
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
#endregion

#region cookie y session
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.Cookie.IsEssential = true;
        options.Cookie.Name = "Cookies";
        options.LoginPath = "/Login";
        options.Cookie.MaxAge = null;
        options.ReturnUrlParameter = "returnurl";
        options.Cookie.HttpOnly = true; 
        options.Cookie.SameSite = SameSiteMode.Lax;
    });
builder.Services.AddAuthorization();
builder.Services.AddCascadingAuthenticationState();
builder.Services.AddHttpContextAccessor();
#endregion

builder.Services.AddRazorComponents().AddInteractiveServerComponents();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

#region restapi y swagger
app.UseSwagger();
app.UseSwaggerUI();
app.MapControllers();
#endregion

#region login
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();
#endregion

app.UseAntiforgery();

app.MapRazorComponents<App>().AddInteractiveServerRenderMode();

app.Run();
