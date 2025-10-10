using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Gym.Models;
using Microsoft.JSInterop.Infrastructure;
using DNTPersianUtils.Core;
using System.ComponentModel.Design;
using System;
using System.IO.Ports;
using Microsoft.Extensions.ObjectPool;
using Microsoft.AspNetCore.Components.Web;
using System.Net.NetworkInformation;
using System.Xml.Linq;

namespace Gym.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;

    private readonly GymContext db = new GymContext();

    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }

    public IActionResult Index()
    {
        return View();
    }
    public IActionResult SingUp()
    {
        return View();
    }
    public IActionResult changePassword()
    {
        return View();
    }

    public IActionResult Privacy()
    {
        return View();
    }

    public IActionResult Index2()
    {
        return View();
    }

    public IActionResult userlogs()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
    [HttpPost]
    public string newPerson(string name, string lname, string age, string classes, string Pnumber, string CardUID)
    {
        var currentDate = DateTime.Now.ToShortPersianDateString();


        var exist = db.Men.FirstOrDefault(x => x.Uid == CardUID);

        int t = int.Parse(classes);

        if (exist != null)
        {
            return "card mojod ast";
        }
        else
        {
            Man man = new Man();
            man.Name = name;
            man.Lname = lname;
            man.Age = age;
            man.Classes = t;
            man.Number = Pnumber;
            man.Date = currentDate;
            man.Uid = CardUID;
            man.Status = false;

            db.Men.Add(man);
            db.SaveChanges(); // بعد از این خط، man.Id مقدار می‌گیره

            return $"{man.Id}"; // آیدی جدید رو به عنوان خروجی می‌دی
        }
    }

    public List<Man> getData(int take, int skip)
    {
        return db.Men.Skip(skip).Take(take).ToList();
    }

    // public IActionResult getmen(int take, int skip, string filterValue)
    // {
    //     var user = db.Men
    //     .Where(x => x.Gender == filterValue)
    //     .Skip(skip)
    //     .Take(take)
    //     .ToList();
    //     return Ok(user);
    // }

    // public IActionResult getwomen(int take, int skip, string filterValue)
    // {
    //     var user = db.Men
    //     .Where(x => x.Gender == filterValue)
    //     .Skip(skip)
    //     .Take(take)
    //     .ToList();
    //     return Ok(user);
    // }

    [HttpGet]
    public IActionResult GetExpiredUsers(int take, int skip)
    {
        var expiredUsers = db.Men
        .Where(u => u.Classes == 0)
        .Skip(skip)
        .Take(take)
        .ToList();
        if (expiredUsers.Any())
            return Ok(expiredUsers);
        else
            return NotFound("هیچ کاربری با جلسات صفر یافت نشد");
    }

    [HttpPost]
    public string updateClasses(int userId, string newClasses)
    {
        int t = int.Parse(newClasses);
        Thread.Sleep(1000);
        Man man = db.Men.Where(x => x.Id == userId).FirstOrDefault();
        man.Classes = t;
        if (man != null)
        {
            db.SaveChanges();
            return "done";
        }
        else
        {
            return "bruh";
        }
    }

    [HttpGet]
    public IActionResult findUser(int Id)
    {
        // جستجوی کاربر در پایگاه داده
        var user = db.Men.SingleOrDefault(u => u.Id == Id);

        if (user != null)
        {
            return Json(user); // بازگشت اطلاعات کاربر در قالب JSON
        }

        return NotFound(); // اگر کاربر پیدا نشد
    }

    [HttpGet]
    public IActionResult UserFind(int Id)
    {
        // جستجوی کاربر در پایگاه داده
        var user = db.Men.SingleOrDefault(u => u.Id == Id);

        if (user != null)
        {
            return Json(user); // بازگشت اطلاعات کاربر در قالب JSON
        }

        return NotFound(); // اگر کاربر پیدا نشد
    }
    [HttpGet]
    public IActionResult findId(long Id)
    {
        var user = db.Men.FirstOrDefault(u => u.Id == Id);
        if (user != null)
        {
            return Json(user);
        }
        else
        {
            return NotFound();
        }
    }
    [HttpPost]
    public IActionResult findByName(string name)
    {
        var user = db.Men.Where(u => u.Lname.Contains(name)).ToList();
        if (user != null)
        {
            return Ok(user);
        }
        else
        {
            return NotFound();
        }
    }

    [HttpPost]
    public IActionResult kkklll([FromBody] long id)
    {
        Console.WriteLine($"ID دریافتی: {id}");

        using (var db = new GymContext())
        {
            var user = db.Men.Find(id); // جستجوی کاربر
            if (user != null)
            {
                db.Men.Remove(user); // حذف کاربر
                db.SaveChanges();
                return Json(new { success = true, message = "کاربر حذف شد." });
            }
            return Json(new { success = false, message = "کاربر یافت نشد." });
        }
    }
    public IActionResult findUse(long id)
    {
        Man sing = db.Men.FirstOrDefault(u => u.Id == id);
        return View("profile", sing);
    }

    [HttpPost]
    public IActionResult EditClasses(int id, string classes, string registrationDate)
    {
        try
        {
            var user = db.Men.FirstOrDefault(x => x.Id == id);
            if (user == null)
            {
                return BadRequest("کاربر پیدا نشد");
            }
            int t = int.Parse(classes);

            user.Classes = t;
            var currentDate = DateTime.Now.ToShortPersianDateString();
            user.Date = currentDate;


            db.SaveChanges();
            return Ok("ویرایش انجام شد");
        }
        catch (Exception ex)
        {
            return StatusCode(500, "خطای سرور: " + ex.Message);
        }
    }


    public IActionResult checkUser(string username, string password)
    {
        var user = db.AdminUsers.Where(x => x.Username == username && x.Password == password).FirstOrDefault();
        if (user != null)
        {
            // HttpContext.Session.SetString("userSession",username);
            // HttpContext.Session.SetString("userId",user.UserId.ToString());
            return Json(new { success = true, role = user.Accessibility });
        }
        else
        {
            return Unauthorized(new { success = false, message = "نام کاربری یا رمز عبور اشتباه است." });
        }
    }

    [HttpPost]
    public string SingByUid(string uid)
    {
        var FindByuid = db.Men.Where(x => x.Uid == uid).FirstOrDefault();

        if (FindByuid == null)
        {
            return "شناسه یافت نشد.";
        }

        if (FindByuid.Status == false)
        {
            FindByuid.Status = true;
            UserLog userLog = new UserLog
            {
                Userid = FindByuid.Id,
                Name = FindByuid.Name,
                Lname = FindByuid.Lname,
                Enterytime = "ok",
                Enterydate = DateTime.Now
            };
            db.UserLogs.Add(userLog);
            db.SaveChanges();
            return $"کاربر {FindByuid.Lname} وارد شد.";
        }

        else if (FindByuid.Status == true)
        {
            var findLog = db.UserLogs.Where(u => u.Userid == FindByuid.Id && u.Exittime == null).FirstOrDefault();
            findLog.Exittime = "byebye";
            findLog.Exitdate = DateTime.Now;
            FindByuid.Status = false;
            TimeSpan workedHours = findLog.Exitdate.Value - findLog.Enterydate.Value;
            findLog.Workedhours = workedHours;
            int workedMinutes = (int)workedHours.TotalMinutes;
            FindByuid.Classes -= workedMinutes;
            db.SaveChanges();
            return $"کاربر {findLog.Lname} خارج شد.";
        }
        return "err";
    }

    [HttpPost]
    public IActionResult updateUser([FromBody] Dictionary<string, string> data)
    {
        if (data == null || !data.ContainsKey("id") || string.IsNullOrWhiteSpace(data["id"]) || !long.TryParse(data["id"], out long userId))
        {
            return BadRequest("شناسه کاربر نامعتبر است.");
        }


        // if (!data.ContainsKey("id") || !long.TryParse(data["id"], out long userId))
        // {
        //     return BadRequest("شناسه کاربر نامعتبر است.");
        // }

        var user = db.Men.FirstOrDefault(x => x.Id == userId);
        if (user == null)
        {
            return NotFound("کاربر یافت نشد.");
        }

        try
        {
            if (data.ContainsKey("name"))
            {
                user.Name = data["name"];
            }

            if (data.ContainsKey("lname"))
            {
                user.Lname = data["lname"];
            }

            if (data.ContainsKey("age"))
            {
                user.Age = data["age"]; // چون nchar هست نیازی به تبدیل نیست
            }


            if (data.ContainsKey("number"))
            {
                user.Number = data["number"];
            }


            db.SaveChanges();

            return Json(new { success = true });
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"خطای سرور: {ex.Message}");
        }
    }
    [HttpPost]
    public string editUid(string uid, int id)
    {
        var existUid = db.Men.FirstOrDefault(y => y.Uid == uid);
        if (existUid != null)
        {
            return "این کارت قبلا ثبت شده است";
        }
        else
        {
            var user = db.Men.FirstOrDefault(x => x.Id == id);

            user.Uid = uid;
            db.SaveChanges();
            return "کارت ثبت شد";
        }
    }
    [HttpPost]
    public string tamdidJalase(int id, string newClasses)
    {
        int t = int.Parse(newClasses);
        var currentDate = DateTime.Now.ToShortPersianDateString();
        var user = db.Men.FirstOrDefault(x => x.Id == id);
        if (user != null)
        {
            user.Date = currentDate;
            user.Classes = t;
            db.SaveChanges();
            return "جلسات تمدید شد.";
        }
        return "خطایی رخ داد";
    }

    // [HttpPost]
    // public string manualPer(int id, int presetCount)
    // {
    //     var user = db.Men.FirstOrDefault(x => x.Id == id);

    //     if (user != null)
    //     {
    //         int currentClass;
    //         if (int.TryParse(user.Classes, out currentClass))
    //         {
    //             currentClass = currentClass - presetCount;
    //             user.Classes = currentClass.ToString();
    //             db.SaveChanges();
    //             return "حضور ثبت شد.";
    //         }
    //     }
    //     return "خطایی رخ داد!";
    // }
    [HttpPost]
    public string newPass(string username, string password, int id)
    {
        int currentId;
        var user = db.AdminUsers.FirstOrDefault(x => x.Adminid == id);

        if (user != null)
        {
            user.Username = username;
            user.Password = password;
            db.SaveChanges();
            return "تغیرات اعمال شد.";
        }
        return "خطایی رخ داد!";
    }
    public IActionResult GetLogss(int take, int skip)
    {
        var logs = db.UserLogs.OrderByDescending(l => l.Logid)
        .Skip(skip)
        .Take(take)
        .ToList();

        var result = logs.Select(x => new
        {
            x.Userid,
            x.Name,
            x.Lname,
            EnteryDate = x.Enterydate?.ToShortPersianDateTimeString(), // ← شمسی
            ExitDate = x.Exitdate?.ToShortPersianDateTimeString()      // ← شمسی
        });
        return Ok(result);
    }
    public IActionResult onlineLogs(int take, int skip)
    {
        var logs = db.UserLogs.Where(l => l.Exitdate == null)
        .OrderByDescending(l => l.Logid)
        .Skip(skip)
        .Take(take)
        .ToList();

        var result = logs.Select(x => new
        {
            x.Userid,
            x.Name,
            x.Lname,
            EnteryDate = x.Enterydate?.ToShortPersianDateTimeString(), // ← شمسی
            ExitDate = x.Exitdate?.ToShortPersianDateTimeString()      // ← شمسی
        });
        return Ok(result);
    }

    public IActionResult oldLogs(int take, int skip)
    {
        var logs = db.UserLogs
        .Skip(skip)
        .Take(take)
        .ToList();

        var result = logs.Select(x => new
        {
            x.Userid,
            x.Name,
            x.Lname,
            EnteryDate = x.Enterydate?.ToShortPersianDateTimeString(), // ← شمسی
            ExitDate = x.Exitdate?.ToShortPersianDateTimeString()      // ← شمسی
        });
        return Ok(result);
    }
    public IActionResult SearchLogs(string name)
    {
        var logs = db.UserLogs
        .Where(x => x.Lname == name)
        .OrderByDescending(l => l.Logid)
        .ToList();
        if (logs == null)
        {
            return NotFound("کاربر پیدا نشد");
        }
        else
        {
            var result = logs.Select(x => new
            {
                x.Userid,
                x.Name,
                x.Lname,
                EnteryDate = x.Enterydate?.ToShortPersianDateTimeString(), // ← شمسی
                ExitDate = x.Exitdate?.ToShortPersianDateTimeString()      // ← شمسی
            });
            return Ok(result);
        }
    }
}
