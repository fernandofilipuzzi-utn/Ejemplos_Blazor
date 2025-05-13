using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Components;
using System.ComponentModel.DataAnnotations;
using System.Security.Claims;

namespace AutentificacionAutorizacion.Components.Pages
{
    public partial class Login
    {

        LoginModel loginModel { get; set; } = new();

        string mensaje;
        bool mostrarMensaje { get; set; } = false;

        [Inject] private NavigationManager Navigation { get; set; }
        [Inject] private IHttpContextAccessor HttpContextAccessor { get; set; }

        async private Task OnLogin()
        {
            mostrarMensaje = false;

            loginModel.Usuario = "admin";
            loginModel.Clave = "123";

            if (loginModel.Usuario != "admin" && loginModel.Clave != "123")
            {
                mostrarMensaje = true;
                mensaje = "usuario o claves incorrectos";
                return;
            }

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, loginModel.Usuario)
            };

            var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            var principal = new ClaimsPrincipal(identity);

            var httpContext = HttpContextAccessor.HttpContext;
            if (httpContext != null)
            {
                //es necesario la using @using Microsoft.AspNetCore.Authentication
                await httpContext?.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);
                Navigation.NavigateTo("/");
            }
            else
            {
                mensaje = "No se pudo iniciar sesión debido a un error interno.";
            }
        }
    }

    public class LoginModel
    {
        //[Required(ErrorMessage = "Debe ingresar el nombre")]
        public string? Usuario { get; set; }

        //[Required(ErrorMessage = "Debe ingresar la clave")]
        public string? Clave { get; set; }
    }
}