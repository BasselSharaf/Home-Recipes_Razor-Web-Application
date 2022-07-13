var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.MapGet("", () =>
{
    return Results.Ok("Welcome to Recipes API");
});

app.MapGet("/recipes", async () =>
{
    Data data = new();
    await data.LoadData();
    return Results.Ok(data.GetRecipes());
});

app.MapGet("/recipes/{id}", async (Guid id) =>
{
    Data data = new();
    await data.LoadData();
    Recipe recipe = data.GetRecipe(id);
    return Results.Ok(recipe);
});

app.MapPost("/recipes", async (Recipe recipe) =>
{
    Data data = new();
    await data.LoadData();
    recipe.Id = Guid.NewGuid();
    data.AddRecipe(recipe);
    await data.SaveDataAsync();
    return Results.Created($"/recipes/{recipe.Id}",recipe);
});

app.MapPut("/recipes/{id}", async (Guid id, Recipe newRecipe) =>
{
    Data data = new();
    await data.LoadData();
    var updatedRecipe = data.EditRecipe(id, newRecipe);
    await data.SaveDataAsync();
    return Results.Ok(updatedRecipe);
});

app.MapDelete("/recipes/{id}", async (Guid id) =>
{
    Data data = new();
    await data.LoadData();
    data.RemoveRecipe(id);
    await data.SaveDataAsync();
    return Results.Ok();
});

app.MapGet("/categories", async () =>
{
    Data data = new();
    await data.LoadData();
    return Results.Ok(data.GetAllCategories());

});

app.MapPost("/categories", async (string category) =>
{
    Data data = new();
    await data.LoadData();
    data.AddCategory(category);
    await data.SaveDataAsync();
    return Results.Created($"/categories/{category}",category);
});

app.MapPut("/categories", async (string category, string newCategory) =>
{
    Data data = new();
    await data.LoadData();
    data.EditCategory(category, newCategory);
    await data.SaveDataAsync();
    return Results.Ok($"Category ({category}) updated to ({newCategory})");
});

app.MapDelete("/categories", async (string category) =>
{
    Data data = new();
    await data.LoadData();
    data.RemoveCategory(category);
    await data.SaveDataAsync();
    return Results.Ok();
});

app.MapPost("recipes/category", async (Guid id ,string category) =>
{
    Data data = new();
    await data.LoadData();
    data.AddCategoryToRecipe(id,category);
    await data.SaveDataAsync();
    return Results.Created($"recipes/category/{category}",category);
});

app.MapDelete("recipes/category", async (Guid id, string category) =>
{
    Data data = new();
    await data.LoadData();
    data.RemoveCategoryFromRecipe(id,category);
    await data.SaveDataAsync();
    return Results.Ok();
});

app.Run();