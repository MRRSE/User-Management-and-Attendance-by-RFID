using Microsoft.AspNetCore.Mvc;

namespace gym.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RfidController : ControllerBase
    {
        private static readonly string _filePath = "uid.txt";

        [HttpGet]
        public IActionResult Get()
        {
            if (System.IO.File.Exists(_filePath))
            {
                var uid = System.IO.File.ReadAllText(_filePath);

                // فایل رو بعد از خوندن پاک می‌کنیم
                System.IO.File.Delete(_filePath);

                return Ok(uid);
            }
            return NotFound("");
        }

        [HttpPost]
        public IActionResult Post([FromBody] string uid)
        {
            Console.WriteLine("کد کارت دریافتی: " + uid);
            System.IO.File.WriteAllText(_filePath, uid);
            return Ok(new { message = "UID دریافت شد", code = uid });
        }
    }
}
