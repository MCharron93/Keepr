using System;
using System.Collections.Generic;
using System.Data;
using Keepr.Models;
using Dapper;

namespace Keepr.Repositories
{
  public class VaultKeepsRepository
  {
    private readonly IDbConnection _db;

    public VaultKeepsRepository(IDbConnection db)
    {
      _db = db;
    }

    internal int Create(VaultKeep newVKS)
    {
      string sql = @"
      INSERT INTO vaultKeeps
      (vaultId, keepId, creatorId)
      VALUES
      (@VaultId, @KeepId, @CreatorId);
      SELECT LAST_INSERT_ID();";
      return _db.ExecuteScalar<int>(sql, newVKS);
    }

    internal IEnumerable<Keep> GetKeepsByVaultId(int vaultId)
    {
      string sql = @"
      SELECT k.*,
      vks.id as VaultKeepsId,
      p.*
      FROM vaultkeeps vks
      JOIN keeps k ON k.id = vks.keepId
      JOIN kprofile p ON p.id = vks.creatorId
      WHERE vaultId = @VaultId;
      ";
      return _db.Query<VaultKeepsViewModel, Profile, VaultKeepsViewModel>(sql, (keep, profile) => { keep.Creator = profile; return keep; }, new { vaultId }, splitOn: "id");
    }

    internal bool Delete(int id)
    {
      string sql = "DELETE FROM vaultKeeps WHERE id = @Id";
      int valid = _db.Execute(sql, new { id });
      return valid > 0;
    }

    internal VaultKeep GetVaultKeepById(int id)
    {
      string sql = "SELECT * FROM vaultKeeps WHERE id = @Id";
      return _db.QueryFirstOrDefault<VaultKeep>(sql, new { id });
    }
  }
}