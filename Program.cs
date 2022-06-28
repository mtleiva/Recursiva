using CsvHelper;
using Recursiva.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Globalization;
using CsvHelper.Configuration;
using Recursiva.Services;

namespace Recursiva
{
    class Program
    {
        static void Main(string[] args)
        {
            

            SocioService ss = new SocioService();
            
            var esp1 = ss.TotalPersonasRegistradas();
            Console.WriteLine("Especificacion 1");
            Console.WriteLine(esp1.ToString());
            var esp2 = ss.PromedioEdadSocios();
            Console.WriteLine("Especificacion 2");
            Console.WriteLine(esp2.ToString());
            var esp3 = ss.SociosByEstadoCivilEducacionList("Casado", "Universitario", 100);
            Console.WriteLine("Especificacion 3");
            foreach (var socios in esp3)
            {
                Console.WriteLine(socios.Equipo.ToString()+" | " + socios.Edad.ToString()+" | " + socios.Nombre.ToString());
              //  Console.WriteLine();
            }

            var esp4 = ss.Top5NombresList();
            Console.WriteLine("Especificacion 4");
            foreach (var nombres in esp4)
            {
                Console.WriteLine(nombres);
            }

            var esp5 = ss.EdadInfoByEquipoList();
            Console.WriteLine("Especificacion 5");
            foreach (var equipo in esp5)
            {
                Console.WriteLine(equipo.Nombre + " | " + equipo.EdadPromedio.ToString() + " | " + equipo.EdadMin.ToString()+ " | "+ equipo.EdadMax.ToString());
                //  Console.WriteLine();
            }

        }

        /*
        public static List<string> ReadInCSV(string absolutePath)
        {
            List<string> result = new List<string>();
            string value;
            using (TextReader fileReader = File.OpenText(absolutePath))
            {
                var csv = new CsvReader((IParser)fileReader);
                csv.Configuration.HasHeaderRecord = false;
                while (csv.Read())
                {
                    for (int i = 0; csv.TryGetField<string>(i, out value); i++)
                    {
                        result.Add(value);
                    }
                }
            }
            return result;
        }

        */

    }
}
