using Filmoteka.API.Services.Auth;
using Filmoteka.API.Services.Email;
using Filmoteka.API.Services.Reziser;
using Filmoteka.API.Services.Sala;
using Filmoteka.API.Services.Termin;
using Filmoteka.API.Services.User;
using Filmoteka.API.Services.Zanr;
using Filmoteka.API.Settings;
using MainProjectOOPIII3.Services.Account;
using MainProjectOOPIII3.Services.Film;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using praktika1.Data;
using praktika1.Models;
using System.Text;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

builder.WebHost.ConfigureKestrel(options =>
{
    options.ListenLocalhost(5000);
});

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.AddHttpContextAccessor();
builder.Services.AddDbContext<MyAppContext>(options =>
    options.UseSqlServer(connectionString));

builder.Services.AddScoped<IFilmService, FilmService>();
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<IReziserService, ReziserService>();
builder.Services.AddScoped<IZanrService, ZanrService>();
builder.Services.AddScoped<ISalaService, SalaService>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<ITerminService, TerminService>();
builder.Services.AddScoped<IEmailService, EmailService>();
builder.Services.AddScoped<JwtService>();

builder.Services.Configure<EmailSettings>(builder.Configuration.GetSection("Email"));

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = builder.Configuration["Jwt:Issuer"],
            ValidAudience = builder.Configuration["Jwt:Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]!))
        };
    });

// Add services to the container.
builder.Services.AddControllers().AddJsonOptions(options => {
    options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
});

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
    });
    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer" 
                }
            },
            new string[] {}
        }
    });
});

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowReactApp", policy =>
    {
        policy.WithOrigins("http://localhost:5173")
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});

var app = builder.Build();

/*
using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<MyAppContext>();
    await context.Database.EnsureDeletedAsync();
    await context.Database.MigrateAsync();

    if (!context.Zanrovi.Any())
    {
        var akcija = new Zanr { Naziv = "Akcija" };
        var scifi = new Zanr { Naziv = "Sci-Fi" };
        var drama = new Zanr { Naziv = "Drama" };
        var triler = new Zanr { Naziv = "Triler" };
        var komedija = new Zanr { Naziv = "Komedija" };
        var krimi = new Zanr { Naziv = "Krimi" };

        var nolan = new Reziser { Ime = "Christopher", Prezime = "Nolan", DatumRodjenja = new DateTime(1970, 7, 30), Filmovi = new List<Film>() };
        var tarantino = new Reziser { Ime = "Quentin", Prezime = "Tarantino", DatumRodjenja = new DateTime(1963, 3, 27), Filmovi = new List<Film>() };

        var lanaWachowski = new Reziser { Ime = "Lana", Prezime = "Wachowski", DatumRodjenja = new DateTime(1965, 6, 21), Filmovi = new List<Film>() };
        var lillyWachowski = new Reziser { Ime = "Lilly", Prezime = "Wachowski", DatumRodjenja = new DateTime(1967, 12, 29), Filmovi = new List<Film>() };

        var fincher = new Reziser { Ime = "David", Prezime = "Fincher", DatumRodjenja = new DateTime(1962, 8, 28), Filmovi = new List<Film>() };
        var scorsese = new Reziser { Ime = "Martin", Prezime = "Scorsese", DatumRodjenja = new DateTime(1942, 11, 17), Filmovi = new List<Film>() };
        var spielberg = new Reziser { Ime = "Steven", Prezime = "Spielberg", DatumRodjenja = new DateTime(1946, 12, 18), Filmovi = new List<Film>() };
        var darabont = new Reziser { Ime = "Frank", Prezime = "Darabont", DatumRodjenja = new DateTime(1959, 1, 28), Filmovi = new List<Film>() };
        var rodriguez = new Reziser { Ime = "Robert", Prezime = "Rodriguez", DatumRodjenja = new DateTime(1968, 6, 20), Filmovi = new List<Film>() };

        var joelCoen = new Reziser { Ime = "Joel", Prezime = "Coen", DatumRodjenja = new DateTime(1954, 11, 29), Filmovi = new List<Film>() };
        var ethanCoen = new Reziser { Ime = "Ethan", Prezime = "Coen", DatumRodjenja = new DateTime(1957, 9, 21), Filmovi = new List<Film>() };

        var filmovi = new List<Film>
        {
            new Film { Naziv = "The Matrix", GodinaIzdanja = 1999, Zanr = akcija, Opis = "Opis filma", Reziseri = new List<Reziser> { lanaWachowski, lillyWachowski } },
            new Film { Naziv = "No Country for Old Men", GodinaIzdanja = 2007, Zanr = krimi, Opis = "Opis filma", Reziseri = new List<Reziser> { joelCoen, ethanCoen } },
            new Film { Naziv = "Grindhouse", GodinaIzdanja = 2007, Zanr = akcija, Opis = "Opis filma", Reziseri = new List<Reziser> { tarantino, rodriguez } },
            new Film { Naziv = "The Matrix Reloaded", GodinaIzdanja = 2003, Zanr = scifi, Opis = "Opis filma", Reziseri = new List<Reziser> { lanaWachowski, lillyWachowski } },

            new Film { Naziv = "Inception", GodinaIzdanja = 2010, Zanr = scifi, Opis = "Opis filma", Reziseri = new List<Reziser> { nolan } },
            new Film { Naziv = "Interstellar", GodinaIzdanja = 2014, Zanr = scifi, Opis = "Opis filma", Reziseri = new List<Reziser> { nolan } },
            new Film { Naziv = "The Dark Knight", GodinaIzdanja = 2008, Zanr = akcija, Opis = "Opis filma", Reziseri = new List<Reziser> { nolan } },

            new Film { Naziv = "Pulp Fiction", GodinaIzdanja = 1994, Zanr = drama, Opis = "Opis filma", Reziseri = new List<Reziser> { tarantino } },
            new Film { Naziv = "Inglourious Basterds", GodinaIzdanja = 2009, Zanr = akcija, Opis = "Opis filma", Reziseri = new List<Reziser> { tarantino } },

            new Film { Naziv = "Fight Club", GodinaIzdanja = 1999, Zanr = drama, Opis = "Opis filma", Reziseri = new List<Reziser> { fincher } },
            new Film { Naziv = "Se7en", GodinaIzdanja = 1995, Zanr = triler, Opis = "Opis filma", Reziseri = new List<Reziser> { fincher } },

            new Film { Naziv = "Goodfellas", GodinaIzdanja = 1990, Zanr = krimi, Opis = "Opis filma", Reziseri = new List<Reziser> { scorsese } },
            new Film { Naziv = "The Wolf of Wall Street", GodinaIzdanja = 2013, Zanr = komedija, Opis = "Opis filma", Reziseri = new List<Reziser> { scorsese } },

            new Film { Naziv = "Saving Private Ryan", GodinaIzdanja = 1998, Zanr = drama, Opis = "Opis filma", Reziseri = new List<Reziser> { spielberg } },
            new Film { Naziv = "The Shawshank Redemption", GodinaIzdanja = 1994, Zanr = drama, Opis = "Opis filma", Reziseri = new List<Reziser> { darabont } }
        };

        context.Filmovi.AddRange(filmovi);
        await context.SaveChangesAsync();
    }
}
*/



if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Filmoteka API v1");
        c.RoutePrefix = "swagger";
    });
}

app.UseHttpsRedirection();
app.UseRouting();
app.UseCors("AllowReactApp");

app.UseAuthentication();
app.UseAuthorization();


app.MapControllers();

app.Run();
