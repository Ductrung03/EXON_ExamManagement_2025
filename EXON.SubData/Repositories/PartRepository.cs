using EXON.SubData.Infrastructures;
using EXON.SubModel.Models;

namespace EXON.SubData.Repositories
{
     public interface IPartRepository : IRepository<PART>

     {
     }

     public class PartRepository : RepositoryBase<PART>, IPartRepository
     {
          public PartRepository(IDbFactory dbFactory) : base(dbFactory)
          {
          }
     }

}