using Bansi.WebUI.Models;
using Microsoft.AspNetCore.Mvc;
using System.Text;
using System.Text.Json;

namespace Bansi.WebUI.Controllers
{
    public class ExamenController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public ExamenController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<IActionResult> Index()
        {
            var modelo = new ExamenViewModel();
            modelo.ListaExamenes = await ObtenerExamenes();
            return View(modelo);
        }

        [HttpPost]
        public async Task<IActionResult> Guardar(ExamenViewModel modelo)
        {
            if (!ModelState.IsValid)
            {
                modelo.ListaExamenes = await ObtenerExamenes();
                return View("Index", modelo);
            }

            var client = _httpClientFactory.CreateClient("BansiApi");
            HttpResponseMessage response;

            if (modelo.IdExamen == 0)
            {
                var createDto = new
                {
                    Nombre = modelo.Nombre,
                    Descripcion = modelo.Descripcion
                };

                var content = new StringContent(JsonSerializer.Serialize(createDto), Encoding.UTF8, "application/json");
                response = await client.PostAsync($"Examen?usarSp={modelo.UsarSp}", content);
            }
            else
            {
                var content = new StringContent(JsonSerializer.Serialize(modelo), Encoding.UTF8, "application/json");
                response = await client.PutAsync($"Examen/{modelo.IdExamen}?usarSp={modelo.UsarSp}", content);
            }

            if (response.IsSuccessStatusCode)
            {
                TempData["Exito"] = "Operación realizada satisfactoriamente.";
            }
            else
            {
                TempData["Error"] = "Ocurrió un error al procesar la solicitud. Revisa los datos e intenta de nuevo.";
            }

            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> Eliminar(int idExamen, bool usarSp)
        {
            var client = _httpClientFactory.CreateClient("BansiApi");
            var response = await client.DeleteAsync($"Examen/{idExamen}?usarSp={usarSp}");

            if (response.IsSuccessStatusCode)
                TempData["Exito"] = "Registro eliminado correctamente.";
            else
                TempData["Error"] = "Error al intentar eliminar el registro.";

            return RedirectToAction("Index");
        }

        private async Task<List<ExamenViewModel>> ObtenerExamenes()
        {
            var client = _httpClientFactory.CreateClient("BansiApi");
            var response = await client.GetAsync("Examen");
            if (response.IsSuccessStatusCode)
            {
                var json = await response.Content.ReadAsStringAsync();
                return JsonSerializer.Deserialize<List<ExamenViewModel>>(json, new JsonSerializerOptions { PropertyNameCaseInsensitive = true }) ?? new List<ExamenViewModel>();
            }
            return new List<ExamenViewModel>();
        }
    }
}
