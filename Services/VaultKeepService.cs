using System;
using System.Collections.Generic;
using Keepr.Models;
using Keepr.Repositories;

namespace Keepr.Services
{
  public class VaultKeepsService
  {
    private readonly VaultKeepsRepository _repo;

    public VaultKeepsService(VaultKeepsRepository repo)
    {
      _repo = repo;
    }

    internal VaultKeep Create(VaultKeep newVKS)
    {
      newVKS.Id = _repo.Create(newVKS);
      return newVKS;
    }

    public IEnumerable<Keep> GetKeepsByVaultId(int vaultId)
    {
      return _repo.GetKeepsByVaultId(vaultId);
    }

    public string Delete(int id, string userId)
    {
      VaultKeep found = _repo.GetVaultKeepById(id);
      if (found == null || found.CreatorId != userId)
      {
        throw new Exception("Invalid Request");
      }
      if (_repo.Delete(id))
      {
        return "Successfully Deleted!";
      }
      return "Unsuccessful Deletion";
    }

    public VaultKeep GetVaultKeepById(int id, Profile userInfo)
    {
      VaultKeep found = _repo.GetVaultKeepById(id);
      if (found.CreatorId == userInfo.Id)
      {

        return _repo.GetVaultKeepById(id);
      }
      else
      {
        throw new Exception("Invalid Request");
      }
    }
  }
}