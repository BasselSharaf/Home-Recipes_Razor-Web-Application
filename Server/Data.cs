using System.Text.Json;

class Data
{
    private List<Recipe> _recipes { get; set; } = new();
    private List<string> _categories { get; set; } = new();
    private string _recipesFilePath;
    private string _categoriesFilePath;

    public Data ()
    {
        var systemPath = Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData);
        _recipesFilePath = Path.Combine(systemPath, "Recipes.json");
        _categoriesFilePath = Path.Combine(systemPath, "Categories.json");
    }

    public async Task LoadData()
    {
        if (!File.Exists(_recipesFilePath))
        {
            _recipes = new List<Recipe>();
            await File.WriteAllTextAsync(_recipesFilePath, JsonSerializer.Serialize(_recipes));
        }
        if (!File.Exists(this._categoriesFilePath))
        {
            _recipes = new List<Recipe>();
            await File.WriteAllTextAsync(_categoriesFilePath, JsonSerializer.Serialize(_recipes));
        }
        using (StreamReader r = new(_recipesFilePath))
        {
            var data = await r.ReadToEndAsync();
            var json = JsonSerializer.Deserialize<List<Recipe>>(data);
            if (json != null)
                _recipes = json;
        }
        using (StreamReader r = new(_categoriesFilePath))
        {
            var data = await r.ReadToEndAsync();
            var json = JsonSerializer.Deserialize<List<string>>(data);
            if (json != null)
                _categories = json;
        }
    }

    public List<Recipe> GetRecipes()
    {
        return _recipes;
    }

    public void AddRecipe(Recipe r)
    {
        _recipes.Add(r);
    }

    public Recipe GetRecipe(Guid id)
    {
        var recipe = _recipes.Find(r => r.Id == id);
        if (recipe == null)
            recipe = new Recipe();
        return recipe;
    }

    public void RemoveRecipe(Guid id)
    {
        var recipe = GetRecipe(id); 
        _recipes.Remove(recipe);
    }

    public Recipe EditRecipe(Guid id, Recipe newRecipe)
    {
        var recipe = GetRecipe(id);
        recipe.Title = newRecipe.Title;
        recipe.Ingredients = newRecipe.Ingredients;
        recipe.Instructions = newRecipe.Instructions;
        recipe.Categories = newRecipe.Categories;
        return recipe;
    }

    public void EditTitle(Guid id, string newTitle)
    {
        var recipe = GetRecipe(id);
        recipe.Title = newTitle;
    }

    public void EditIngredients(Guid id, string newIngredients)
    {
        var recipe = GetRecipe(id);
        recipe.Ingredients = newIngredients;
    }

    public void EditInstructions(Guid id, string newInstructions)
    {
        var recipe = GetRecipe(id);
        recipe.Instructions = newInstructions;
    }
    
    //TODO: The only way to edit a category is from the original Method that will edit it inside all recipes
    public void EditCategory(Guid id, string category, string newCategory)
    {
        RemoveCategoryFromRecipe(id, category);
        AddCategoryToRecipe(id, newCategory);
    }

    public List<string> GetAllCategories()
    {
        return _categories;
    }

    public void AddCategory(string category)
    {
        if(!_categories.Contains(category))
            _categories.Add(category);
    }

    public void EditCategory(string category, string newCategory)
    {
        var index = _categories.IndexOf(category);
        if (index != -1)
        {
            _categories[index] = newCategory;
            foreach(var recipe in _recipes)
                foreach(var c in recipe.Categories)
                    if (c == category)
                    {
                        recipe.Categories[recipe.Categories.IndexOf(c)] = newCategory;
                    }
        }
    }

    public void RemoveCategory(string category)
    {
        if (_categories.Contains(category))
        {
            _categories.Remove(category);
            var recipes =_recipes.Where(r => r.Categories.Contains(category)).ToList();
            foreach (var recipe in recipes)
                recipe.Categories.Remove(category);
        }
    }

    public void AddCategoryToRecipe(Guid id, string category)
    {
        if (_categories.Contains(category))
        {
            var recipe = GetRecipe(id);
            recipe.Categories.Add(category);
        }
    }

    public void RemoveCategoryFromRecipe(Guid id, string category)
    {
        var recipe = GetRecipe(id);
        recipe.Categories.Remove(category);
    }

    public async Task WriteToFile(string path, string data)
    {

    }

    public async Task SaveDataAsync()
    {
        await File.WriteAllTextAsync(_recipesFilePath, JsonSerializer.Serialize(_recipes));
        await File.WriteAllTextAsync(_categoriesFilePath, JsonSerializer.Serialize(_categories));
    }
}