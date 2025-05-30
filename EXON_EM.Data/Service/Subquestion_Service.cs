using EXON_EM.Data.Interface;
using EXON_EM.Data.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EXON_EM.Data.Service
{
    public class Subquestion_Service
    {
          private EXON_DbContext db = new EXON_DbContext();
          public IEnumerable<SUBQUESTION> getAll()
          {
               return db.SUBQUESTIONS.ToList();
          }
          public SUBQUESTION Find(int id)
          {
               SUBQUESTION a = db.SUBQUESTIONS.Where(p => p.SubQuestionID == id).FirstOrDefault();
               return a;
          }
     }
}
