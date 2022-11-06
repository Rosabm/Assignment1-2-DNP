using System.Security.Claims;
using Domain.Models;
using Microsoft.AspNetCore.Mvc;
using WebApi.Services;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using Application.LogicInterfaces;
using Domain.DTOs;
using Microsoft.IdentityModel.Tokens;


namespace WebApi.Controllers;
[ApiController]
[Route("[controller]")]
public class AuthController : ControllerBase
{
    private readonly IConfiguration config;
    private readonly IAuthService authService;
    private readonly IUserLogic userLogic;

    public AuthController(IConfiguration config, IAuthService authService, IUserLogic userLogic)
    {
        this.config = config;
        this.authService = authService;
        this.userLogic = userLogic;
    }
    
    private List<Claim> GenerateClaims(User user)
    {
        var claims = new[]
        {
            new Claim(JwtRegisteredClaimNames.Sub, config["Jwt:Subject"]),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            new Claim(JwtRegisteredClaimNames.Iat, DateTime.UtcNow.ToString()),
            new Claim(ClaimTypes.Name, user.UserName),
           };
        return claims.ToList();
    }
    
    
    private string GenerateJwt(User user)
    {
        List<Claim> claims = GenerateClaims(user);
    
        SymmetricSecurityKey key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["Jwt:Key"]));
        SigningCredentials signIn = new SigningCredentials(key, SecurityAlgorithms.HmacSha512);
    
        JwtHeader header = new JwtHeader(signIn);
    
        JwtPayload payload = new JwtPayload(
            config["Jwt:Issuer"],
            config["Jwt:Audience"],
            claims, 
            null,
            DateTime.UtcNow.AddMinutes(60));
    
        JwtSecurityToken token = new JwtSecurityToken(header, payload);
    
        string serializedToken = new JwtSecurityTokenHandler().WriteToken(token);
        return serializedToken;
    }
    
    [HttpPost, Route("login")]
    public async Task<ActionResult> Login([FromBody] UserCreationDto userLoginDto)
    {
        try
        {
            User user = await authService.ValidateUser(userLoginDto.UserName, userLoginDto.Password);
            string token = GenerateJwt(user);
    
            return Ok(token);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }
    
    
    [HttpPost, Route("register")]
    public async Task<ActionResult<User>> RegisterAsync([FromBody] UserCreationDto dto)
    {
        try
        {
            User user = await userLogic.CreateAsync(dto);
            return Ok(user);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return StatusCode(500, e.Message);
        }
    }
  
}