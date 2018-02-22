using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Findwise.SolutionManager
{
    /// <summary>
    /// Defines a module that performs additional processing on project save/load operations.
    /// </summary>
    public interface ISaveLoadAware
    {
        /// <summary>
        /// Method invoked before the project is saved.
        /// </summary>
        void BeforeSave();

        /// <summary>
        /// Method invoked after the project is saved.
        /// </summary>
        void AfterSave();

        /// <summary>
        /// Method invoked after the project is loaded.
        /// </summary>
        void AfterLoad();
    }
}
