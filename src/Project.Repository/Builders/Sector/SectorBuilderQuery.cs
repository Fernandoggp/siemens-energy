using System.Text;

namespace Project.Repository.Builders.Sector
{
    public partial class SectorBuilder
    {
        protected string FindAllSectorsSql()
        {
            var sqlBuilder = new StringBuilder();
            sqlBuilder.Append($@"
                SELECT 
                    sector.name,
                    sector.perennial
                FROM
                    sector;
            ");

            return sqlBuilder.ToString();
        }
    }
}