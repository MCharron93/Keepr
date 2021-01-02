using System;
using System.Collections.Generic;
using System.Data;
using Keepr.Models;
using Dapper;

namespace Keepr.Repositories
{
  public class KeepsRepository
  {
    private readonly IDbConnection _db;
    private readonly string populateCreator = "SELECT keep.*, profile.* FROM keeps keep INNER JOIN kprofile profile ON keep.creatorId = profile.Id ";

    public KeepsRepository(IDbConnection db)
    {
      _db = db;
    }

    // Test the functionality of this when the WHERE statement is removed
    public IEnumerable<Keep> GetAllKeeps()
    {
      string sql = populateCreator;
      return _db.Query<Keep, Profile, Keep>(sql, (keep, profile) => { keep.Creator = profile; return keep; }, splitOn: "id");
    }

    // Test the functionality of this and remove the IsAvailable trait
    internal int CreateKeep(Keep newKeep)
    {
      string sql = @"
      INSERT INTO keeps
      (name, img, creatorId)
      VALUES 
      (@Name, @Img, @CreatorId);
      SELECT LAST_INSERT_ID();
      ";
      return _db.ExecuteScalar<int>(sql, newKeep);
    }

    internal Keep GetSingleKeep(int id)
    {
      string sql = @"SELECT * FROM keeps WHERE id = @id";
      return _db.QueryFirstOrDefault<Keep>(sql, new { id });
    }

    // Will need to handle increasing keep/view count in service for this!!!!
    internal void Edit(Keep editData)
    {
      string sql = @"
      UPDATE keeps
      SET 
      name = @Name,
      description = @Description,
      img = @Img
      WHERE id = @Id;";
      _db.Execute(sql, editData);
    }

    internal bool Delete(int id)
    {
      string sql = "DELETE FROM keeps WHERE id = @Id";
      int valid = _db.Execute(sql, new { id });
      return valid > 0;
    }

    // internal IEnumerable<Keep> GetKeepsByProfileId(Profile id)
    // {
    //   string sql = "SELECT * FROM keeps WHERE id = @Id";
    //   return _db.Query<Keep>(sql, creatorId);
    // }
  }
}