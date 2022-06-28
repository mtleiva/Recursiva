using CsvHelper.Configuration.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Recursiva.Models
{
    class Socio
    {
        [Index(0)]
        public string Nombre { get; set; }
        [Index(1)]
        public int Edad { get; set; }
        [Index(2)]
        public string Equipo { get; set; }
        [Index(3)]
        public string EstadoCivil { get; set; }
        [Index(4)]
        public string NivelDeEstudios { get; set; }

        public Socio()
        {
        }

        public Socio(string nombre, int edad, string equipo, string estadoCivil, string nivelDeEstudios)
        {
            Nombre = nombre;
            Edad = edad;
            Equipo = equipo;
            EstadoCivil = estadoCivil;
            NivelDeEstudios = nivelDeEstudios;
        }
    }
}
