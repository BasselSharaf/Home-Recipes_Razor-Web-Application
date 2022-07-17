var builder = WebApplication.CreateBuilder(args);

using IHost host = Host.CreateDefaultBuilder(args).Build();
IConfiguration config = host.Services.GetRequiredService<IConfiguration>();

builder.Services.AddCors();

var app = builder.Build();

app.UseCors(
    options => options.WithOrigins(config.GetValue<string>("clientUrl")).AllowAnyMethod().AllowAnyHeader()
    );

app.MapGet("", () =>
{
    return Results.Ok("Welcome to Recipes API");
});

app.MapGet("/recipes", async () =>
{
    Data data = new();
    var recipes = await data.GetRecipesAsync();
    return Results.Ok(recipes);
});

app.MapGet("/recipes/{id}", async (Guid id) =>
{
    Data data = new();
    Recipe recipe = await data.GetRecipeAsync(id);
    return Results.Ok(recipe);
});

app.MapPost("/recipes", async (Recipe recipe) =>
{
    Data data = new();
    recipe.Id = Guid.NewGuid();
    await data.AddRecipeAsync(recipe);
    return Results.Created($"/recipes/{recipe.Id}",recipe);
});

app.MapPut("/recipes/{id}", async (Guid id, Recipe newRecipe) =>
{
    Data data = new();
    var updatedRecipe =await data.EditRecipeAsync(id, newRecipe);
    return Results.Ok(updatedRecipe);
});

app.MapDelete("/recipes/{id}", async (Guid id) =>
{
    Data data = new();
    await data.RemoveRecipeAsync(id);
    return Results.Ok();
});

app.MapGet("/categories", async () =>
{
    Data data = new();
    var categories = await data.GetAllCategoriesAsync();
    return Results.Ok(categories);

});

app.MapPost("/categories", async (string category) =>
{
    Data data = new();
    await data.AddCategoryAsync(category);
    return Results.Created($"/categories/{category}",category);
});

app.MapPut("/categories", async (string category, string newCategory) =>
{
    Data data = new();
    await data.EditCategoryAsync(category, newCategory);
    return Results.Ok($"Category ({category}) updated to ({newCategory})");
});

app.MapDelete("/categories", async (string category) =>
{
    Data data = new();
    await data.RemoveCategoryAsync(category);
    return Results.Ok();
});

app.MapPost("recipes/category", async (Guid id ,string category) =>
{
    Data data = new();
    await data.AddCategoryToRecipeAsync(id,category);
    return Results.Created($"recipes/category/{category}",category);
});

app.MapDelete("recipes/category", async (Guid id, string category) =>
{
    Data data = new();
    await data.RemoveCategoryFromRecipeAsync(id,category);
    return Results.Ok();
});

app.Run();