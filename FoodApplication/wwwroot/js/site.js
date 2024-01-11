// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

let apiURL = "https://forkify-api.herokuapp.com/api/v2/recipes";
let apiKey = "2138a512-189e-4776-a61c-133e1546e1e3";

async function GetRecipes(recipeName,id,isAllShow) {
    let resp = await fetch(`${apiURL}?search=${recipeName}&key=${apiKey}`);
    let result = await resp.json();
    let Recipes = isAllShow ? result.data.recipes : result.data.recipes.slice(1, 7);
    showRecipes(Recipes, id);
     
}
function showRecipes(recipes, id) {
    $.ajax({
        contentType: "application/json; charset=utf-8",
        dataType: 'html',
        type: 'POST',
        url: '/Recipe/GetRecipeCard',
        data: JSON.stringify(recipes),
        success: function (htmlResult) {
            $('#' + id).html(htmlResult);
        }
    })
}
async function getOrderRecipe(id,showId) {
    
    let resp = await fetch(`${apiURL}/${id}?key=${apiKey}`);
    let result = await resp.json();
    console.log(result);
    let recipe = result.data.recipe;
    showOrderRecipeDetails(recipe, showId);

}
function showOrderRecipeDetails(orderRecipeDetails, showId) {
    $.ajax({
        dataType: 'html',
        type: 'POST',
        url: '/Recipe/ShowOrder',
        data: orderRecipeDetails,
        success: function (htmlResult) {
            $('#' + showId).html(htmlResult);
        }
    })
} 

// OrderPage
function quantity(option) {
    let qty = $('#qty').val();
    if (option === 'inc') {
        qty = parseInt(qty)+ 1;
    } else {
        qty = qty == 1 ? qty : qty - 1;
    }
    $('#qty').val(qty);
}