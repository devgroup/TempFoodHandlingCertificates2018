using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace TempFoodHandlingCertificates2014.Models
{
    [MetadataType(typeof(FoodDetailsMetadata))]
    public partial class FoodDetails
    {
        public int Id { get; set; }
    }

    public class FoodDetailsMetadata
    {
        [Display(Name="Type of food to be sold")]
        public string FoodDetailsTypeOfFood { get; set; }

        [Display(Name = "Where will the food be stored before the event? (Include address and conditions of storage)")]
        public string FoodDetailsWhereStoredPrior { get; set; }

        [Display(Name = "Will any foods be prepared/cooked other than at the temporary food outlet?  (Include address and Certificate of Registration eference number where applicable).")]
        public string FoodDetailsFoodsPreparedOffSite { get; set; }

        [Display(Name = "Food preparation - describe food to be prepared at event site.")]
        public string FoodDetailsFoodsPreparedOnSite { get; set; }
    }
}