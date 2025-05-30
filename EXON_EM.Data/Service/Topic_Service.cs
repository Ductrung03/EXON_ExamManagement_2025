using EXON_EM.Data.Interface;
using EXON_EM.Data.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EXON_EM.Data.Service
{
     public class Topic_Service
     {
          private EXON_DbContext db = new EXON_DbContext();
          public IEnumerable<TOPIC> getAll()
          {
               return db.TOPICS.ToList();
          }
          public TOPIC Find(int id)
          {
               TOPIC a = db.TOPICS.Where(p => p.TopicID == id).FirstOrDefault();
               return a;
          }
     }
}
