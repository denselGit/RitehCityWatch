using CityWatch.Web.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace CityWatch.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {

            DeviceViewModel hwm = new DeviceViewModel()
            {
                CategoriesSelectList = new List<SelectListItem>()
                {
                    new SelectListItem("Svi uređaji","0"),
                    new SelectListItem("Kontejneri","1"),
                    new SelectListItem("Šahte","2"),
                    new SelectListItem("Parking","3"),
                },
                CategoryId = 0
            };

            _logger.Log(LogLevel.Information,"Getting list of All Devices");

            return View(hwm);
        }

        private List<CategoryViewModel> DeviceCategoriesList()
        {
            List<CategoryViewModel> list = new List<CategoryViewModel>() {
                new CategoryViewModel() { Id = 0, Name = "Svi uređaji"},
                new CategoryViewModel() { Id = 1, Name = "Kontejneri"},
                new CategoryViewModel() { Id = 2, Name = "Šahte"},
                new CategoryViewModel() { Id = 3, Name = "Parking"},
            };
            return list;
        }

        [HttpGet]
        public JsonResult GetDevicesList(int id)
        {
            List<DeviceViewModel> listVM = new List<DeviceViewModel>();


            listVM.Add(new DeviceViewModel() { Id = 1, LastChange = RandomDay(), State = true, Name = "Kontejner 1", CategoryId = 1, PinPosition= "[45.35627427475257, 14.406555691392729]", DeviceId = Guid.NewGuid(), CategoryVM = DeviceCategoriesList().Where(p => p.Id == 1).FirstOrDefault() });
            listVM.Add(new DeviceViewModel() { Id = 2, LastChange = RandomDay(), State = true, Name = "Kontejner 2", CategoryId = 1, PinPosition = "[45.35675677590046, 14.374283352525767]", DeviceId = Guid.NewGuid(), CategoryVM = DeviceCategoriesList().Where(p => p.Id == 1).FirstOrDefault() });
            listVM.Add(new DeviceViewModel() { Id = 3, LastChange = RandomDay(), State = false, Name = "Kontejner 3", CategoryId = 1, PinPosition= "[45.344934313417966, 14.445351162583865]", DeviceId = Guid.NewGuid(), CategoryVM = DeviceCategoriesList().Where(p => p.Id == 1).FirstOrDefault() });
            listVM.Add(new DeviceViewModel() { Id = 4, LastChange = RandomDay(), State = true, Name = "Kontejner 4", CategoryId = 1, PinPosition= "[45.34059074780746, 14.393852749498288]", DeviceId = Guid.NewGuid(), CategoryVM = DeviceCategoriesList().Where(p => p.Id == 1).FirstOrDefault() });
            listVM.Add(new DeviceViewModel() { Id = 5, LastChange = RandomDay(), State = false, Name = "Kontejner 5", CategoryId = 1, PinPosition= "[45.351690308654966, 14.399689236314652]", DeviceId = Guid.NewGuid(), CategoryVM = DeviceCategoriesList().Where(p => p.Id == 1).FirstOrDefault() });
            listVM.Add(new DeviceViewModel() { Id = 6, LastChange = RandomDay(), State = true, Name = "Kontejner 6", CategoryId = 1, PinPosition= "[45.32707530143406, 14.460114041001725]", DeviceId = Guid.NewGuid(), CategoryVM = DeviceCategoriesList().Where(p => p.Id == 1).FirstOrDefault() });
            listVM.Add(new DeviceViewModel() { Id = 7, LastChange = RandomDay(), State = true, Name = "Kontejner 7", CategoryId = 1, PinPosition= "[45.34276257226861, 14.312828579576982]", DeviceId = Guid.NewGuid(), CategoryVM = DeviceCategoriesList().Where(p => p.Id == 1).FirstOrDefault() });
            listVM.Add(new DeviceViewModel() { Id = 8, LastChange = RandomDay(), State = false, Name = "Kontejner 8", CategoryId = 1, PinPosition = "[45.37002394524227, 14.426468411119155]", DeviceId = Guid.NewGuid(), CategoryVM = DeviceCategoriesList().Where(p => p.Id == 1).FirstOrDefault() });

            listVM.Add(new DeviceViewModel() { Id = 9, LastChange = RandomDay(), State = true, Name = "Šahta 1", CategoryId = 2, PinPosition = "[45.34603620275044, 14.384069884287229]", DeviceId = Guid.NewGuid(), CategoryVM = DeviceCategoriesList().Where(p => p.Id == 2).FirstOrDefault() });
            listVM.Add(new DeviceViewModel() { Id = 10, LastChange = RandomDay(), State = true,  Name = "Šahta 2", CategoryId = 2, PinPosition = "[45.33710757457997, 14.423208675305004]", DeviceId = Guid.NewGuid(), CategoryVM = DeviceCategoriesList().Where(p => p.Id == 2).FirstOrDefault() });
            listVM.Add(new DeviceViewModel() { Id = 11, LastChange = RandomDay(), State = false, Name = "Šahta 3", CategoryId = 2, PinPosition = "[45.32190153534943, 14.477625327728843]", DeviceId = Guid.NewGuid(), CategoryVM = DeviceCategoriesList().Where(p => p.Id == 2).FirstOrDefault() });
            listVM.Add(new DeviceViewModel() { Id = 12, LastChange = RandomDay(), State = true,  Name = "Šahta 4", CategoryId = 2, PinPosition = "[45.31803903407411, 14.470243889071982]", DeviceId = Guid.NewGuid(), CategoryVM = DeviceCategoriesList().Where(p => p.Id == 2).FirstOrDefault() });
            listVM.Add(new DeviceViewModel() { Id = 13, LastChange = RandomDay(), State = false, Name = "Šahta 5", CategoryId = 2, PinPosition = "[45.33879688250097, 14.402265988883212]", DeviceId = Guid.NewGuid(), CategoryVM = DeviceCategoriesList().Where(p => p.Id == 2).FirstOrDefault() });
            listVM.Add(new DeviceViewModel() { Id = 14, LastChange = RandomDay(), State = true,  Name = "Šahta 6", CategoryId = 2, PinPosition = "[45.3630449616131, 14.442778070813894]", DeviceId = Guid.NewGuid(), CategoryVM = DeviceCategoriesList().Where(p => p.Id == 2).FirstOrDefault() });
            listVM.Add(new DeviceViewModel() { Id = 15, LastChange = RandomDay(), State = true,  Name = "Šahta 7", CategoryId = 2, PinPosition = "[45.35492508089824, 14.337822367837779]", DeviceId = Guid.NewGuid(), CategoryVM = DeviceCategoriesList().Where(p => p.Id == 2).FirstOrDefault() });
            listVM.Add(new DeviceViewModel() { Id = 16, LastChange = RandomDay(), State = false, Name = "Šahta 8", CategoryId = 2, PinPosition = "[45.346020276761585, 14.355602695623121]", DeviceId = Guid.NewGuid(), CategoryVM = DeviceCategoriesList().Where(p => p.Id == 2).FirstOrDefault() });

            listVM.Add(new DeviceViewModel() { Id = 17, LastChange = RandomDay(), State = true,  Name = "Parking 1", CategoryId = 3, PinPosition = "[45.33029342098564, 14.457363721439526]", DeviceId = Guid.NewGuid(), CategoryVM = DeviceCategoriesList().Where(p => p.Id == 3).FirstOrDefault() });
            listVM.Add(new DeviceViewModel() { Id = 18, LastChange = RandomDay(), State = true,  Name = "Parking 2", CategoryId = 3, PinPosition = "[45.32172440971138, 14.466589794220187]", DeviceId = Guid.NewGuid(), CategoryVM = DeviceCategoriesList().Where(p => p.Id == 3).FirstOrDefault() });
            listVM.Add(new DeviceViewModel() { Id = 19, LastChange = RandomDay(), State = false, Name = "Parking 3", CategoryId = 3, PinPosition = "[45.33946444739392, 14.408053269057636]", DeviceId = Guid.NewGuid(), CategoryVM = DeviceCategoriesList().Where(p => p.Id == 3).FirstOrDefault() });
            listVM.Add(new DeviceViewModel() { Id = 20, LastChange = RandomDay(), State = true,  Name = "Parking 4", CategoryId = 3, PinPosition = "[45.34489396034702, 14.371317737137444]", DeviceId = Guid.NewGuid(), CategoryVM = DeviceCategoriesList().Where(p => p.Id == 3).FirstOrDefault() });
            listVM.Add(new DeviceViewModel() { Id = 21, LastChange = RandomDay(), State = false, Name = "Parking 5", CategoryId = 3, PinPosition = "[45.33040127912501, 14.44829672917746]", DeviceId = Guid.NewGuid(), CategoryVM = DeviceCategoriesList().Where(p => p.Id == 3).FirstOrDefault() });
            listVM.Add(new DeviceViewModel() { Id = 22, LastChange = RandomDay(), State = true,  Name = "Parking 6", CategoryId = 3, PinPosition = "[45.326823624346005, 14.469915002487335]", DeviceId = Guid.NewGuid(), CategoryVM = DeviceCategoriesList().Where(p => p.Id == 3).FirstOrDefault() });
            listVM.Add(new DeviceViewModel() { Id = 23, LastChange = RandomDay(), State = true,  Name = "Parking 7", CategoryId = 3, PinPosition = "[45.32436453305841, 14.441849619381175]", DeviceId = Guid.NewGuid(), CategoryVM = DeviceCategoriesList().Where(p => p.Id == 3).FirstOrDefault() });
            listVM.Add(new DeviceViewModel() { Id = 24, LastChange = RandomDay(), State = false, Name = "Parking 8", CategoryId = 3, PinPosition = "[45.34215363358111, 14.372491102090223]", DeviceId = Guid.NewGuid(), CategoryVM = DeviceCategoriesList().Where(p => p.Id == 3).FirstOrDefault() });

            if (id != 0)
                listVM = listVM.Where(p => p.CategoryVM.Id == id).ToList();

            return Json( new { data = listVM });
        }

        private Random gen = new Random();
        DateTime RandomDay()
        {
            DateTime start = new DateTime(2021, 10, 1);
            int range = (DateTime.Today - start).Days;
            return start.AddDays(gen.Next(range));
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
