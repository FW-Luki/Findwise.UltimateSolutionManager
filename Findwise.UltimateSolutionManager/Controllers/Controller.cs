using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Findwise.SolutionManager.Controllers
{
    public abstract class Controller : Component
    {
        public static T GetController<T>(IEnumerable<Controller> controllerCollection) where T : Controller
        {
            return controllerCollection.OfType<T>().FirstOrDefault()
                ?? throw new InvalidOperationException($"{typeof(T).Name} controller not set.");
        }
    }
}