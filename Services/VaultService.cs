using System;
using System.Collections.Generic;
using Keepr.Models;
using Keepr.Repositories;

namespace Keepr.Services
{
  public class VaultService
  {
    private readonly VaultRepository _repo;

    public VaultService(VaultRepository repo)
    {
      _repo = repo;
    }

    public Vault CreateVault(Vault newVault)
    {
      newVault.Id = _repo.CreateVault(newVault);
      return newVault;
    }

    public IEnumerable<Vault> GetAllVaults(string userId)
    {

      return _repo.GetAllVaults(userId);
    }

    public Vault GetSingleVault(int id, Profile userInfo)
    {
      Vault foundVault = _repo.GetSingleVault(id);
      if (foundVault.IsPrivate != true)
      {
        return foundVault;
      }
      else if (userInfo != null && foundVault.CreatorId == userInfo.Id)
      {
        return foundVault;
      }
      throw new Exception("Invalid Request");
    }

    public string Delete(int id, string userId)
    {
      Vault original = _repo.GetSingleVault(id);
      if (original == null || original.CreatorId != userId)
      {
        throw new Exception("Invalid Request!");
      }
      if (_repo.Delete(id))
      {
        return "Successfully Deleted Vault";
      }
      return "Unsuccessful in Deleting Vault";
    }

    public IEnumerable<Vault> GetVaultsByProfileId(string creatorId)
    {
      return _repo.GetVaultsByProfileId(creatorId);
    }


  }
}