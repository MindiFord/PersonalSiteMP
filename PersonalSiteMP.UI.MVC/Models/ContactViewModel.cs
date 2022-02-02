using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace PersonalSiteMP.UI.MVC.Models
{
    public class ContactViewModel
    {
        [Required(ErrorMessage = " * Name is required")]
        public string Name { get; set; }

        [Required(ErrorMessage = " * Email is required")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [UIHint("MultilineText")]
        [Required(ErrorMessage = " * Message is required")]
        public string Message { get; set; }


    }
}