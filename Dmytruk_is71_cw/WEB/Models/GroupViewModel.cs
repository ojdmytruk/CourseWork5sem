using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace WEB.Models
{
    public class GroupViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Введіть назву групи")]
        [RegularExpression(@"^([\u0410-\u044F\u0406\u0407\u0490\u0404\u0456\u0457\u0491\u0454a-zA-Z]{1,2})?-([0-9]{2})$", ErrorMessage = "Неправильна назва групи")]
        public string Name { get; set; }
    }
}
