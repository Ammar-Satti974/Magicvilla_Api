using MagicVilla_VillaApi.Model;
using Microsoft.AspNetCore.Mvc;
using MagicVilla_VillaApi.Model.Dto;
using MagicVilla_VillaApi.Data;


namespace MagicVilla_VillaApi.Controllers
{
    [Route("api/VillaApi")]
    [ApiController]
    public class VillaApiController : ControllerBase
    {
        private readonly AppicationDbContext _db;
        private readonly ILogger<VillaApiController> _logger;
        //public VillaApiController(AppicationDbContext db)
        //{
        //    _db = db;
        //}
        public VillaApiController(AppicationDbContext db, ILogger<VillaApiController> logger)
        {
            _db = db;
            _logger = logger;
        }
        
        //public VillaApiController(ILogger<VillaApiController> logger)
        //{
        //    _logger = logger;
        //}

        //private readonly ILogging _logger;
        //public VillaApiController(ILogging logger)
        //{
        //    _logger = logger;
        //}

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<IEnumerable<VillaDTO>> GetVillas()
        {
            _logger.LogInformation("Getting all villas");
            //_logger.Log("Getting all villas", "");
            //return Ok(VillaStore.villaList);
            return Ok(_db.Villas.ToList());

        }
        [HttpGet("{id:int}", Name = "GetVilla")]
        //[ProducesResponseType(200)]
        //[ProducesResponseType(404)]
        //[ProducesResponseType(400)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<VillaDTO> GetVillas(int id)
        {
            if (id == 0)
            {
                _logger.LogError("Get villa Id with Error" + id);
                //_logger.Log("Get villa Id with Error" + id, "error");
                return BadRequest();
            }
            //var villa = VillaStore.villaList.FirstOrDefault(u => u.Id == id);
            var villa = _db.Villas.FirstOrDefault(u => u.Id == id);
            if (villa == null)
            {
                return NotFound();
            }

            return Ok(villa);

        }
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<VillaDTO> Createvilla([FromBody] VillaDTO villaDTO)
        {
            //if (!ModelState.IsValid)
            //{
            //    return BadRequest(ModelState);
            //}
            //if (VillaStore.villaList.FirstOrDefault(u => u.Name.ToLower() == villaDTO.Name.ToLower()) != null)
            if (_db.Villas.FirstOrDefault(u => u.Name.ToLower() == villaDTO.Name.ToLower()) != null)
            {
                ModelState.AddModelError("CustomError", "Villa already exist!");
                return BadRequest(ModelState);
            }
            if (villaDTO == null) {
                return BadRequest(villaDTO);
            }
            if (villaDTO.Id > 0)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);

            }
            //villaDTO.Id = VillaStore.villaList.OrderByDescending(u => u.Id).FirstOrDefault().Id + 1;
            //VillaStore.villaList.Add(villaDTO);

            Villa model = new()
            {
                Amenity = villaDTO.Amenity,
                Details = villaDTO.Details,
                Id = villaDTO.Id,
                ImageUrl = villaDTO.ImageUrl,
                Name = villaDTO.Name,
                Occupancy = villaDTO.Occupancy,
                Rate = villaDTO.Rate,
                Sqft = villaDTO.Sqft
            };
            _db.Villas.Add(model);
            _db.SaveChanges();


            return CreatedAtRoute("GetVilla", new { id = villaDTO.Id }, villaDTO);
        }

        [HttpDelete("{id:int}", Name = "DeleteVilla")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult DeleteVilla(int id)
        {
            if (id == 0)
            {
                return BadRequest();
            }
            //var villa = VillaStore.villaList.FirstOrDefault(u => u.Id == id);
            var villa = _db.Villas.FirstOrDefault(u => u.Id == id);
            if (villa == null)
            {
                return NotFound();
            }
            //VillaStore.villaList.Remove(villa);
            _db.Villas.Remove(villa);
            _db.SaveChanges();
            return NoContent();
        }
        [HttpPut("{id:int}", Name = "UpdateVilla")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        // [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult UpdateVilla(int id, [FromBody] VillaDTO villaDTO)
        {
            if (villaDTO == null || id != villaDTO.Id) {
                return BadRequest();
            }

            //var villa = VillaStore.villaList.FirstOrDefault(u => u.Id == id);
            //villa.Name = villaDTO.Name;
            //villa.Occupancy = villaDTO.Occupancy;
            //villa.Sqft = villaDTO.Sqft;

            Villa model = new()
            {
                Amenity = villaDTO.Amenity,
                Details = villaDTO.Details,
                Rate = villaDTO.Rate,
                Sqft = villaDTO.Sqft,
                Id = villaDTO.Id,
                Occupancy = villaDTO.Occupancy,
                Name = villaDTO.Name,
                ImageUrl = villaDTO.ImageUrl

            };
            _db.Villas.Update(model);
            _db.SaveChanges();
            return NoContent();
        }
        //[HttpPatch("{id:int}", Name ="UpdatePartialvilla")]
        //[ProducesResponseType(StatusCodes.Status204NoContent)]
        //[ProducesResponseType(StatusCodes.Status400BadRequest)]

        //public IActionResult UpdatePartialvilla(int id, JsonPatchDocument<VillaDTO> patchDTO) {
        //    if (patchDTO == null || id = 0)
        //    {
        //        return BadRequest();
        //    }
        //    var villa = VillaStore.villaList.FirstOrDefault(u => u.Id == id);
        //    var villa = _db.Villas.FirstOrDefault(u => u.Id == id);
        //Villa.Name = "new name";
        //    _db.SaveChanges();
        //VillaDTO villaDTO = new()
        //Villa model = new()
        //{
        //    Amenity = villaDTO.Amenity,
        //    Details = villaDTO.Details,
        //    Rate = villaDTO.Rate,
        //    Sqft = villaDTO.Sqft,
        //    Id = villaDTO.Id,
        //    Occupancy = villaDTO.Occupancy,
        //    Name = villaDTO.Name,
        //    ImageUrl = villaDTO.ImageUrl

        //};
        //    if(villa == null)
        //    {
        //        return BadRequest();
        //    }
        //    patchDTO.ApplyTo(villa, ModelState);
        //    patchDTO.ApplyTo(villaDTO, ModelState);
        //Villa model = new()
        //Villa model = new Villa()
        //{
        //    Amenity = villaDTO.Amenity,
        //    Details = villaDTO.Details,
        //    Rate = villaDTO.Rate,
        //    Sqft = villaDTO.Sqft,
        //    Id = villaDTO.Id,
        //    Occupancy = villaDTO.Occupancy,
        //    Name = villaDTO.Name,
        //    ImageUrl = villaDTO.ImageUrl

        //};
        //_db.Villas.Update(model);
        //_db.SaveChanges();
        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }
        //    return NoContent();
        //}

    }
}
