using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Globalization;
using CsvHelper.Configuration;
using System.IO;
using Recursiva.Models;
using CsvHelper;

namespace Recursiva.Services
{
    class SocioService
    {
        public SocioService()
        {
        }

        private static List<Socio> socios = null;

        public static List<Socio> Socios
        {
            get
            {
                if (socios == null)
                    socios = GetSocios();

                return socios;

            }

        }

        private static List<Socio> GetSocios()
        {

            using (var reader = new StreamReader(@"..\..\Resources\socios.csv"))
            {
                var csvConfig = new CsvConfiguration(CultureInfo.InvariantCulture)
                {
                    Delimiter = ";",
                    HasHeaderRecord = false

                };

                using (var csv = new CsvReader(reader, csvConfig))
                {
                    var records = csv.GetRecords<Socio>().ToList();
                    return records;
                }
            }

        }

        /// <summary>
        /// Especificacion 1:
        /// Cantidad total de personas registradas.
        /// </summary>
        /// <returns></returns>
        public int TotalPersonasRegistradas()
        {
            return Socios.Count;
        }
        /// <summary>
        /// Especificacion 2
        /// El promedio de edad de los socios de Racing.
        /// </summary>
        /// <returns></returns>
        public double PromedioEdadSocios()
        {
            return Socios.Average(socio => socio.Edad);
        }
        /// <summary>
        /// Especificacion 3
        /// Listado con las 100 primeras personas casadas, con estudios
        /// Universitarios, ordenadas de menor a mayor según su edad.
        /// Por cada persona, muestra: nombre, edad y equipo.
        /// </summary>
        /// <returns></returns>
        public List<SocioAux> SociosByEstadoCivilEducacionList(string estadocivil, string nivelDeEstudios, int size) {

            var query = Socios.Where(socio =>
                        socio.EstadoCivil == estadocivil &&
                        socio.NivelDeEstudios == nivelDeEstudios)
                      .OrderBy(socio => socio.Edad)
                      .AsQueryable();
            query = query.Take(size);
            return query.Select(socio => new SocioAux(socio.Nombre, socio.Edad, socio.Equipo)).ToList();

        }
        /// <summary>
        /// Especificacion 4
        /// Un listado con los 5 nombres más comunes entre los hinchas de River
        /// </summary>
        /// <returns></returns>
        public List<string> Top5NombresList()
        {
            //var socios = Socios.GroupBy(socio => socio.Nombre)
            List<string> nombres = new List<string>();

            foreach (var line in Socios.GroupBy(socio => socio.Nombre)
                        .Select(group => new
                        {
                            Nombre = group.Key
                        })
                        .OrderByDescending(x => x.Nombre)) {
                nombres.Add((line.Nombre));
            }
            return nombres.Take(5).ToList();

        }
        /// <summary>
        /// Especificacion 5
        /// Un listado, ordenado de mayor a menor según la cantidad de
        //  socios, que enumere, junto con cada equipo, el promedio de edad
        //  de sus socios, la menor edad registrada y la mayor edad registrada.
        /// </summary>
        /// <returns></returns>
        public List<EquipoAux> EdadInfoByEquipoList(){

            var equipos = Socios
                .GroupBy(socio => socio.Equipo)
                .Select(group => new EquipoAuxExtend(
                        group.First().Equipo, 
                        group.Average(sd => sd.Edad), 
                        group.Min(sd => sd.Edad), 
                        group.Max(equipo => equipo.Edad), 
                        group.Count()
                        )
                )
                .OrderByDescending(equipo => equipo.TotalSocios);
            return equipos.Select(equipo => (EquipoAux) equipo).ToList();
        }


        public class EquipoAuxExtend : EquipoAux
        {
            public int TotalSocios { get; set; }

            public EquipoAuxExtend(string nombre, double edadPromedio, int edadMin, int edadMax, int totalSocios) : base(nombre, edadPromedio, edadMin, edadMax)
            {
                TotalSocios = totalSocios;
            }
        }
        public class EquipoAux
        {
            public string Nombre { get; set; }
            public double EdadPromedio { get; set; }
            public int EdadMin { get; set; }
            public int EdadMax { get; set; }

            public EquipoAux(string nombre, double edadPromedio, int edadMin, int edadMax)
            {
                Nombre = nombre;
                EdadPromedio = edadPromedio;
                EdadMin = edadMin;
                EdadMax = edadMax;
            }
        }
        public class SocioAux {
            public string Nombre { get; set; }
            public int Edad { get; set; }
            public string Equipo { get; set; }

            public SocioAux(string nombre, int edad, string equipo)
            {
                Nombre = nombre;
                Edad = edad;
                Equipo = equipo;
            }
        }
    }
            
}
