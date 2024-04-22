using System.Data.SqlClient;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using OtmApi.Data;
using OtmApi.Data.Entities;
using OtmApi.Utils.Exceptions;

namespace OtmApi.Services.Players;

public class PlayerService : IPlayerService
{
    private readonly DataContext _db;
    private readonly IMapper _mapper;

    public PlayerService(DataContext db, IMapper mapper)
    {
        _db = db;
        _mapper = mapper;
    }

    public Player? Delete(int id)
    {
        throw new NotImplementedException();
    }

    public List<Player> Get()
    {
        try
        {
            var players = _db.Players.ToList();
            return players;
        }
        catch (SqlException err)
        {
            Console.WriteLine(err.Message);
            throw;
        }
    }

    public async Task<Player?> GetByIdAsync(int id)
    {
        try
        {
            var player = await _db.Players.Include(p => p.Tournaments).SingleOrDefaultAsync((p) => p.Id == id);
            return player;
        }
        catch (SqlException err)
        {
            Console.WriteLine(err.Message);
            throw;
        };
    }

    public async Task<List<Player>> GetMultipleById(List<int> ids)
    {
        try
        {
            var players = await _db.Players.Where((p) => ids.Contains(p.Id)).ToListAsync();
            return players;
        }
        catch (SqlException err)
        {
            Console.WriteLine(err.Message);
            throw;
        };
    }

    public async Task<Player> PostAsync(Player player)
    {
        try
        {
            var addedPlayer = await _db.Players.AddAsync(player);
            await _db.SaveChangesAsync();

            return addedPlayer.Entity;
        }
        catch (SqlException err)
        {
            Console.WriteLine(err.Message);
            throw;
        }
    }

    public Player? Update(Player player)
    {

        throw new NotImplementedException();
        // TODO use automapper to update
        // try
        // {
        //     var playerToUpdate = _db.Players.SingleOrDefault((t) => t.Id == player.Id);
        //     if (playerToUpdate != null)
        //     {
        //         playerToUpdate.Name = player.Name;
        //         playerToUpdate.Rank = player.Rank;
        //         playerToUpdate.Country = player.Country;
        //         playerToUpdate.Tournaments = player.Tournaments;

        //         _db.SaveChanges();
        //         return playerToUpdate;
        //     }
        //     else
        //     {
        //         return null;
        //     }

        // }
        // catch (SqlException err)
        // {
        //     Console.WriteLine(err.Message);
        //     throw;
        // }
    }

    public bool Exists(int id)
    {
        try
        {
            var player = _db.Players.SingleOrDefault((p) => p.Id == id);
            return player != null;
        }
        catch (SqlException err)
        {
            Console.WriteLine(err.Message);
            throw;
        }
    }

    public List<Player> GetMinimal()
    {
        try
        {
            var players = _db.Players.Select(p => new Player
            {
                Id = p.Id,
                Username = p.Username,

            }).ToList();
            return players;
        }
        catch (SqlException err)
        {
            Console.WriteLine(err.Message);
            throw;
        }
    }



    public async Task AddMultipleAsync(List<Player> players)
    {
        await _db.AddRangeAsync(players);
        await _db.SaveChangesAsync();
        return;
    }

    public async Task UpdateDiscordUsername(int id, string newDiscordUsername)
    {
        var player = _db.Players.SingleOrDefault(p => p.Id == id);
        if (player == null) throw new NotFoundException("Player", id);

        player.DiscordUsername = newDiscordUsername;
        await _db.SaveChangesAsync();
    }
}
