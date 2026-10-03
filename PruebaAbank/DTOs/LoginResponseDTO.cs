using PruebaAbank.Aplicacion.CasosDeUso.Usuario.Consultas.ObtenerUsuarioPorId;

namespace PruebaAbank.DTOs
{
    public class LoginResponseDTO
    {
        public string Token { get; set; } = string.Empty;
        public UsuarioDetalleDTO Usuario { get; set; } = null!;
    }
}
