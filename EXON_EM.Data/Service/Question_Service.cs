using EXON_EM.Data.Interface;
using EXON_EM.Data.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EXON_EM.Data.Service
{
     public class Question_Service
     {
          private EXON_DbContext db = new EXON_DbContext();
          public IEnumerable<QUESTION> getAll()
          {
               return db.QUESTIONS.ToList();
          }
          public QUESTION Find(int id)
          {
               QUESTION a = db.QUESTIONS.Where(p => p.QuestionID == id).FirstOrDefault();
               return a;
          }
     }
}
