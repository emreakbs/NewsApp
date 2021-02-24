using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ImageApp.Views.Shared.Components.InfoComponent
{
    public class InfoComponentViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View("InfoComponentView");
        }
    }
}
