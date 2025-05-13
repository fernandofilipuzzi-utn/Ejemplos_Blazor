using System.ComponentModel.DataAnnotations;

namespace EjemploFormulario.Components.Pages;

public partial class Personas
{
    List<PersonaViewModel> personasModels = new();

    PersonaViewModel personaModel=new();
  
    protected override Task OnInitializedAsync()
    {

        //consulta
        personasModels.AddRange(new []{ 
            new PersonaViewModel{ Nombre="Juan", DNI="23432432"},
            new PersonaViewModel{ Nombre="Nélida", DNI="33432432"},
            new PersonaViewModel{ Nombre="Celeste", DNI="43432432"}
        });

        return base.OnInitializedAsync();
    }

    protected override Task OnAfterRenderAsync(bool firstRender)
    {
        return base.OnAfterRenderAsync(firstRender);
    }

    private void OnCrearPersona()
    {
        personasModels.Add(personaModel);
        personaModel = new PersonaViewModel();
    }
}

public class PersonaViewModel
{
    [Required(ErrorMessage ="Debe ingresar el nombre")]
    public string Nombre { get; set; }

    [Required(ErrorMessage = "Escribí una breve descripción de tu actividad.")]
    public string DNI { get; set; }
}