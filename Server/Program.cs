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
    Recipe recipe = data.getRecipe(id);
    return Results.Ok(recipe);
});

app.MapPost("/recipes", (Recipe recipe) =>
{
    Data data = new();
    recipe.Id = Guid.NewGuid();
    data.AddRecipe(recipe);
    data.SaveData();
    return Results.Created($"/recipes/{recipe.Id}",recipe);
});

app.MapPut("/recipes/{id}", (Guid id, Recipe newRecipe) =>
{
    Data data = new();
    var updatedRecipe = data.EditRecipe(id, newRecipe);
    data.SaveData();
    return Results.Ok(updatedRecipe);
});

app.MapDelete("/recipes/{id}", (Guid id) =>
{
    Data data = new();
    data.RemoveRecipe(id);
    data.SaveData();
    return Results.Ok();
});

app.MapGet("/categories", () =>
{
    Data data = new();
    return Results.Ok(data.GetAllCategories());

});

app.MapPost("/categories", (string category) =>
{
    Data data = new();
    data.AddCategory(category);
    data.SaveData();
    return Results.Created($"/categories/{category}",category);
});

app.MapPut("/categories", (string category, string newCategory) =>
{
    Data data = new();
    data.EditCategory(category, newCategory);
    data.SaveData();
    return Results.Ok($"Category ({category}) updated to ({newCategory})");
});

app.MapDelete("/categories",(string category) =>
{
    Data data = new();
    data.RemoveCategory(category);
    data.SaveData();
    return Results.Ok();
});

app.MapPost("recipes/category", (Guid id ,string category) =>
{
    Data data = new();
    data.AddCategoryToRecipe(id,category);
    data.SaveData();
    return Results.Created($"recipes/category/{category}",category);
});

app.MapDelete("recipes/category", (Guid id, string category) =>
{
    Data data = new();
    data.RemoveCategoryFromRecipe(id,category);
    data.SaveData();
    return Results.Ok();
});

app.Run();