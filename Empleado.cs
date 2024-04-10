using System;
using System.Collections.Generic;
using System.Data.SqlClient;

public class Empleado
{
    // Propiedades
    public int Id { get; set; }
    public string Nombre { get; set; }
    public string Apellidos { get; set; }
    public double SueldoBruto { get; set; }
    public char Categoria { get; set; }
    public double Aumento { get; set; }
    public double SueldoNeto { get; set; }

    // Constructor
    public Empleado(string nombre, string apellidos, double sueldoBruto, char categoria)
    {
        Nombre = nombre;
        Apellidos = apellidos;
        SueldoBruto = sueldoBruto;
        Categoria = categoria;
    }

    // Métodos
    public void CalcularAumento()
    {
        switch (Categoria)
        {
            case 'A':
                Aumento = SueldoBruto * 0.1;
                break;
            case 'B':
                Aumento = SueldoBruto * 0.2;
                break;
            case 'C':
                Aumento = SueldoBruto * 0.3;
                break;
            default:
                Console.WriteLine("Categoría inválida. Aumento establecido en 0.");
                break;
        }
    }

    public void CalcularSueldoNeto()
    {
        SueldoNeto = SueldoBruto + Aumento;
    }

    public void Guardar(string connectionString)
    {
        try
        {
            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();

                var cmd = new SqlCommand("INSERT INTO Empleados (Nombre, Apellidos, SueldoBruto, Categoria, Aumento, SueldoNeto) VALUES (@Nombre, @Apellidos, @SueldoBruto, @Categoria, @Aumento, @SueldoNeto)", connection);
                cmd.Parameters.AddWithValue("@Nombre", Nombre);
                cmd.Parameters.AddWithValue("@Apellidos", Apellidos);
                cmd.Parameters.AddWithValue("@SueldoBruto", SueldoBruto);
                cmd.Parameters.AddWithValue("@Categoria", Categoria);
                cmd.Parameters.AddWithValue("@Aumento", Aumento);
                cmd.Parameters.AddWithValue("@SueldoNeto", SueldoNeto);

                cmd.ExecuteNonQuery();
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("Error al guardar el empleado en la base de datos: " + ex.Message);
        }
    }

    public static List<Empleado> ObtenerTodos(string connectionString)
    {
        var empleados = new List<Empleado>();

        try
        {
            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();

                var cmd = new SqlCommand("SELECT * FROM Empleados", connection);
                var reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    empleados.Add(new Empleado(reader.GetString(1), reader.GetString(2), reader.GetDouble(3), reader.GetChar(4))
                    {
                        Id = reader.GetInt32(0),
                        Aumento = reader.GetDouble(5),
                        SueldoNeto = reader.GetDouble(6)
                    });
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("Error al obtener los empleados de la base de datos: " + ex.Message);
        }

        return empleados;
    }
}





