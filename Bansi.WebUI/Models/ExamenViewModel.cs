using System.ComponentModel.DataAnnotations;

namespace Bansi.WebUI.Models
{
    public class ExamenViewModel
    {
        public int IdExamen { get; set; }
        [Required(ErrorMessage = "El nombre es obligatorio")]
        [StringLength(255)]
        public string Nombre { get; set; } = string.Empty;
        [StringLength(255)]
        public string Descripcion { get; set; } = string.Empty;
        public bool UsarSp { get; set; }
        public List<ExamenViewModel> ListaExamenes { get; set; } = new();
    }
}
