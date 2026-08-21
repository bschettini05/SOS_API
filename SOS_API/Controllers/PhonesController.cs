using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SOS_API.Data;
using SOS_API.Models;
using System.Reflection.Metadata;

namespace SOS_API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PhonesController : ControllerBase
    {
        private readonly AppDbContext _context;

        public PhonesController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet("{iso3Code}")]
        public async Task<IActionResult> GetPhonesByCountry(string iso3Code)
        {
            var country = await _context.Countries
                .FirstOrDefaultAsync(c => c.Iso3Code == iso3Code.ToUpper());
            
            if (country == null) {
                return NotFound(new
                {
                    message = "Country not found"
                });
             }

            var phones = await _context.Phones
                .Where(p => p.CountryId == country.Id)
                .OrderBy(p => p.Id)
                .Select(p => new
                {
                    p.Id,
                    p.CountryId,
                    p.Category,
                    p.PhoneNumber
                })
                .ToListAsync();

            return Ok(phones);
        }

        [HttpGet("all")]
        public async Task<IActionResult> GetAllPhones()
        {
            var phones = await (
                from phone in _context.Phones.AsNoTracking()
                join country in _context.Countries.AsNoTracking()
                    on phone.CountryId equals country.Id
                orderby country.Name, phone.Category
                select new
                {
                    number = phone.PhoneNumber,
                    category = phone.Category,
                    countryName = country.Name,
                    countryIso2Code = country.Iso2Code,
                    countryIso3Code = country.Iso3Code,
                }
            ).ToListAsync();

            return Ok(phones);
        }
    }
}