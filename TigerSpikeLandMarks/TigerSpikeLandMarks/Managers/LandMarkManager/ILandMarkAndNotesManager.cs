using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TigerSpikeLandMarks.DBContexts;
using TigerSpikeLandMarks.Entities;
using TigerSpikeLandMarks.Entities.DTOs;
using TigerSpikeLandMarks.Managers.UserManager;
using TigerSpikeLandMarks.Repositories;

namespace TigerSpikeLandMarks.Managers.LandMarkManager
{
    public interface ILandMarkAndNotesManager
    {
        LandMarkNote GetLandmarkById(int id);
        int CreateLandMark(double longitude, double latitude, int userId, string text);
        List<LandMarkNote> GetLandmarksOfUser(string userId);
        List<MarkerAndText> GetAllLandmarks(string userId);
        List<MarkerAndText> search(string searchText);
        bool UpdateLandMarkNote(MarkerAndText markerAndText);
    }

    public class LandMarAndkNotesManager : ILandMarkAndNotesManager
    {
        private SQLDBContext _dbContext;
        private IUserManager _userManager;
        private IDataRepository _detaRepository;
        public LandMarAndkNotesManager(SQLDBContext dbContext, IUserManager userService, IDataRepository dataRepository)
        {
            _dbContext = dbContext;
            _userManager = userService;
            _detaRepository = dataRepository;
        }

        public int CreateLandMark(double longitude, double latitude, int userId, string text)
        {
            try
            {
                User user = _detaRepository.GetUser(userId);
                LandMarkNote landmark = new LandMarkNote();
                landmark.Latitude = latitude;
                landmark.Longitude = longitude;
                landmark.User = user;
                landmark.UserNote = text;
                return _detaRepository.AddLandMark(landmark);
                
            }
            catch (Exception ex)
            {
                return -1;
            }
        }

        public List<MarkerAndText> GetAllLandmarks(string userId)
        {
            List<LandMarkNote> markers = _detaRepository.GetAllLandMarks();
            List<MarkerAndText> returnMarkers = new List<MarkerAndText>();
            foreach (LandMarkNote m in markers)
            {
                MarkerAndText markerAndText = new MarkerAndText();
                markerAndText.id = m.Id;
                markerAndText.latitude = m.Latitude;
                markerAndText.longitude = m.Longitude;
                markerAndText.text = m.UserNote;
                markerAndText.userId = m.User.Id;
                markerAndText.userName = m.User.Username;
                returnMarkers.Add(markerAndText);
            }
            return returnMarkers;
        }

        public LandMarkNote GetLandmarkById(int id)
        {
            return _detaRepository.GetLandMark(id);
        }

        public List<LandMarkNote> GetLandmarksOfUser(string userId)
        {
            throw new NotImplementedException();
        }

        public List<MarkerAndText> search(string searchText)
        {
            if (searchText == "all")
            {
                searchText = "";
            }
            List<LandMarkNote> markers = _detaRepository.searchLandMarks(searchText);
            List<MarkerAndText> returnMarkers = new List<MarkerAndText>();
            foreach (LandMarkNote m in markers)
            {
                MarkerAndText markerAndText = new MarkerAndText();
                markerAndText.id = m.Id;
                markerAndText.latitude = m.Latitude;
                markerAndText.longitude = m.Longitude;
                markerAndText.text = m.UserNote;
                markerAndText.userId = m.User.Id;
                markerAndText.userName = m.User.Username;
                returnMarkers.Add(markerAndText);
            }
            return returnMarkers;
        }

        public bool UpdateLandMarkNote(MarkerAndText markerAndText)
        {
            try
            {
                LandMarkNote landmark = _detaRepository.GetLandMark(markerAndText.id);
                landmark.Latitude = markerAndText.latitude;
                landmark.Longitude = markerAndText.longitude;
                landmark.UserNote = markerAndText.text;
                return _detaRepository.UpdateLandMark(landmark) > 0;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}
