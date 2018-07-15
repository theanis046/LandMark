using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TigerSpikeLandMarks.DBContexts;
using TigerSpikeLandMarks.Entities;

namespace TigerSpikeLandMarks.Repositories
{

       public interface IDataRepository
       {
        SQLDBContext _dbContext { get; set; }

        User GetUser(int id);
        User GetUser(string username, string password);
        bool FindUser(string username);
        void AddUser(User user);
        LandMarkNote GetLandMark(int id);
        List<LandMarkNote> GetLandMarks(int userId);
        List<LandMarkNote> GetAllLandMarks();
        List<LandMarkNote> searchLandMarks(string searchText);
        LandMarkNote GetLandMark(double latitude, double longitude);
        int AddLandMark(LandMarkNote landmark);
        int UpdateLandMark(LandMarkNote landmark);
        
    }
}
