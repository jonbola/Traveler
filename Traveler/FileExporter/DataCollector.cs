using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Traveler.FileExporter
{
    public interface DataCollector
    {
        void ExportData(TourData tourData);
    }
}