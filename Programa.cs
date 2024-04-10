using System;
using System.Collections.Generic;

namespace ConsoleApp
{
    public class Program
    {
        public static void Main(string[] args)
        {
            try
            {
                List<Empleado> empleados = new List<Empleado>();

                while (true)
                {
                    Console.WriteLine("Ingrese los datos del empleado (o escriba 'salir' para terminar):");
                    Console.Write("Nombre: ");
                    string nombre = Console.ReadLine();

                    if (nombre.ToLower() == "salir")
                    {
                        break;
                    }

                    Console.Write("Apellidos: ");
                    string apellidos = Console.ReadLine();

                    Console.Write("Sueldo Bruto (mayor a cero): ");
                    double sueldoBruto = ObtenerValorMayorACero();

                    Console.Write("Categoría del trabajador (A/B/C): ");
                    char categoria = ObtenerCategoria();

                    Empleado empleado = new Empleado(nombre, apellidos, sueldoBruto, categoria);
                    empleados.Add(empleado);
                }

                // Aquí puedes realizar otras operaciones con la lista de empleados,
                // como insertar los datos en una base de datos.

            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }
        }

        public static double ObtenerValorMayorACero()
        {
            while (true)
            {
                if (double.TryParse(Console.ReadLine(), out double valor) && valor > 0)
                {
                    return valor;
                }
                else
                {
                    Console.WriteLine("Debe ingresar un valor numérico mayor que cero. Inténtelo de nuevo.");
                }
            }
        }

        public static char ObtenerCategoria()
        {
            while (true)
            {
                string input = Console.ReadLine().Trim().ToUpper();
                if (input == "A" || input == "B" || input == "C")
                {
                    return input[0];
                }
                else
                {
                    Console.WriteLine("Por favor, ingrese A, B o C como categoría del trabajador.");
                }
            }
        }
    }

    public class Empleado
    {
        public string Nombre { get; set; }
        public string Apellidos { get; set; }
        public double SueldoBruto { get; set; }
        public char Categoria { get; set; }

        public Empleado(string nombre, string apellidos, double sueldoBruto, char categoria)
        {
            Nombre = nombre;
            Apellidos = apellidos;
            SueldoBruto = sueldoBruto;
            Categoria = categoria;
        }
    }
}


