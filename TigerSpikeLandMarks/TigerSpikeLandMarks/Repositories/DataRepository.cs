using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TigerSpikeLandMarks.DBContexts;
using TigerSpikeLandMarks.Entities;

namespace TigerSpikeLandMarks.Repositories
{
    public class DataRepository : IDataRepository
    {
        
        public SQLDBContext _dbContext{ get; set; }
        public DataRepository(SQLDBContext dbContext)
        {
            _dbContext = dbContext;
        }

        #region Users
        public User GetUser(int id)
        {
            return _dbContext.Users.Find(id);
        }
        public User GetUser(string username, string password)
        {
            return _dbContext.Users.FirstOrDefault(u => u.Username.Equals(username) && u.PasswordHash.Equals(password));
        }
        public bool FindUser(string username)
        {
            return _dbContext.Users.Any(u => u.Username.Equals(username, StringComparison.OrdinalIgnoreCase));
        }
        public void AddUser(User user)
        {
            _dbContext.Users.Add(user);
            _dbContext.SaveChanges();
        }
        #endregion
        #region LandMarks
        public LandMarkNote GetLandMark(int id)
        {
            return _dbContext.LandMarks.Find(id);
        }
        public List<LandMarkNote> GetLandMarks(int userId)
        {
            return _dbContext.LandMarks.Where(lm => lm.User.Id == userId).ToList();
        }
        public List<LandMarkNote> GetAllLandMarks()
        {
            return _dbContext.LandMarks.Include("User").Where(lm => lm.User.Id != -1).ToList();
        }

        public List<LandMarkNote> searchLandMarks(string searchText)
        {
            return _dbContext.LandMarks.Include("User").
                    Where(lm => lm.UserNote.Contains(searchText) != false 
                    || lm.User.Username.Contains(searchText)).ToList();
        }


        public LandMarkNote GetLandMark(double latitude, double longitude)
        {
            return _dbContext.LandMarks.FirstOrDefault(lm => lm.Latitude == latitude && lm.Longitude == longitude);
        }
        public int AddLandMark(LandMarkNote landmark)
        {

            _dbContext.LandMarks.Add(landmark);
            _dbContext.SaveChanges();
            return landmark.Id;
        }
        public int UpdateLandMark(LandMarkNote landmark)
        {
            _dbContext.LandMarks.Update(landmark);
            return _dbContext.SaveChanges();
        }

        #endregion
    }
}
