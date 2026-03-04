using Dapper;
using Project.Repository.Core;
using System.Data;

namespace Project.Repository.Builders.Sector
{
    public partial class SectorBuilder : ISectorBuilder
    {
        public Request FindAllSectors()
        {
            var sql = FindAllSectorsSql();

            return new Request(sql,null);
        }
    }
}
