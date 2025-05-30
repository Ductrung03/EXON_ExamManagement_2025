using EXON.SubData.Infrastructures;
using EXON.SubData.Repositories;
using EXON.SubModel.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EXON.Common;
namespace EXON.SubData.Services
{
    public interface IViolationService
    {
        VIOLATION Add(VIOLATION _VIOLATION);

        void Update(VIOLATION _VIOLATION);

        VIOLATION Delete(int id);

        IEnumerable<VIOLATION> GetAll();
        IEnumerable<VIOLATION> GetByConstestshiftID(string contestantCodeID);
        VIOLATION GetById(int id);

        void Save();
    }

    public class ViolationService : IViolationService
    {
        private ViolationRepository _ViolationRepository;
        private IUnitOfWork _unitOfWork;
        private IDbFactory dbFactory;

        public ViolationService()
        {
            dbFactory = new DbFactory();
            this._ViolationRepository = new ViolationRepository(dbFactory);
            this._unitOfWork = new UnitOfWork(dbFactory);
        }

        public VIOLATION Add(VIOLATION _VIOLATION)
        {
            return _ViolationRepository.Add(_VIOLATION);
        }

        public VIOLATION Delete(int id)
        {
            return _ViolationRepository.Delete(id);
        }

        public IEnumerable<VIOLATION> GetAll()
        {
            return _ViolationRepository.GetAll();
        }

        public VIOLATION GetById(int id)
        {
            return _ViolationRepository.GetSingleById(id);
        }

        public void Save()
        {
            _unitOfWork.Commit();
        }

        public void Update(VIOLATION _VIOLATION)
        {
            _ViolationRepository.Update(_VIOLATION);
        }
        
          public IEnumerable<VIOLATION> GetByConstestshiftID(string contestantCodeID)
          {
            List<int> a = new List<int>();
            List<VIOLATION> d = new List<VIOLATION>();
            var all = _ViolationRepository.GetAll().ToList();
            for (int i = 0; i < all.Count; i++)
            {
                try
                {
                    VIOLATION c;
                    UserLoginComputerDifferent b;
                    c = all[i];
                    b = UserHelper.FromJSONToObject3(all[i].Description);
                    if (b.ContestantCode == contestantCodeID)
                    {
                        a.Add(c.ViolationID);
                    }
                }
                catch { }
                
            }
            foreach (var i in a)
            {
                _ViolationRepository.GetSingleById(i);
                d.Add(_ViolationRepository.GetSingleById(i));
            }
            return d;
          }
     }
    }
