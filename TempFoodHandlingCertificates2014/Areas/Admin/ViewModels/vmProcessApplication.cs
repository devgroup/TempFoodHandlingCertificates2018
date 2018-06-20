using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TempFoodHandlingCertificates2014.Areas.Admin.ViewModels
{
    public class vmProcessApplication
    {
        public int Id { get; set; }
        public int AppId { get; set; }

        public Models.Application Application { get; set; }

        public Models.ProcessApplication ProcessApplication { get; set; }
    }
}