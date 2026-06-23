using Microsoft.EntityFrameworkCore;
using praktika1.Data;
using praktika1.Models;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.AddDbContext<MyAppContext>(options =>
    options.UseSqlite(connectionString));

// Add services to the container.
builder.Services.AddControllersWithViews();

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

            new Film { Naziv = "The Matrix", GodinaIzdanja = 1999, Zanr = akcija, Reziseri = new List<Reziser> { lanaWachowski, lillyWachowski } },
            new Film { Naziv = "No Country for Old Men", GodinaIzdanja = 2007, Zanr = krimi, Reziseri = new List<Reziser> { joelCoen, ethanCoen } },
            new Film { Naziv = "Grindhouse", GodinaIzdanja = 2007, Zanr = akcija, Reziseri = new List<Reziser> { tarantino, rodriguez } },
            new Film { Naziv = "The Matrix Reloaded", GodinaIzdanja = 2003, Zanr = scifi, Reziseri = new List<Reziser> { lanaWachowski, lillyWachowski } },

            new Film { Naziv = "Inception", GodinaIzdanja = 2010, Zanr = scifi, Reziseri = new List<Reziser> { nolan } },
            new Film { Naziv = "Interstellar", GodinaIzdanja = 2014, Zanr = scifi, Reziseri = new List<Reziser> { nolan } },
            new Film { Naziv = "The Dark Knight", GodinaIzdanja = 2008, Zanr = akcija, Reziseri = new List<Reziser> { nolan } },

            new Film { Naziv = "Pulp Fiction", GodinaIzdanja = 1994, Zanr = drama, Reziseri = new List<Reziser> { tarantino } },
            new Film { Naziv = "Inglourious Basterds", GodinaIzdanja = 2009, Zanr = akcija, Reziseri = new List<Reziser> { tarantino } },

            new Film { Naziv = "Fight Club", GodinaIzdanja = 1999, Zanr = drama, Reziseri = new List<Reziser> { fincher } },
            new Film { Naziv = "Se7en", GodinaIzdanja = 1995, Zanr = triler, Reziseri = new List<Reziser> { fincher } },

            new Film { Naziv = "Goodfellas", GodinaIzdanja = 1990, Zanr = krimi, Reziseri = new List<Reziser> { scorsese } },
            new Film { Naziv = "The Wolf of Wall Street", GodinaIzdanja = 2013, Zanr = komedija, Reziseri = new List<Reziser> { scorsese } },

            new Film { Naziv = "Saving Private Ryan", GodinaIzdanja = 1998, Zanr = drama, Reziseri = new List<Reziser> { spielberg } },
            new Film { Naziv = "The Shawshank Redemption", GodinaIzdanja = 1994, Zanr = drama, Reziseri = new List<Reziser> { darabont } }
        };

        context.Filmovi.AddRange(filmovi);
        await context.SaveChangesAsync();
    }
}
*/

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseRouting();

app.UseAuthorization();

app.MapStaticAssets();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();


app.Run();
