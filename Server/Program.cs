
var builder = WebApplication.CreateBuilder(args);

using IHost host = Host.CreateDefaultBuilder(args).Build();
IConfiguration config = host.Services.GetRequiredService<IConfiguration>();

builder.Services.AddCors();
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();
app.UseHttpsRedirection();

app.UseCors(
    options => options.WithOrigins(config.GetValue<string>("clientUrl")).AllowAnyMethod().AllowAnyHeader()
    );

app.UseSwaggerUI(options =>
{
    options.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
    options.RoutePrefix = string.Empty;
});

app.MapGet("recipes", async () =>
{
    Data data = new(app.Logger);
    var recipes = await data.GetRecipesAsync();
    return Results.Ok(recipes);
});

app.MapGet("/recipes/{id}", async (Guid id) =>
{
    Data data = new(app.Logger);
    Recipe recipe = await data.GetRecipeAsync(id);
    return Results.Ok(recipe);
});

app.MapPost("/recipes", async (Recipe recipe) =>
{
    Data data = new(app.Logger);
    recipe.Id = Guid.NewGuid();
    await data.AddRecipeAsync(recipe);
    return Results.Created($"/recipes/{recipe.Id}",recipe);
});
    
app.MapPut("/recipes/{id}", async (Guid id, Recipe newRecipe) =>
{
    Data data = new(app.Logger);
    var updatedRecipe =await data.EditRecipeAsync(id, newRecipe);
    return Results.Ok(updatedRecipe);
});

app.MapDelete("/recipes/{id}", async (Guid id) =>
{
    Data data = new(app.Logger);
    await data.RemoveRecipeAsync(id);
    return Results.Ok();
});

app.MapGet("/categories", async () =>
{
    Data data = new(app.Logger);
    var categories = await data.GetAllCategoriesAsync();
    return Results.Ok(categories);

});

app.MapPost("/categories", async (string category) =>
{
    Data data = new(app.Logger);
    await data.AddCategoryAsync(category);
    return Results.Created($"/categories/{category}",category);
});

app.MapPut("/categories", async (string category, string newCategory) =>
{
    Data data = new(app.Logger);
    await data.EditCategoryAsync(category, newCategory);
    return Results.Ok($"Category ({category}) updated to ({newCategory})");
});

app.MapDelete("/categories", async (string category) =>
{
    Data data = new(app.Logger);
    await data.RemoveCategoryAsync(category);
    return Results.Ok();
});

app.MapPost("recipes/category", async (Guid id ,string category) =>
{
    Data data = new(app.Logger);
    await data.AddCategoryToRecipeAsync(id,category);
    return Results.Created($"recipes/category/{category}",category);
});

app.MapDelete("recipes/category", async (Guid id, string category) =>
{
    Data data = new(app.Logger);
    await data.RemoveCategoryFromRecipeAsync(id,category);
    return Results.Ok();
});

app.Run();