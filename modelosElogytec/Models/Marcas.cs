using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace modelosElogytec.Models
{
    public class Marcas
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "el campo {0} es requeido")]
        public string Marca { get; set; }
    }
}
