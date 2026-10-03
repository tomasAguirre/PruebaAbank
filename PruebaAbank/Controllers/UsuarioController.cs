using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using PruebaAbank.Aplicacion.CasosDeUso.Usuario.Comandos.ActualizarUsuario;
using PruebaAbank.Aplicacion.CasosDeUso.Usuario.Comandos.AgregarUuario;
using PruebaAbank.Aplicacion.CasosDeUso.Usuario.Comandos.EliminarUsuario;
using PruebaAbank.Aplicacion.CasosDeUso.Usuario.Consultas.ObtenerListadoUsuario;
using PruebaAbank.Aplicacion.CasosDeUso.Usuario.Consultas.ObtenerUsuarioPorId;
using PruebaAbank.Aplicacion.CasosDeUso.Usuario.Consultas.ObtenerUuarioPorTelefonoYPassword;
using PruebaAbank.Aplicacion.Mediador;
using PruebaAbank.Dominio.Entidades;
using PruebaAbank.DTOs;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace PruebaAbank.Controllers
{
    [ApiController]
    [Route("api/usuario")]
    public class UsuarioController : Controller
    {
        public IMediator Mediator { get; }
        public IConfiguration Configuration { get; }

        public UsuarioController(IMediator mediator, IConfiguration configuration)
        {
            Mediator = mediator;
            Configuration = configuration;
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Post(CrearUsuarioDTO crearUsuarioDTO)
        {
            var comando = new ComandoAgregarUsuario 
            { 
                Nombres = crearUsuarioDTO.Nombres,
                Apellidos = crearUsuarioDTO.Apellidos,
                FechaNacimiento = crearUsuarioDTO.FechaNacimiento,
                Direccion = crearUsuarioDTO.Direccion,
                Password = crearUsuarioDTO.Password,
                Telefono = crearUsuarioDTO.Telefono,
                Email = crearUsuarioDTO.Email,
                Estado = crearUsuarioDTO.Estado
            };
            await Mediator.Send(comando);
            return Ok();
        }

        [HttpGet]
        public async Task<List<ListadoUsuariosDTO>> Get()
        {
            var consulta = new ConsultaObtenerListadoUsuario();
            var listadoUsuarios = await Mediator.Send(consulta);
            return listadoUsuarios;
        }

        [HttpPost("login")]
        public async Task<IActionResult> login(TelefonoYPasswordDTO telefonoYPasswordDTO)
        {
            var consulta = new ConsultaObtenerUsuarioPorTelefonoYPassword() 
            {
                Password = telefonoYPasswordDTO.Password,
                Telefono = telefonoYPasswordDTO.Telefono
            };
            var usuarioDetalleDTO = await Mediator.Send(consulta);
            var tokenJwt = this.GenerarTokenJwt(usuarioDetalleDTO);

            var respuesta = new LoginResponseDTO
            {
                Token = tokenJwt,
                Usuario = usuarioDetalleDTO
            };
            return Ok(respuesta);
        }

        private string GenerarTokenJwt(UsuarioDetalleDTO usuario)
        {
            var jwtSettings = Configuration.GetSection("Jwt");
            var key = Encoding.UTF8.GetBytes(jwtSettings["Key"]!);

            // Definimos los claims (la información que viaja dentro del token)
            var claims = new[]
            {
        new Claim(JwtRegisteredClaimNames.Sub, usuario.Id.ToString()),
        new Claim(ClaimTypes.Name, usuario.Nombres),
        new Claim(ClaimTypes.MobilePhone, usuario.Telefono),
        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
    };

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddHours(2), // El token expira en 2 horas
                Issuer = jwtSettings["Issuer"],
                Audience = jwtSettings["Audience"],
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

        [Authorize]
        [HttpGet("{id}")]
        public async Task<UsuarioDetalleDTO> Get(int id)
        {
            var consulta = new ConsultaObtenerUsuarioPorId { id = id };
            var usuarioDetalleDTO = await Mediator.Send(consulta);
            return usuarioDetalleDTO;
        }

        [Authorize]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var consulta = new ComandoEliminarUsuario { Id = id };
            await Mediator.Send(consulta);
            return Ok();
        }

        [Authorize]
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, ActualizarUsuarioDTO actualizarUsuarioDTO)
        {
            var comando = new ComandoActualizarUsuario
            {
                Id = id,
                Nombres = actualizarUsuarioDTO.Nombres,
                Apellidos = actualizarUsuarioDTO.Apellidos,
                Direccion = actualizarUsuarioDTO.Direccion,
                FechaNacimiento = actualizarUsuarioDTO.FechaNacimiento,
                Password = actualizarUsuarioDTO.Password,
                Telefono = actualizarUsuarioDTO.Telefono,
                Email = actualizarUsuarioDTO.Email
            };
            await Mediator.Send(comando);
            return Ok();
        }

    }
}
