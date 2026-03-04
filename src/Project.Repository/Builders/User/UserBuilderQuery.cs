using System.Text;

namespace Project.Repository.Builders.User
{
    public partial class UserBuilder
    {
        public string Id { get; set; }
        public string Email { get; set; }
        public string CpfCnpj { get; set; }
        public string Password { get; set; }
        public string Salt { get; set; }

        public string CreateUserSql()
        {
            var sqlBuilder = new StringBuilder();
            sqlBuilder.Append($@"
                INSERT INTO ""user"" (
                    id,
                    email,
                    password,
                    salt,
                    cpfCnpj
                ) VALUES (
                    '{Id}',
                    '{Email}',
                    '{Password}',
                    '{Salt}',
                    '{CpfCnpj}'
                );
            ");

            return sqlBuilder.ToString();
        }

        public string GetUserByIdSql()
        {
            var sqlBuilder = new StringBuilder();
            sqlBuilder.Append($@"
                SELECT * FROM ""user"" WHERE id = '{Id}';
            ");

            return sqlBuilder.ToString();
        }

        public string GetUserByEmailSql()
        {
            var sqlBuilder = new StringBuilder();
            sqlBuilder.Append($@"
                SELECT * FROM ""user"" WHERE email = '{Email}';
            ");

            return sqlBuilder.ToString();
        }

        public string GetUserByCpfCnpjSql()
        {
            var sqlBuilder = new StringBuilder();
            sqlBuilder.Append($@"
                SELECT * FROM ""user"" WHERE cpfCnpj = '{CpfCnpj}';
            ");

            return sqlBuilder.ToString();
        }
    }
}
