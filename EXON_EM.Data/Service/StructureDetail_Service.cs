using EXON_EM.Data.Interface;
using EXON_EM.Data.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EXON_EM.Data.Service
{
     public class StructureDetail_Service
     {
          private EXON_DbContext db = new EXON_DbContext();
          public IEnumerable<STRUCTURE_DETAILS> getAll()
          {
               return db.STRUCTURE_DETAILS.ToList();
          }
          public STRUCTURE_DETAILS Find(int id)
          {
               STRUCTURE_DETAILS a = db.STRUCTURE_DETAILS.Where(p => p.TopicID == id).FirstOrDefault();
               return a;
          }
          public STRUCTURE_DETAILS Find(int topicid, int level,int scheduleid)
          {
               SCHEDULE b = db.SCHEDULES.Where(p => p.ScheduleID == scheduleid).FirstOrDefault();
               STRUCTURE_DETAILS a = db.STRUCTURE_DETAILS.Where(p => p.TopicID == topicid && p.Level == level && p.STRUCTURE.ScheduleID == scheduleid).FirstOrDefault();
               return a;
          }

     }

}
