using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Ajax;
using Kona.Model;
using Kona.Infrastructure;

namespace Kona.Web.Controllers
{
    public class WidgetController : Controller
    {

        IProductRepository _productRepository;
        ICMSRepository _cmsRepository;
        public WidgetController(IProductRepository productRepository,
            ICMSRepository cmsRepository) {
            _productRepository = productRepository;
            _cmsRepository = cmsRepository;
        }
        
        
        //
        // GET: /Widget/

        public ActionResult Index()
        {
            return View();
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Create(IWidget widget) {
            if (ModelState.IsValid) {
                //save em
                _cmsRepository.SaveWidget(widget);
            }
            return new EmptyResult();
        }
    }
}
