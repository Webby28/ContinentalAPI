// Archivo: Models/Persona.cs
using System;
using System.ComponentModel.DataAnnotations;


namespace ContinentalAPI.Models
{
    public class Persona
    {
        public int Id { get; set; }
        [Required] // Campo obligatorio
        public string NombreApellido { get; set; } = string.Empty;
        [Required] // Campo obligatorio
        public string NroDocumento { get; set; } = string.Empty;
        [EmailAddress] // Campo con formato de correo electrónico
        public string CorreoElectronico { get; set; } = string.Empty;
        public string Telefono { get; set; } = string.Empty;
        [DataType(DataType.Date)] // Campo con formato de fecha
        public DateTime FechaNacimiento { get; set; }
    }
}