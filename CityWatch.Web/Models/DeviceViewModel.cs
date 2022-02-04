using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;


namespace CityWatch.Web.Models
{
    public class DeviceViewModel
    {
        [Key]
        public int Id { get; set; }

        [Display(Name = "ID uređaja")]
        [Required(ErrorMessage = "Id uređaja je obvezan.")]
        public Guid DeviceId {get;set;}

        [Display(Name = "Naziv uređaja")]
        [Required(ErrorMessage = "Naziv uređaja je obvezan.")]
        public string Name { get; set; }

        [Display(Name = "Pozicija uređaja")]
        [Required(ErrorMessage = "Pozicija uređaja je obvezna.")]
        public string PinPosition { get; set; }

        [Display(Name = "Kategorija uređaja")]
        [Required(ErrorMessage = "Kategorija uređaja je obvezna.")]
        public int CategoryId { get; set; }

        public bool State { get; set; }

        public DateTime LastChange { get; set; }


        public CategoryViewModel CategoryVM { get; set; }

        public IEnumerable<SelectListItem> CategoriesSelectList { get; set; }

    }
}
