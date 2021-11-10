using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlClient;
using AccesoDatos.Models;

namespace AccesoDatos.Repositories
{
    public class RepositoryDoctor
    {
        //lo que necesito para conectarme
        private SqlConnection cn;
        private SqlCommand com;
        private SqlDataReader reader;
        private String cadenaconexion;



        public RepositoryDoctor()
        {
            //instanciamos herramientas de la clase
            this.cadenaconexion = @"Data Source=LOCALHOST\SQLEXPRESS;Initial Catalog=HOSPITAL;Persist Security Info=True;User ID=SA;Password=azure";
            this.cn = new SqlConnection(cadenaconexion);
            this.com = new SqlCommand();

            this.com.Connection = this.cn; //conexion
            this.com.CommandType = System.Data.CommandType.Text; //tipo de consulta 
        }

        public int InsertarDoctor(int hospital_cod, int doctor_no, String apellido, String especialidad, int salario)
        {
            String sqlinsert = "INSERT INTO DOCTOR VALUES (@HOSPICOD, @DOCTORNO, @APELLIDO, @ESPECIALIDAD, @SALARIO)";

            SqlParameter pamhospicod = new SqlParameter("@HOSPICOD", hospital_cod);
            SqlParameter pamdoctorno = new SqlParameter("@DOCTORNO", doctor_no);
            SqlParameter pamapellido = new SqlParameter("@APELLIDO", apellido);
            SqlParameter pamespecialidad = new SqlParameter("@ESPECIALIDAD", especialidad);
            SqlParameter pamsalario = new SqlParameter("@SALARIO", salario);

            //añadimos los parámetros al command
            this.com.Parameters.Add(pamhospicod);
            this.com.Parameters.Add(pamdoctorno);
            this.com.Parameters.Add(pamapellido);
            this.com.Parameters.Add(pamespecialidad);
            this.com.Parameters.Add(pamsalario);


            this.com.CommandText = sqlinsert;
            this.cn.Open();

            int result = this.com.ExecuteNonQuery();
            this.com.Parameters.Clear();
            this.cn.Close();

            return result;
        }

        public int EliminarDoctor(int doctor_no)
        {
            String sqldelete = "DELETE FROM DOCTOR WHERE DOCTOR_NO = @DOCTORNO";

            SqlParameter pamdoctorno = new SqlParameter("@DOCTORNO", doctor_no);

            this.com.Parameters.Add(pamdoctorno);

            this.com.CommandText = sqldelete;
            this.cn.Open();

            int result = this.com.ExecuteNonQuery();
            this.com.Parameters.Clear();
            this.cn.Close();


            return result;
        }

        public int ModificarDoctor(int doctor_no, int hospital_cod, String apellido, String especialidad, int salario)
        {
            String sqlupdate = "UPDATE DOCTOR " +
                "SET HOSPITAL_COD = @HOSPICOD, " +
                "APELLIDO = @APELLIDO, " +
                "ESPECIALIDAD = @ESPECIALIDAD, " +
                "SALARIO = @SALARIO " +
                "WHERE DOCTOR_NO = @DOCTORNO";

            SqlParameter pamhospicod = new SqlParameter("@HOSPICOD", hospital_cod);
            SqlParameter pamdoctorno = new SqlParameter("@DOCTORNO", doctor_no);
            SqlParameter pamapellido = new SqlParameter("@APELLIDO", apellido);
            SqlParameter pamespecialidad = new SqlParameter("@ESPECIALIDAD", especialidad);
            SqlParameter pamsalario = new SqlParameter("@SALARIO", salario);

            //añadimos los parámetros al command
            this.com.Parameters.Add(pamhospicod);
            this.com.Parameters.Add(pamdoctorno);
            this.com.Parameters.Add(pamapellido);
            this.com.Parameters.Add(pamespecialidad);
            this.com.Parameters.Add(pamsalario);


            this.com.CommandText = sqlupdate;
            this.cn.Open();

            int result = this.com.ExecuteNonQuery();
            this.com.Parameters.Clear();
            this.cn.Close();

            return result;
        }

        public Doctor BuscarDoctor(int doctor_no)
        {
            String sqlselect = "SELECT * FROM DOCTOR WHERE DOCTOR_NO = @DOCTORNO";
            this.com.CommandText = sqlselect;
            SqlParameter pamdoctorno = new SqlParameter("@DOCTORNO", doctor_no);

            this.com.Parameters.Add(pamdoctorno);
            this.reader = this.com.ExecuteReader();
            this.cn.Open();
            this.reader.Read();

            Doctor doctor = new Doctor();
            doctor.Hospital_cod = int.Parse(this.reader["HOSPITAL_COD"].ToString());
            doctor.Doctor_no = int.Parse(this.reader["DOCTOR_NO"].ToString());
            doctor.Apellido = this.reader["APELLIDO"].ToString();
            doctor.Especialidad = this.reader["ESPECIALIDAD"].ToString();
            doctor.Salario = int.Parse(this.reader["SALARIO"].ToString());

            this.reader.Close();
            this.cn.Close();
            this.com.Parameters.Clear();

            return doctor;
        }

        public List<Doctor> getDoctores()
        {
            String sqlselect = "SELECT * FROM DOCTOR";
            this.com.CommandText = sqlselect;
            this.cn.Open();
            this.reader = this.com.ExecuteReader();

            //creamos la colección
            List<Doctor> doctores = new List<Doctor>();
            //ahora la recorremos
            while (this.reader.Read())
            {
                Doctor doctor = new Doctor();
                doctor.Hospital_cod = int.Parse(this.reader["HOSPITAL_COD"].ToString());
                doctor.Doctor_no = int.Parse(this.reader["DOCTOR_NO"].ToString());
                doctor.Apellido = this.reader["APELLIDO"].ToString();
                doctor.Especialidad = this.reader["ESPECIALIDAD"].ToString();
                doctor.Salario = int.Parse(this.reader["SALARIO"].ToString());

                doctores.Add(doctor);
            }
            this.reader.Close();
            this.cn.Close();


            return doctores;
        }

    }
}
