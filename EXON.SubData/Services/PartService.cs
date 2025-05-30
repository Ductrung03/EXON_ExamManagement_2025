using EXON.SubData.Infrastructures;
using EXON.SubData.Repositories;
using EXON.SubModel.Models;
using System.Collections.Generic;
using System;

namespace EXON.SubData.Services
{

     public interface IPartService
     {

          IEnumerable<PART> GetAll();

          void Save();
     }

     public class PastService : IPartService
     {
          private IPartRepository _PartRepository;
          private IUnitOfWork _unitOfWork;
          private IDbFactory dbFactory;

          public PastService()
          {
               dbFactory = new DbFactory();
               this._PartRepository = new PartRepository(dbFactory);
               this._unitOfWork = new UnitOfWork(dbFactory);
          }


          public IEnumerable<PART> GetAll()
          {
               return _PartRepository.GetAll();
          }

          public void Save()
          {
               _unitOfWork.Commit();
          }

         
     }
}
