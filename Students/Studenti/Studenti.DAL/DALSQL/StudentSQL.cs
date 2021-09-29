using Studenti.DAL.Interfaces;
using Studenti.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.Data;

namespace Studenti.DAL.DALSQL
{
    public class StudentSQL : BaseDALSQL, IStudentDAL
    {
        public void Add(Student student)
        {
            SqlConnection connection = BaseDALSQL.GetConnection();

            using (connection)
            {
                SqlCommand command = new SqlCommand("INSERT INTO Students(FirstName,LastName,Address,Sex,DateofBirth,Description,StudentGroupId) VALUES(@FirstName,@LastName,@Address,@Sex,@DateofBirth,@Description,@StudentGroupId)", connection);


                connection.Open();

                // command.Parameters.AddWithValue("@Id", student.Id);
                command.Parameters.AddWithValue("@FirstName", student.FirstName);
                command.Parameters.AddWithValue("@LastName", student.LastName);
                command.Parameters.AddWithValue("@Address", student.Address);
                
                if (student.Sex == "M")
                {
                    command.Parameters.AddWithValue("@Sex", true);
                }
                else
                {
                    command.Parameters.AddWithValue("@Sex", false);
                }

                command.Parameters.AddWithValue("@DateofBirth", student.DateofBirth);
                command.Parameters.AddWithValue("@Description", student.Description);
                command.Parameters.AddWithValue("@StudentGroupId", student.StudentGroupId);



                command.ExecuteNonQuery();
                connection.Close();

            }
        }

        public void Delete(int studentId)
        {
            SqlConnection connection = BaseDALSQL.GetConnection();

            using (connection)
            {
                SqlCommand command = new SqlCommand("DELETE FROM Students WHERE Id = @studentId", connection); 
                command.Parameters.Add("@studentId", SqlDbType.Int).Value = studentId;
            
                connection.Open();
                command.ExecuteNonQuery();
                connection.Close();
            }
        }


        public List<Student> GetStudents()
        {
            List<Student> students = new List<Student>();

        
    
            SqlConnection connection = BaseDALSQL.GetConnection();

            using (connection)
            {
                SqlCommand command = new SqlCommand("SELECT * FROM Students;", connection);

                connection.Open();

                SqlDataReader reader = command.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {

                        students.Add(new Student
                        {
                            Id = (int)reader["id"],
                            FirstName = (string)reader["FirstName"],
                            LastName = (string)reader["LastName"],
                            Sex = reader["Sex"].ToString(),
                            Address = (string)reader["Address"],
                            DateofBirth = (DateTime)reader["DateofBirth"],
                            Description = (string)reader["Description"],
                            StudentGroupId=(int)reader["StudentGroupId"]
                                                                                   
                        });

                      
                    }
                }

                reader.Close();
            }
            return students;
        }



        public void Update(Student student)
        {
            SqlConnection connection = BaseDALSQL.GetConnection();

            using (connection)
            {
                SqlCommand command = new SqlCommand("UPDATE Students SET FirstName = @FirstName,LastName=@LastName,Address=@Address,Sex=@Sex,DateofBirth=@DateofBirth,Description=@Description,StudentGroupId=@StudentGroupId WHERE Id=@studentId;", connection);
                connection.Open();

                command.Parameters.AddWithValue("@studentId", student.Id);
                command.Parameters.AddWithValue("@FirstName", student.FirstName);
                command.Parameters.AddWithValue("@LastName", student.LastName);
                command.Parameters.AddWithValue("@Address", student.Address);
                if (student.Sex == "M")
                {
                    command.Parameters.AddWithValue("@Sex", true);
                }

                else
                {
                    command.Parameters.AddWithValue("@Sex", false);
                }

                command.Parameters.AddWithValue("@DateofBirth", student.DateofBirth);
                command.Parameters.AddWithValue("@Description", student.Description);
                command.Parameters.AddWithValue("@StudentGroupId", student.StudentGroupId);



                command.ExecuteNonQuery();
                
                connection.Close(); 
            }

        }


    }
}

