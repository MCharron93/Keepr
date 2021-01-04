using System;
using System.Collections.Generic;
using System.Data;
using Keepr.Models;
using Dapper;
using System.Linq;

namespace Keepr.Repositories
{
  public class VaultRepository
  {
    private readonly IDbConnection _db;
    private readonly string populateCreator = "SELECT vault.*, profile.* FROM vaults vault INNER JOIN kprofile profile ON vault.creatorId = profile.Id ";

    public VaultRepository(IDbConnection db)
    {
      _db = db;
    }

    internal int CreateVault(Vault newVault)
    {
      string sql = @"
      INSERT INTO vaults
      (name, creatorId, description, isPrivate)
      VALUES 
      (@Name, @CreatorId, @Description, @IsPrivate);
      SELECT LAST_INSERT_ID();
      ";
      return _db.ExecuteScalar<int>(sql, newVault);
    }

    // NOTE 
    internal Vault GetSingleVault(int id)
    {

      string sql = populateCreator + "WHERE vault.id = @id";
      return _db.Query<Vault, Profile, Vault>(sql, (vault, profile) => { vault.Creator = profile; return vault; }, new { id }, splitOn: "id").FirstOrDefault();
    }

    // internal Vault GetSingleVault(int id)
    // {
    //   string sql = "SELECT * FROM vaults WHERE id = @Id";
    //   return _db.QueryFirstOrDefault<Vault>(sql, new { id });
    // }

    internal bool Delete(int id)
    {
      string sql = "DELETE FROM Vaults WHERE id = @Id";
      int valid = _db.Execute(sql, new { id });
      return valid > 0;
    }

    public IEnumerable<Vault> GetVaultsByProfileId(string creatorId)
    {
      string sql = populateCreator + "WHERE creatorId = @CreatorId";
      return _db.Query<Vault, Profile, Vault>(sql, (vault, profile) => { vault.Creator = profile; return vault; }, new { creatorId }, splitOn: "id");
    }

    internal IEnumerable<Vault> GetAllVaults(string userId)
    {
      string sql = populateCreator + "WHERE creatorId = @userId";
      return _db.Query<Vault, Profile, Vault>(sql, (vault, profile) => { vault.Creator = profile; return vault; }, new { userId }, splitOn: "id");
    }
  }
}