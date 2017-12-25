using InvoiceNet.Data.Entities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InvoiceNet.Data.Managers
{
    public class UserManager
    {
        private SqlConnection m_connection;

        public GenericUser GetUser(String userName)
        {
            GenericUser user = null;
            m_connection.Open();

            SqlCommand command = m_connection.CreateCommand();
            command.CommandText = "SELECT * FROM users WHERE user_name=@UserName";
            IDbDataParameter parameter = command.CreateParameter();
            parameter.ParameterName = "@UserName";
            parameter.Value = userName;
            command.Parameters.Add(parameter);
            SqlDataReader dataReader = command.ExecuteReader();

            if (dataReader.Read())
            {
                if (dataReader.GetString(dataReader.GetOrdinal("user_name")).Equals(userName))
                {
                    UInt32 uid = dataReader.GetUInt32(dataReader.GetOrdinal("user_id"));
                    user = new GenericUser(uid, userName);
                }
            }

            dataReader.Close();

            return user;
        }

        public UserManager(SqlConnection connection)
        {
            m_connection = connection;
        }
    }
}
