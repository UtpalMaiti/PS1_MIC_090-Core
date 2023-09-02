using BlazorApp.Server.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http.HttpResults;
using BlazorApp.Server.Models;

namespace BlazorApp.Server.Controllers
{
    public static class NameSuffixEndpoints
    {
        public static void MapNameSuffixEndpoints(this IEndpointRouteBuilder routes)
        {
            var group = routes.MapGroup("/api/NameSuffix").WithTags(nameof(NameSuffix));

            group.MapGet("/", async (ApplicationDbContext db) =>
            {
                return await db.NameSuffix.ToListAsync();
            })
            .WithName("GetAllNameSuffixes")
            .WithOpenApi();

            group.MapGet("/{id}", async Task<Results<Ok<NameSuffix>, NotFound>> (int suffixid, ApplicationDbContext db) =>
            {
                return await db.NameSuffix.AsNoTracking()
                    .FirstOrDefaultAsync(model => model.SuffixId == suffixid)
                    is NameSuffix model
                        ? TypedResults.Ok(model)
                        : TypedResults.NotFound();
            })
            .WithName("GetNameSuffixById")
            .WithOpenApi();

            group.MapPut("/{id}", async Task<Results<Ok, NotFound>> (int suffixid, NameSuffix nameSuffix, ApplicationDbContext db) =>
            {
                var affected = await db.NameSuffix
                    .Where(model => model.SuffixId == suffixid)
                    .ExecuteUpdateAsync(setters => setters
                      .SetProperty(m => m.SuffixId, nameSuffix.SuffixId)
                      .SetProperty(m => m.Suffix, nameSuffix.Suffix)
                    );

                return affected == 1 ? TypedResults.Ok() : TypedResults.NotFound();
            })
            .WithName("UpdateNameSuffix")
            .WithOpenApi();

            group.MapPost("/", async (NameSuffix nameSuffix, ApplicationDbContext db) =>
            {
                db.NameSuffix.Add(nameSuffix);
                await db.SaveChangesAsync();
                return TypedResults.Created($"/api/NameSuffix/{nameSuffix.SuffixId}", nameSuffix);
            })
            .WithName("CreateNameSuffix")
            .WithOpenApi();

            group.MapDelete("/{id}", async Task<Results<Ok, NotFound>> (int suffixid, ApplicationDbContext db) =>
            {
                var affected = await db.NameSuffix
                    .Where(model => model.SuffixId == suffixid)
                    .ExecuteDeleteAsync();

                return affected == 1 ? TypedResults.Ok() : TypedResults.NotFound();
            })
            .WithName("DeleteNameSuffix")
            .WithOpenApi();
        }
    }
}