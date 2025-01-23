
namespace PokemonAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddAuthorization();

            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();

            var pokemons = new List<Pokemon>
            {
                new Pokemon {Id = 1, Name = "bulbasaur", Type = "Grass"},
                new Pokemon {Id = 2, Name = "Ivysaur", Type = "Grass"},
                new Pokemon {Id = 3, Name = "Venosaur", Type = "Grass"},
                new Pokemon {Id = 4, Name = "Charmander", Type = "Fire"}
            };
            //Create
            app.MapPost("/pokemon", (Pokemon pokemon) =>
            {
                pokemons.Add(pokemon);
                return Results.Ok(pokemons);
            });

            //Read all
            app.MapGet("/pokemons", () =>
            {
                return Results.Ok(pokemons);
            });

            //Read By Id
            app.MapGet("/pokemon/{id}", (int id) =>
            {
                var pokemon = pokemons.Find(p => p.Id == id);

                if (pokemon == null)
                {
                    return Results.NotFound("Sorry, this pokemon does not exist");
                }

                return Results.Ok(pokemon);
            });
            //Update 
            app.MapPut("/pokemon/{id}", (Pokemon updatePokemon, int id) =>
            {
                var pokemon = pokemons.Find(p => p.Id == id);
                if (pokemon == null)
                {
                    return Results.NotFound("This pokemon does not exist!");
                }

                pokemons [id] = updatePokemon;


                return Results.Ok(pokemon);
            });
            //Delete
            app.MapDelete("/pokemon/{id}", (int id) =>
            {
                var pokemon = pokemons.Find(p => p.Id == id);
                if (pokemon == null)
                {
                    return Results.NotFound("You cant remove this pokemon because its not existing");
                }

                pokemons.Remove(pokemon);

                return Results.Ok("ok");
            });
            app.Run();
        }
    }
}
