using EjemploPersonasData.Models;
using EjemploPersonasData.Services;
using Microsoft.AspNetCore.Components;

namespace EjemploInyeccion.Components.Pages
{
    public partial class Personas
    {
        List<PersonaModel?>? personasModels = null;

        //desde local
        //[Inject] PersonasService _PersonasService{get;set;}

        async protected override Task OnInitializedAsync()
        {
            personasModels = await _PersonasService.GetAll();

            await base.OnInitializedAsync();
        }
    }
}