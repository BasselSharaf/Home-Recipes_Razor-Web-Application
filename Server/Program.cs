var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.MapGet("", () =>
{
    return Results.Ok("Welcome to Recipes API");
});

app.MapGet("/recipes", () =>
{
    Data data = new();
    return Results.Ok(data.GetRecipes());
});

app.MapGet("/recipes/{id}", (Guid id) =>
{
    Data data = new();
    Recipe recipe = data.GetRecipe(id);
    return Results.Ok(recipe);
});

app.MapPost("/recipes", async (Recipe recipe) =>
{
    Data data = new();
    recipe.Id = Guid.NewGuid();
    data.AddRecipe(recipe);
    await data.SaveDataAsync();
    return Results.Created($"/recipes/{recipe.Id}",recipe);
});

app.MapPut("/recipes/{id}", async (Guid id, Recipe newRecipe) =>
{
    Data data = new();
    var updatedRecipe = data.EditRecipe(id, newRecipe);
    await data.SaveDataAsync();
    return Results.Ok(updatedRecipe);
});

app.MapDelete("/recipes/{id}", async (Guid id) =>
{
    Data data = new();
    data.RemoveRecipe(id);
    await data.SaveDataAsync();
    return Results.Ok();
});

app.MapGet("/categories", () =>
{
    Data data = new();
    return Results.Ok(data.GetAllCategories());

});

app.MapPost("/categories", async (string category) =>
{
    Data data = new();
    data.AddCategory(category);
    await data.SaveDataAsync();
    return Results.Created($"/categories/{category}",category);
});

app.MapPut("/categories", async (string category, string newCategory) =>
{
    Data data = new();
    data.EditCategory(category, newCategory);
    await data.SaveDataAsync();
    return Results.Ok($"Category ({category}) updated to ({newCategory})");
});

app.MapDelete("/categories", async (string category) =>
{
    Data data = new();
    data.RemoveCategory(category);
    await data.SaveDataAsync();
    return Results.Ok();
});

app.MapPost("recipes/category", async (Guid id ,string category) =>
{
    Data data = new();
    data.AddCategoryToRecipe(id,category);
    await data.SaveDataAsync();
    return Results.Created($"recipes/category/{category}",category);
});

app.MapDelete("recipes/category", async (Guid id, string category) =>
{
    Data data = new();
    data.RemoveCategoryFromRecipe(id,category);
    await data.SaveDataAsync();
    return Results.Ok();
});

app.Run();