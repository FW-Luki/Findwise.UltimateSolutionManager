using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Findwise.Sharepoint.SolutionInstaller
{
    /// <summary>
    /// Defines a module that performs some special processing on project save/load operations.
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
        void AfterSave(); //Sounds like aftershave

        /// <summary>
        /// Method invoked after the project is loaded.
        /// </summary>
        void AfterLoad();
    }
}
