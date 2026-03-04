using System.Text;

namespace Project.Repository.Builders.Portfolio
{
    public partial class PortfolioBuilder
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public float Value { get; set; }
        public int DesiredPercentage { get; set; }
        public string UserId { get; set; }

        public string CreateAssetSql()
        {
            var sqlBuilder = new StringBuilder();
            sqlBuilder.Append($@"
                INSERT INTO ""asset"" (
                    id,
                    name,
                    value,
                    desired_percentage,
                    user_id
                ) VALUES (
                    '{Id}',
                    '{Name}',
                    '{Value}',
                    '{DesiredPercentage}',
                    '{UserId}'
                );
            ");

            return sqlBuilder.ToString();
        }

        public string GetAssetByUserIdSql()
        {
            var sqlBuilder = new StringBuilder();
            sqlBuilder.Append($@"
                SELECT * FROM ""asset"" WHERE user_id = '{UserId}';
            ");

            return sqlBuilder.ToString();
        }

        public string UpdateAssetSql()
        {
            var sqlBuilder = new StringBuilder();
            sqlBuilder.Append($@"
                UPDATE asset
                SET 
                    name = '{Name}',
                    value = {Value},
                    desired_percentage = {DesiredPercentage}
                WHERE id = '{Id}';
            ");

            return sqlBuilder.ToString();
        }

        public string GetAssetByIdSql()
        {
            var sqlBuilder = new StringBuilder();
            sqlBuilder.Append($@"
                SELECT * FROM ""asset"" WHERE id = '{Id}';
            ");

            return sqlBuilder.ToString();
        }

        public string DeleteAssetByIdSql()
        {
            var sqlBuilder = new StringBuilder();
            sqlBuilder.Append($@"
                DELETE FROM ""asset"" WHERE id = '{Id}';
            ");

            return sqlBuilder.ToString();
        }

    }
}
