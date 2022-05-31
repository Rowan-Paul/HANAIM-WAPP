using Microsoft.EntityFrameworkCore;

namespace Inside_Airbnb.Server.Repositories;

public class NeighbourhoodRepository : INeighbourhoodRepository
{
    private readonly inside_airbnbContext _context;

    public NeighbourhoodRepository(inside_airbnbContext context)
    {
        _context = context;
    }

    public async Task<List<Neighbourhood?>> GetAllNeighbourhoods()
    {
        List<Neighbourhood?> neighbourhoods = await _context.Neighbourhoods.ToListAsync();

        return neighbourhoods;
    }

    public async Task<Neighbourhood?> GetNeighbourhoodById(int id)
    {
        Neighbourhood? neighbourhood = await _context.Neighbourhoods.FindAsync(id);

        return neighbourhood;
    }
}