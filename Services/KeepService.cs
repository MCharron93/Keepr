using System;
using System.Collections.Generic;
using Keepr.Models;
using Keepr.Repositories;

namespace Keepr.Services
{
  public class KeepsService
  {
    private readonly KeepsRepository _repo;

    public KeepsService(KeepsRepository repo)
    {
      _repo = repo;
    }

    public IEnumerable<Keep> GetAllKeeps()
    {
      return _repo.GetAllKeeps();
    }

    public Keep CreateKeep(Keep newKeep)
    {
      newKeep.Id = _repo.CreateKeep(newKeep);
      return newKeep;
    }

    public Keep GetSingleKeep(int id)
    {
      return _repo.GetSingleKeep(id);
    }

    public string Edit(Keep editData, Profile userInfo)
    {
      Keep original = GetSingleKeep(editData.Id);
      if (original.CreatorId == userInfo.Id)
      {
        editData.Name = editData.Name == null ? original.Name : editData.Name;
        editData.Description = editData.Description == null ? original.Description : editData.Description;
        editData.Img = editData.Img == null ? original.Img : editData.Img;
        _repo.Edit(editData);
        return "Successfully Updated";
      }
      else
      {
        throw new Exception("Access Denied.");
      }
    }

    internal string Delete(int id, string userId)
    {
      Keep original = _repo.GetSingleKeep(id);
      if (original == null || original.CreatorId != userId)
      {
        throw new Exception("Invalid Request!");
      }
      if (_repo.Delete(id))
      {
        return "Successfully Deleted Item.";
      }
      return "Unsucessful in Deleting Item.";
    }
  }
}