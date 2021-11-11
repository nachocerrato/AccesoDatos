using AccesoDatos.Models;
using AccesoDatos.Repositories;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;


namespace AccesoDatos
{
    class Program
    {
        static void Main(string[] args)
        {
            //EliminarDepartamentos();
            //LeerRegistros();
            //MostrarEmpleadosDpto();
            //ModificaSala();

            RepositoryDoctor repo = new RepositoryDoctor();
            //int resultado = repo.InsertarDoctor(19, 999, "Cerrato R.", "Futbología", 500000);
            //int resultado = repo.ModificarDoctor(120, 19, "Cerrato R.", "Futbología", 500000);
            //Console.WriteLine(resultado);
            //Doctor doctor = repo.BuscarDoctor()
            //List<Doctor> doctores = repo.getDoctores();
            //foreach (Doctor doctor in doctores)
            //{
            //    Console.WriteLine(doctor.Apellido + " - " + doctor.Especialidad
            //                                      + " - " + doctor.Salario);
            //}
            //int resultado = repo.incrementaSalario(22, 50);
            //Console.WriteLine(resultado);

            int result = repo.InsertarDoctor2("General", 111, "Cerrato Rsss.", "Futbologíasss", 111111);







            //RepositoryDepartamentos repo = new RepositoryDepartamentos();
            ////necesitamos recuperar un departamento
            //Departamento departamento = repo.BuscarDepartamento(10);
            //Console.WriteLine(departamento.Numero + " - " + departamento.Nombre + " - " + departamento.Localidad);


            //int resultado = repo.InsertarDepartamento(50, "NUEVO", "NUEVO");
            //int resultado = repo.ElimnarDepartamento(50);
            //int resultado = repo.ModificarDepartamento(30, "VENTAS2", "TABARNIA");
            //Console.WriteLine(resultado);
        }


        static void ModificaSala()
        {
            String cadenaconexion = @"Data Source=LOCALHOST\SQLEXPRESS;Initial Catalog=HOSPITAL;Persist Security Info=True;User ID=SA;Password=azure";
            SqlConnection cn = new SqlConnection(cadenaconexion);
            SqlCommand com = new SqlCommand();
            SqlDataReader reader;

            String sql = "SELECT distinct NOMBRE, SALA_COD FROM SALA";


            com.Connection = cn;
            com.CommandText = sql;
            com.CommandType = System.Data.CommandType.Text;

            cn.Open();
            reader = com.ExecuteReader();

            while (reader.Read())
            {
                String nombresala = reader["NOMBRE"].ToString();
                String sala_cod = reader["SALA_COD"].ToString();

                Console.WriteLine(nombresala + ", " + sala_cod);
            }

            //liberamos el lector
            reader.Close();

            //necesitamos hacer otra consutlta, y si tuviéramos parámetros habría que limpiar
            String sql2 = "UPDATE SALA SET NOMBRE=@NOMBRE "
                        + " WHERE SALA_COD=@SALACOD";
            com.CommandText = sql2;

            Console.WriteLine("Introduzca un número de sala: ");
            int numero = int.Parse(Console.ReadLine());
            Console.WriteLine("Introduzca un nuevo nombre: ");
            string nombre = Console.ReadLine();
            SqlParameter pamnumero = new SqlParameter("@SALACOD", numero);
            SqlParameter pamnombre = new SqlParameter("@NOMBRE", nombre);

            com.Parameters.Add(pamnumero);
            com.Parameters.Add(pamnombre);
            int modificados = com.ExecuteNonQuery();
            Console.WriteLine("Modificados: " + modificados);
        }

        static void MostrarEmpleadosDpto()
        {
            String cadenaconexion = @"Data Source=LOCALHOST\SQLEXPRESS;Initial Catalog=HOSPITAL;Persist Security Info=True;User ID=SA;Password=azure";
            SqlConnection cn = new SqlConnection(cadenaconexion);
            SqlCommand com = new SqlCommand();
            SqlDataReader reader;

            String sql = "SELECT APELLIDO, OFICIO, SALARIO FROM EMP WHERE DEPT_NO=@NUMERO";
            Console.WriteLine("Introduzca un número de departamento: ");
            int numero = int.Parse(Console.ReadLine());
            SqlParameter pamnumero = new SqlParameter("@NUMERO", numero);
            com.Connection = cn;
            com.CommandText = sql;
            com.CommandType = System.Data.CommandType.Text;


            com.Parameters.Add(pamnumero);

            cn.Open();


            reader = com.ExecuteReader();
            reader.Read();

            while (reader.Read())
            {
                String apellido = reader["APELLIDO"].ToString();
                String oficio = reader["OFICIO"].ToString();

                Console.WriteLine(apellido + ", " + oficio);
            }


            reader.Close();
            cn.Close();
            com.Parameters.Clear();

        }

        static void EliminarDepartamentos()
        {
            String cadenaconexion = @"Data Source=LOCALHOST\SQLEXPRESS;Initial Catalog=HOSPITAL;Persist Security Info=True;User ID=SA;Password=azure";
            SqlConnection cn = new SqlConnection(cadenaconexion);
            SqlCommand com = new SqlCommand();
            String sql = "DELETE FROM DEPT WHERE DEPT_NO=@NUMERO";
            Console.WriteLine("Introduzca un número de departamento: ");
            int numero = int.Parse(Console.ReadLine());
            SqlParameter pamnumero = new SqlParameter("@NUMERO", numero);
            com.Connection = cn;
            com.CommandText = sql;
            com.CommandType = System.Data.CommandType.Text;
            //incluimos los parámetros dentro del comando
            com.Parameters.Add(pamnumero);
            cn.Open();
            int eliminados = com.ExecuteNonQuery();
            //no importa el orden ahora
            cn.Close();
            //liberamos los parámetros del comando (IMPORTANTE)
            com.Parameters.Clear();
            Console.WriteLine("Eliminados: " + eliminados);
        }
        static void AccionRegistro()
        {
            String cadenaconexion = @"Data Source=LOCALHOST\SQLEXPRESS;Initial Catalog=HOSPITAL;Persist Security Info=True;User ID=SA;Password=azure";
            SqlConnection cn = new SqlConnection(cadenaconexion);
            SqlCommand com = new SqlCommand();
            String sql = "INSERT INTO DEPT VALUES (66,'lalala','GIJON')";
            com.Connection = cn;
            com.CommandText = sql;
            com.CommandType = System.Data.CommandType.Text;
            cn.Open();
            //las consultas de acción se ejecutan con ExecuteNonQuery()
            //y devuelven el número de registros que han sido afectados
            int insertados = com.ExecuteNonQuery();
            cn.Close();
            Console.WriteLine("Insertados: " + insertados);
        }
        static void LeerRegistros()
        {
            String cadenaconexion = @"Data Source=LOCALHOST\SQLEXPRESS;Initial Catalog=HOSPITAL;Persist Security Info=True;User ID=SA;Password=azure";
            //declaramos los objetos a utilizar
            SqlConnection cn = new SqlConnection(cadenaconexion);
            SqlCommand com = new SqlCommand();
            SqlDataReader reader;
            //creamos la consulta 
            String consulta = "SELECT * FROM EMP";
            //indicamos al comando tres propiedades
            //1. conexión a utilizar
            com.Connection = cn;
            //2. la consulta a realizar
            com.CommandText = consulta;
            //3. el tipo de consulta
            com.CommandType = System.Data.CommandType.Text;
            //entrar salir, abrimos conexión
            cn.Open();
            //ejecutamos la consulta
            //al ser consulta select, utilizamos el método ExecuteReader, que devuelve un lector
            reader = com.ExecuteReader();
            //el lector tiene un método Read() que devuelve Boolean y leerá los datos
            //cada vez que ejecutamos Read() lee una fila
            reader.Read();
            //para recuperar los datos se utiliza ["columna"]
            String nombre = reader["DNOMBRE"].ToString();
            String localidad = reader["LOC"].ToString();
            Console.WriteLine(nombre + " - " + localidad);
            reader.Close();
            cn.Close();
        }
    }
}
