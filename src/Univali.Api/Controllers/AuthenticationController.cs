using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Univali.Api.Models;

namespace Univali.Api.Controllers;

[ApiController]
[Route("api/authentication")]
public class AuthenticationController : ControllerBase
{
    private readonly IConfiguration _configuration;
    public AuthenticationController(IConfiguration configuration) {
        _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
    }

    [HttpPost("authenticate")]
    public ActionResult<String> Authenticate(AuthenticationRequestDto authenticationRequestDto) {
        var user = ValidateUserCredentials(authenticationRequestDto.Username!, authenticationRequestDto.password!);
        if(user == null)
            return Unauthorized();

        var securityKey = new SymmetricSecurityKey(
            Encoding.UTF8.GetBytes(
                _configuration["Authentication:SecretKey"]
                    ?? throw new ArgumentNullException(nameof(_configuration))
            )
        );

        var signingCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

        var claims = new List<Claim>();
        claims.Add(new Claim("sub", user.UserId.ToString()));
        claims.Add(new Claim("given_name", user.Name));

        var jwt = new JwtSecurityToken(
            _configuration["Authentication:Issuer"],
            _configuration["Authentication:Audience"],
            claims,
            DateTime.UtcNow,
            DateTime.UtcNow.AddHours(1),
            signingCredentials
        );

        var jwtToReturn = new JwtSecurityTokenHandler().WriteToken(jwt);
        return Ok(jwtToReturn);
    }

    private InfoUser? ValidateUserCredentials(string username, string password) {
        var userFromDatabase = new Entities.User {
            Id = 1,
            Name = "Ada Lovelace",
            Username = "love",
            Password = "MinhaSenha"
        };

        if(userFromDatabase.Username == username && userFromDatabase.Password == password)
            return new InfoUser(userFromDatabase.Id, username, userFromDatabase.Name);
        return null;
    }

    private class InfoUser {
        public int UserId {get; set;}
        public string UserName {get; set;}
        public string Name {get; set;}

        public InfoUser(int userId, string userName, string name) {
            UserId = userId;
            UserName = userName;
            Name = name;
        }
    }
}