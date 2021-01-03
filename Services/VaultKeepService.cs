// using System;
// using System.Collections.Generic;
// using Keepr.Models;
// using Keepr.Repositories;

// namespace Keepr.Services
// {
//   public class VaultKeepsService
//   {
//     private readonly VaultKeepsRepository _repo;

//     public VaultKeepsService(VaultKeepsRepository repo)
//     {
//       _repo = repo;
//     }

//     internal VaultKeeps Create(VaultKeeps newVKS)
//     {
//       newVKS.Id = _repo.Create(newVKS);
//       return newVKS;
//     }

//     internal IEnumerable<Vault> GetKeepsByVaultId(int vaultId)
//     {
//       return _repo.GetKeepsByVaultId(vaultId);
//     }
//   }
// }