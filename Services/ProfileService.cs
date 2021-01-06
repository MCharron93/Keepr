using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Keepr.Models;
using Keepr.Repositories;

namespace Keepr.Services
{
  public class ProfileService
  {
    private readonly ProfileRepository _repo;
    public ProfileService(ProfileRepository repo)
    {
      _repo = repo;
    }

    public Profile GetOrCreateProfile(Profile userInfo)
    {
      Profile foundProfile = _repo.GetByEmail(userInfo.Email);
      if (foundProfile == null)
      {
        return _repo.Create(userInfo);
      }
      return foundProfile;
    }

    // NOTE Updates for KeepCounts based on creating and deleting keeps
    public void updateKeepCount(Profile userInfo)
    {
      userInfo.Keeps++;
      _repo.updateKeepCount(userInfo);
    }

    public string deleteKeepCount(Profile userInfo)
    {
      userInfo.Keeps--;
      _repo.updateKeepCount(userInfo);
      return "Successfully decreased total keeps!";
    }

    // NOTE Updates for VaultCount based on creating and deleting vaults
    public void updateVaultCount(Profile userInfo)
    {
      userInfo.Vaults++;
      _repo.updateVaultCount(userInfo);
    }

    public Profile GetProfileById(string id)
    {
      return _repo.GetProfileById(id);
    }
  }
}