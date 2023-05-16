using AM.ApplicationCore;
using AM.ApplicationCore.Domain;
using AM.ApplicationCore.Interfaces;
using AM.ApplicationCore.Services;
using AM.Infrastructure;
using AM.UI.Web.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace AM.UI.Web.Controllers
{
    public class FlightController : Controller
    {
        IUnitOfWork iUnitOfWork;

        IFlightService iFlightService;

        IWebHostEnvironment iwebHostEnvironment;

        IPlaneService iplaneService;

        public FlightController(IUnitOfWork iUnitOfWork, IFlightService iFlightService, IWebHostEnvironment iwebHostEnvironment, IPlaneService iplaneService)
        {
            this.iUnitOfWork = iUnitOfWork;
            this.iFlightService = iFlightService;
            this.iwebHostEnvironment = iwebHostEnvironment;
            this.iplaneService = iplaneService;
        }


        public ActionResult Index()
        {
            return View(iFlightService.GetAll());
        }

        [HttpPost]
        public ActionResult Index(DateTime? filter)
        {
            if(filter == null)
            {
               return View(iFlightService.GetAll());
            }

            return View(iFlightService.GetFlightsByDate(filter.Value));
        }

        public ActionResult Details(int id)
        {
            return View(iFlightService.GetById(id));
        }

        public ActionResult Create()
        {
            var list = iplaneService.GetAll();
            ViewBag.Planes = new SelectList(list.Select(e => new { e.PlaneId, Description = e.PlaneId + " ** " + e.PlaneType }), nameof(Plane.PlaneId), "Description") ;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Flight flight)
        {
            try
            {
                if(!ModelState.IsValid)
                {
                    return View();
                }
               flight.Pilot = Request.AddRequestFile(iwebHostEnvironment,"wwwroot","upload");

                iFlightService.Add(flight);
                iUnitOfWork.Commit();
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        public ActionResult Edit(int id)
        {
            return View(iFlightService.GetById(id));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, Flight flight)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View();
                }

                iFlightService.Update(flight);
                iUnitOfWork.Commit();
                return RedirectToAction(nameof(Index));
    
            }
            catch
            {
                return View();
            }
        }

        public ActionResult Delete(int id)
        {
            return View(iFlightService.GetById(id));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, Flight flight)
        {
            try
            {
                flight.FlightId = id;
                iFlightService.Delete(flight);
                iUnitOfWork.Commit();

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
