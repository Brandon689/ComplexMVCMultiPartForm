using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

public class SearchData
{
    public string Jesse { get; set; }
    public string James { get; set; }
    public string PickedItems { get; set; }
    public IFormFile Image { get; set; } // Property to hold the uploaded image file
    public string Gender { get; set; } // Property to hold the selected gender option
}

public class SearchFilter
{
    public string Keywords { get; set; }
    public bool Strict { get; set; }
}
public class ImgData
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Image { get; set; }
}
namespace ComplexForm.Api
{
    [Route("api/[controller]")]
    [ApiController]
    public class FormController : ControllerBase
    {
        private readonly IWebHostEnvironment _webHostEnvironment;

        public FormController(IWebHostEnvironment webHostEnvironment)
        {
            _webHostEnvironment = webHostEnvironment;
        }

        [HttpPost("submit")]
        public IActionResult Submit([FromForm] SearchData searchData)
        {
            Console.WriteLine(JsonSerializer.Serialize(searchData));
            var results = new string[] { $"Results for keywords: '{searchData.Jesse}', strict: {searchData.James}" };

            if (searchData.Image != null && searchData.Image.Length > 0)
            {
                var uploadsFolder = Path.Combine(_webHostEnvironment.WebRootPath, "uploads");
                var uniqueFileName = Guid.NewGuid().ToString() + "_" + searchData.Image.FileName;
                var filePath = Path.Combine(uploadsFolder, uniqueFileName);

                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    searchData.Image.CopyTo(fileStream);
                }
            }
            return Ok(results);
        }

        [HttpPost("search")]
        public IActionResult Search([FromForm] SearchFilter words)
        {
            Console.WriteLine(JsonSerializer.Serialize(words));
            List<ImgData> y = new List<ImgData>();
            y.Add(new ImgData
            {
                Id =1,
                Name="Fire",
                Image = "https://static.myfigurecollection.net/upload/items/0/186848-06b0b.jpg"
            });
            y.Add(new ImgData
            {
                Id=2,
                Name = "Inferno",
                Image= "https://static.myfigurecollection.net/upload/items/0/287683-72c19.jpg"
            });
            return Ok(y);
            //Console.WriteLine(JsonSerializer.Serialize(searchData));
            //var results = new string[] { $"Results for keywords: '{searchData.Jesse}', strict: {searchData.James}" };

            //if (searchData.Image != null && searchData.Image.Length > 0)
            //{
            //    var uploadsFolder = Path.Combine(_webHostEnvironment.WebRootPath, "uploads");
            //    var uniqueFileName = Guid.NewGuid().ToString() + "_" + searchData.Image.FileName;
            //    var filePath = Path.Combine(uploadsFolder, uniqueFileName);

            //    using (var fileStream = new FileStream(filePath, FileMode.Create))
            //    {
            //        searchData.Image.CopyTo(fileStream);
            //    }
            //}
            //return Ok(results);
        }
    }
}
