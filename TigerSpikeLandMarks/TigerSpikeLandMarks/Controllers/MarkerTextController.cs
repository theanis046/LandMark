using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Security.Claims;
using TigerSpikeLandMarks.Entities;
using TigerSpikeLandMarks.Entities.DTOs;
using TigerSpikeLandMarks.Managers.LandMarkManager;

namespace TigerSpikeLandMarks.Controllers
{

    public class MarkerTextController : Controller
    {
        private string user;
        private readonly ILandMarkAndNotesManager _landMarkAndNotesManager;
        IHttpContextAccessor _httpContextAccessor;
        public MarkerTextController(
            ILandMarkAndNotesManager landMarkAndNotesManager,
            IHttpContextAccessor httpContextAccessor)
        {
            _landMarkAndNotesManager = landMarkAndNotesManager;
            _httpContextAccessor = httpContextAccessor;
            
        }

        [Authorize]
        [HttpGet("api/getalllandmarks")]
        public IActionResult GetAllLandMarks()
        {
            user = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.Name).Value;
            var markers = _landMarkAndNotesManager.GetAllLandmarks(user.ToString());
            return Ok(markers);
        }


        [Authorize]
        [HttpGet("api/search/{searchText}")]
        public IActionResult search([FromRoute]string searchText)
        {
            user = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.Name).Value;
            var markers = _landMarkAndNotesManager.search(searchText);
            return Ok(markers);
        }

        // GET api/values/5
        [HttpGet]
        public List<LandMarkNote> GetLandMarksOfUser()
        {
            user = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.Name).Value;
            return _landMarkAndNotesManager.GetLandmarksOfUser(user.ToString());
        }

        // POST api/values
        [Authorize]
        [HttpPost("api/createMarkerAndText")]
        public IActionResult CreateLandMark([FromBody]MarkerAndText landMark)
        {
            user = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.Name).Value;
            var obj = landMark;//JsonConvert.DeserializeObject<LandMark>(landMark.ToString());
            if (obj != null)
            {
                int result = _landMarkAndNotesManager.CreateLandMark(obj.longitude, obj.latitude, landMark.userId,landMark.text);
                return Ok(new { status = result });
            }
            return Ok(new { status = "false" });
        }

        [Authorize]
        [HttpPut("api/updatelandmark")]
        public bool UpdateLandMarkNote([FromBody]MarkerAndText model)
        {
            user = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.Name).Value;
            return _landMarkAndNotesManager.UpdateLandMarkNote(model);
        }
    }
}