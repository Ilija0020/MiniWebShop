using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MiniWebShop.Api.Models;
using MiniWebShop.Api.Repositories;

namespace MiniWebShop.Api.Controllers
{
    [Route("api/artists")]
    [ApiController]
    public class ArtistController : ControllerBase
    {
        private readonly ArtistRepository artistRepo;

        public ArtistController(IConfiguration configuration)
        {
            artistRepo = new ArtistRepository(configuration);
        }

        [HttpGet]
        public ActionResult GetPaged([FromQuery] int page = 1, [FromQuery] int pageSize = 10)
        {
            if (page < 1 || pageSize < 1)
            {
                return BadRequest("Broj stranice i veličina moraju biti veći od nule.");
            }

            try
            {
                List<Artist> artists = artistRepo.GetPaged(page, pageSize);
                int totalCount = artistRepo.CountAll();

                Object result = new
                {
                    Data = artists,
                    TotalCount = totalCount
                };

                return Ok(result);
            }
            catch (Exception ex)
            {
                return Problem("Došlo je do greške prilikom dobavljanja liste izvođača.");
            }
        }

        [HttpGet("{id}")]
        public ActionResult<Artist> GetById(int id)
        {
            try
            {
                Artist? artist = artistRepo.GetById(id);
                if (artist == null)
                {
                    return NotFound($"Izvođač sa ID-jem {id} nije pronađen.");
                }
                return Ok(artist);
            }
            catch (Exception ex)
            {
                return Problem("Greška pri čitanju podataka iz baze.");
            }
        }

        [HttpPost]
        public ActionResult<Artist> Create([FromBody] Artist newArtist)
        {
            if (newArtist == null || string.IsNullOrWhiteSpace(newArtist.Name))
            {
                return BadRequest("Podaci o izvođaču nisu validni.");
            }

            try
            {
                Artist createdArtist = artistRepo.Create(newArtist);
                return Ok(createdArtist);
            }
            catch (Exception ex)
            {
                return Problem("Greška prilikom dodavanja novog izvođača.");
            }
        }

        [HttpPut("{id}")]
        public ActionResult<Artist> Update(int id, [FromBody] Artist artist)
        {
            if (artist == null || string.IsNullOrWhiteSpace(artist.Name))
            {
                return BadRequest("Neispravni podaci za ažuriranje.");
            }

            try
            {
                artist.Id = id;
                Artist? updatedArtist = artistRepo.Update(artist);

                if (updatedArtist == null)
                {
                    return NotFound($"Nemoguće ažurirati: Izvođač sa ID-jem {id} ne postoji.");
                }

                return Ok(updatedArtist);
            }
            catch (Exception ex)
            {
                return Problem("Greška prilikom izmene podataka.");
            }
        }

        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            try
            {
                bool isDeleted = artistRepo.Delete(id);
                if (isDeleted)
                {
                    return NoContent();
                }
                return NotFound($"Izvođač sa ID-jem {id} nije pronađen za brisanje.");
            }
            catch (Exception ex)
            {
                return Problem("Greška prilikom brisanja iz baze.");
            }
        }
    }
}
