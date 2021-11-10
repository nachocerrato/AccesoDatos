using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlClient;
using AccesoDatos.Models;

namespace AccesoDatos.Repositories
{
    public class RepositoryDepartamentos
    {
        //qué objetos necesita la clase para acceder a datos?
        //CadenaConexion, Conection, Command, DataReader
        //La CLASE Program utilizará dichos objetos para INSERTAR?
        private SqlConnection cn;
        private SqlCommand com;
        private SqlDataReader reader;
        private String cadenaconexion;


        //estos objetos debo instanciarlos... cuándo?

        public RepositoryDepartamentos()
        {
            //instanciamos todas las herramientas de la clase
            this.cadenaconexion = @"Data Source=LOCALHOST\SQLEXPRESS;Initial Catalog=HOSPITAL;Persist Security Info=True;User ID=SA;Password=azure";
            this.cn = new SqlConnection(cadenaconexion);
            this.com = new SqlCommand();
            //solamente le diremos una vez al comando su conexión y tipo de consultas
            this.com.Connection = this.cn;
            this.com.CommandType = System.Data.CommandType.Text;
        }

        //creamos un método para insertar, ¿qué necesitamos que nos den?
        public int InsertarDepartamento(int num, String nom, String localidad)
        {
            String sqlinsert = "INSERT INTO DEPT VALUES (@NUMERO,  @NOMBRE, @LOCALIDAD)";
            SqlParameter pamnumero = new SqlParameter("@NUMERO", num);
            SqlParameter pamnombre = new SqlParameter("@NOMBRE", nom);
            SqlParameter pamnlocalidad = new SqlParameter("@LOCALIDAD", localidad);
            //añadimos parámetros al commmand
            this.com.Parameters.Add(pamnumero);
            this.com.Parameters.Add(pamnombre);
            this.com.Parameters.Add(pamnlocalidad);
            //indicamos al comando la consulta sql
            this.com.CommandText = sqlinsert;
            //abrimos conexión
            this.cn.Open();
            //ejecutamos la acción
            int result = this.com.ExecuteNonQuery();
            //cerramos
            this.cn.Close();
            this.com.Parameters.Clear();
            return result;
        }

        public int ElimnarDepartamento(int dept_no)
        {
            String sqldelete = "DELETE FROM DEPT WHERE DEPT_NO = @DEPT_NO";
            //indicamos su consulta al comando
            this.com.CommandText = sqldelete;
            //creamos el parámetro
            SqlParameter pamdeptno = new SqlParameter("@DEPT_NO", dept_no);
            this.com.Parameters.Add(pamdeptno);

            this.cn.Open();
            int result = this.com.ExecuteNonQuery();
            this.cn.Close();
            this.com.Parameters.Clear();
            return result;
        }

        public int ModificarDepartamento(int dept_no, String nombre, String localidad)
        {
            String sqlupdate = "UPDATE DEPT SET DNOMBRE = @DNOMBRE, LOC = @LOCALIDAD WHERE DEPT_NO = @DEPTNO";
            this.com.CommandText = sqlupdate;
            SqlParameter pamnum = new SqlParameter("@dDEPT_NO", dept_no);
            SqlParameter pamnom = new SqlParameter("@NOMBRE", nombre);
            SqlParameter pamloc = new SqlParameter("@LOCALIDAD", localidad);

            this.com.Parameters.Add(pamnum);
            this.com.Parameters.Add(pamnom);
            this.com.Parameters.Add(pamloc);

            this.cn.Open();
            int result = this.com.ExecuteNonQuery();
            this.cn.Close();
            this.com.Parameters.Clear();
            return result;

        }

        public Departamento BuscarDepartamento(int id)
        {
            String sqlselect = "SELECT * FROM DEPT WHERE DEPT_NO=@DEPTNO";
            this.com.CommandText = sqlselect;
            SqlParameter pamnum = new SqlParameter("@DEPTNO", id);
            this.com.Parameters.Add(pamnum);
            this.cn.Open();
            //ejecutamos la consulta select con el reader
            this.reader = this.com.ExecuteReader();
            //solo existe un registro, leemos
            this.reader.Read();
            //creamos un departamento para devolver los datos
            Departamento departamento = new Departamento();
            //guardamos los datos del reader en nuestro departamento
            departamento.Numero = int.Parse(this.reader["DEPT_NO"].ToString());
            departamento.Nombre = this.reader["DNOMBRE"].ToString();
            departamento.Localidad = this.reader["LOC"].ToString();
            //liberamos el lector
            this.reader.Close();
            this.cn.Close();
            this.com.Parameters.Clear();

            return departamento;
        }

        public List<Departamento> GetDepartamentos()
        {
            String sql = "SELECT * FROM DEPT";
            this.com.CommandText = sql;
            this.cn.Open();
            this.reader = this.com.ExecuteReader();
            //extraemos múltiples datos
            //dichos datos necesitamos almacenarlos en una colección
            //instanciamos una colección

            List<Departamento> departamentos = new List<Departamento>();

            while (this.reader.Read())
            {
                //creamos un objeto por cada fila de Reader
                Departamento departamento = new Departamento();
                //asignamos valores
                departamento.Nombre = this.reader["DNOMBRE"].ToString();
                departamento.Numero = int.Parse(this.reader["DEPT_NO"].ToString());
                departamento.Localidad = this.reader["LOC"].ToString();

                departamentos.Add(departamento);

            }
            this.reader.Close();
            this.cn.Close();

            return departamentos;
        }
    }
}
