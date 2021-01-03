// using System;
// using System.Collections.Generic;
// using System.Data;
// using Keepr.Models;
// using Dapper;

// namespace Keepr.Repositories
// {
//   public class VaultKeepsRepository
//   {
//     private readonly IDbConnection _db;

//     public VaultKeepsRepository(IDbConnection db)
//     {
//       _db = db;
//     }

//     internal int Create(VaultKeeps newPWL)
//     {
//       // string sql = @"
//       // INSERT INTO productWish
//       // (productId, wishListId, creatorId)
//       // VALUES
//       // (@ProductId, @WishListId, @CreatorId);
//       // SELECT LAST_INSERT_ID();";
//       // return _db.ExecuteScalar<int>(sql, newPWL);
//     }

//     internal IEnumerable<Keep> GetKeepsByVaultId(int vaultId)
//     {
// string sql = @"
// SELECT pd.*,
// pwl.id as VaultKeepsId,
// p.*
// FROM productwish pwl
// JOIN products pd ON pd.id = pwl.productId
// JOIN profiles p ON p.id = pwl.creatorId
// WHERE vaultId = @VaultId;
// ";
// return _db.Query<VaultKeepsViewModel, Profile, VaultKeepsViewModel>(sql, (keep, profile) => { keep.Creator = profile; return keep; }, new { vaultId }, splitOn: "id");
//     }
//   }
// }