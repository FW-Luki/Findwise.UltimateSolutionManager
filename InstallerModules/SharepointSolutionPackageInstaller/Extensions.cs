using Microsoft.SharePoint.BusinessData.Administration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharepointSolutionPackageInstaller
{
    public static class Extensions
    {
        public static bool HasOnlyInactiveEntities(this LobSystem lobSystem)
        {
            if (lobSystem == null)
            {
                throw new ArgumentNullException("lobSystem");
            }
            if (lobSystem.GetEntityCount() == 0)
            {
                return true;
            }
            foreach (Entity entity in lobSystem.Entities)
            {
                if (entity.Active)
                {
                    return false;
                }
            }
            return true;
        }
    }
}