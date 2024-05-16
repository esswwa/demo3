using demo3.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace demo3.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly DemoexzContext _context;

        public HomeController(ILogger<HomeController> logger, DemoexzContext context)
        {
            _logger = logger;
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            _context.TypeMalfunctions.ToList();
            _context.Equipment.ToList();
            _context.Users.ToList();
            _context.Executors.ToList();
            if (UserCheck.User != null)
            {
                ViewBag.Executors = _context.Executors.ToList();
                if (UserCheck.User.IdRole == 0 || UserCheck.User.IdRole == 2)
                {
                    return View(_context.Applications.ToList());
                }
                else
                {
                    var Users = _context.Executors.Where(i => i.IdExecutor == UserCheck.User.IdUser).ToList();

                    return View(_context.Applications.Where(i => Users.Any(j => i.IdApplication == j.IdApplication)).ToList());

                }
            }

            else
            {
                return RedirectToAction("SignIn");
            }
        }
        [HttpPost]
        public async Task<IActionResult> Index(string search)
        {
            _context.TypeMalfunctions.ToList();
            _context.Equipment.ToList();
            _context.Users.ToList();
            _context.Executors.ToList();
            int n;
            if (UserCheck.User != null)
            {
                ViewBag.Executors = _context.Executors.ToList();
                if (UserCheck.User.IdRole == 0 || UserCheck.User.IdRole == 2)
                {
                    if (search != null)
                    {
                        bool z = int.TryParse(search, out n);
                        if(z)
                            return View(_context.Applications.Where(i => i.IdApplication == int.Parse(search)).ToList());
                        else
                            return View(_context.Applications.Where(i => i.Description == search).ToList());

                    }
                    else
                    {
                        return View(_context.Applications.ToList());

                    }
                }
                else
                {

                    var Users = _context.Executors.Where(i => i.IdExecutor == UserCheck.User.IdUser).ToList();
                    if (search != null)
                    {
                        bool z = int.TryParse(search, out n);
                        if (z)
                            return View(_context.Applications.Where(i => Users.Any(j => i.IdApplication == j.IdApplication) && i.IdApplication == int.Parse(search)).ToList());
                        else
                            return View(_context.Applications.Where(i => Users.Any(j => i.IdApplication == j.IdApplication) && i.Description == search).ToList());
                    }
                    else
                    {

                        return View(_context.Applications.Where(i => Users.Any(j => i.IdApplication == j.IdApplication)).ToList());

                    }

                }
                
            }
            else
            {
                return RedirectToAction("SignIn");
            }
        }
        public async Task<IActionResult> SignIn()
        {
         
                return View();
          
        }
        [HttpPost]
        public async Task<IActionResult> SignIn(string Login, string Password)
        {
            var user = _context.Users.FirstOrDefault(i => i.Login == Login && i.Password == Password);
            if(user != null)
            {
                UserCheck.User = user;
                return RedirectToAction("Index");
            }
            return View();

        }
        public async Task<IActionResult> SignUp()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> SignUp(string Login, string FullName, string Password)
        {
            var user = _context.Users.FirstOrDefault(i => i.Login == Login);
            if(user == null)
            {
                _context.Users.Add(new User { IdUser = _context.Users.Max(i => i.IdUser) + 1, FullName = FullName, Login = Login, Password = Password});
                await _context.SaveChangesAsync();
                return RedirectToAction("SignIn");
            }
            return View();
        }
        public async Task<IActionResult> Statistics()
        {
            if (UserCheck.User != null && UserCheck.User.IdRole == 0)
            {

                return View();
            }
            else
            {
                return RedirectToAction("SignIn");
            }
        }
        public async Task<IActionResult> AddApplication(int id)
        {
            if (UserCheck.User != null && UserCheck.User.IdRole == 0)
            {

                var type = _context.TypeMalfunctions.ToList();
                var typeS = type.Select(i => new SelectListItem{ Text = i.TypeMalfunctionName, Value = i.IdTypeMalfunction.ToString() });
                var equipment = _context.Equipment.ToList();
                var equipmentS = equipment.Select(i => new SelectListItem { Text = i.EquipmentName, Value = i.IdEquipment.ToString() });
                ViewBag.TypeMalfunction = typeS;
                ViewBag.Equipment = equipmentS;
                if (id != 0)
                {
                    var application = _context.Applications.FirstOrDefault(i => i.IdApplication == id);
                    if (application != null)
                    {
                        ViewBag.Application = application;
                        return View();
                    }
                    else
                    {
                        return View();
                    }
                }
                else
                {
                    return View();
                }
            }
            else
            {
                return RedirectToAction("SignIn");
            }
        }
        [HttpPost]
        public async Task<IActionResult> AddApplication(int IdApplication, string Description, string PhaseComplete, int IdTypeMalfunction, int IdEquipment, string FullName, double PriceApplication)
        {
            if (UserCheck.User != null && UserCheck.User.IdRole == 0)
            {
                var application = _context.Applications.FirstOrDefault(i=>i.IdApplication == IdApplication);
                var applications = await _context.Applications.ToListAsync();
                if (application == null)
                {
                    DateOnly date = DateOnly.FromDateTime(DateTime.Now);
      
                    //applications.Add()
                    _context.Applications.Add(new Application { IdApplication = IdApplication, DateAddApplication = date, Description = Description,
                        PhaseComplete = PhaseComplete, StatusApplication = "В ожидании", IdTypeMalfunction = IdTypeMalfunction, IdEquipment = IdEquipment,
                        FullName = FullName, PriceApplication = PriceApplication });
                        await _context.SaveChangesAsync();
                    return RedirectToAction("Index");
                }
                else
                {
                    return RedirectToAction("AddApplication");
                }
              
            }
            else
            {
                return RedirectToAction("SignIn");
            }
        }
        [HttpPost]
        public async Task<IActionResult> EditApplication(int IdApplication, string Description, string PhaseComplete, int IdTypeMalfunction, int IdEquipment, string FullName, double PriceApplication)
        {
            if (UserCheck.User != null && UserCheck.User.IdRole == 0)
            {
                var application = _context.Applications.FirstOrDefault(i => i.IdApplication == IdApplication);
                var applications = await _context.Applications.ToListAsync();
                if (application != null)
                {
                    application.Description = Description;
                    application.PhaseComplete = PhaseComplete;
                    application.IdTypeMalfunction = IdTypeMalfunction;
                    application.IdEquipment = IdEquipment;
                    application.FullName = FullName;
                    application.PriceApplication = PriceApplication;

                    applications.Add(application);
                    await _context.SaveChangesAsync();
                    return RedirectToAction("Index");
                }
                else
                {
                    return RedirectToAction("AddApplication");
                }

            }
            else
            {
                return RedirectToAction("SignIn");
            }
        }
        public async Task<IActionResult> Privacy()
        {
            if (UserCheck.User != null)
            {

                return View();
            }
            else
            {
                return RedirectToAction("SignIn");
            }
        }



        public async Task<IActionResult> AddExecutor(int id)
        {
            if (UserCheck.User != null && (UserCheck.User.IdRole == 0 || UserCheck.User.IdRole == 2))
            {

                var users = _context.Users.Where(i=>i.IdRole == 2).ToList();
                var usersS = users.Select(i => new SelectListItem { Text = i.FullName, Value = i.IdUser.ToString() });

                ViewBag.Executors = usersS;

                if (id != 0)
                {
                    var application = _context.Applications.FirstOrDefault(i => i.IdApplication == id);
                    ViewBag.IdApplication = id;
                    return View();
                    
                }
                else
                {
                    return View();
                }
            }
            else
            {
                return RedirectToAction("SignIn");
            }
        }
        [HttpPost]
        public async Task<IActionResult> AddExecutor(int IdApplication, int IdExecutor)
        {
            if (UserCheck.User != null && (UserCheck.User.IdRole == 0 || UserCheck.User.IdRole == 2))
            {
                var executor = _context.Executors.FirstOrDefault(i => i.IdUser == IdExecutor && i.IdApplication == IdApplication);
                
                if (executor == null)
                {
                    _context.Executors.Add(new Executor
                    {
                        IdExecutor = _context.Executors.Max(i=>i.IdExecutor) + 1,
                        IdApplication = IdApplication,
                        IdUser = IdExecutor
                    });
                    await _context.SaveChangesAsync();
                    return RedirectToAction("Index");
                }
                else
                {
                    return RedirectToAction("Index");
                }

            }
            else
            {
                return RedirectToAction("SignIn");
            }
        }



        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
